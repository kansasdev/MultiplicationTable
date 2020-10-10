using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using System.Xml.Linq;
using Xamarin.Forms;
using System.Linq;
using Xamarin.Forms.Xaml;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Threading;
using System.Linq.Expressions;
using Acr.UserDialogs;
using Xamarin.Essentials;
using System.Net.Http.Headers;
using MultiplicationTable.Models;
using MultiplicationTable.Views;

namespace MultiplicationTable.ViewModels
{
    public class DictViewModel : BaseViewModel
    {
        public Command ShuffleCommand { get; }
        public Command SayCommand { get; }
        public Command CheckCommand { get; }

        private string txtXml;
        private XDocument xDoc;
        private string selectedText;

        private List<SpecialWords> lstSpecialWords;

        private FormattedString fText;
        public FormattedString FText
        {
            get { return fText; }
            set
            {
                SetProperty(ref fText, value);
            }
        }

        private bool buttonEnabled;
        public bool ButtonEnabled
        {
            get { return buttonEnabled; }
            set
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    SayCommand?.ChangeCanExecute();
                });
                SetProperty(ref buttonEnabled, value);
            }
        }

        private bool buttonSayEnabled;
        public bool ButtonSayEnabled
        {
            get { return buttonSayEnabled; }
            set
            {
               
                SetProperty(ref buttonSayEnabled, value);
            }
        }

        private bool waitIndicator;
        public bool WaitIndicator
        {
            get { return waitIndicator; }
            set
            {
                SetProperty(ref waitIndicator, value);
            }
        }

        private void SetWaiting(bool state)
        {
           
                WaitIndicator = state;
                ButtonEnabled = !state;
                ButtonEnabled = !state;
           
        }

        public DictViewModel()
        {
            Title = "Dictation";
            ButtonEnabled = true;
            lstSpecialWords = new List<SpecialWords>();

            ShuffleCommand = new Command(new Action(ShuffleCommandAction));
            SayCommand = new Command(new Action<object>(SayItCommandAction), CanSayItCommandAction);
            CheckCommand = new Command(new Action<object>(CheckCommandAction), new Func<object, bool>(CanCheckCommandAction));

            SayCommand.ChangeCanExecute();

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(DictViewModel)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("MultiplicationTable.XMLText.xml");
            txtXml = string.Empty;
            using (var reader = new System.IO.StreamReader(stream))
            {
                txtXml = reader.ReadToEnd();
            }
            xDoc = XDocument.Parse(txtXml);
        }

        private void ShuffleCommandAction()
        {
            if(xDoc!=null)
            {
                Task.Run(() =>
                {
                    SetWaiting(true);
                    try
                    {
                        List<XElement> lst = new List<XElement>();
                        IEnumerable<XElement> de =
                        from el in xDoc.Descendants()
                        select el;
                        foreach (XElement el in de)
                        {
                            lst.Add(el);
                        }

                        Random r = new Random();
                        int gettedDict = r.Next(1, lst.Count);

                        selectedText = lst[gettedDict].Value;
                        List<string> wordsAndPunctation = selectedText.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();

                        FText = new FormattedString();
                        int indexSpecialWords = 0;
                        List<SpecialWords> lstWords = new List<SpecialWords>();
                        foreach (string word in wordsAndPunctation)
                        {
                            if (word.Contains("Ó") || word.Contains("ó") || word.Contains("U") || word.Contains("u") || word.Contains("rz") || word.Contains("Rz") ||
                                word.Contains("Ż") || word.Contains("ż") || word.Contains("h") || word.Contains("H") || word.Contains("Ch") || word.Contains("ch")
                                )
                            {
                                
                                MainThread.BeginInvokeOnMainThread(() =>
                                {
                                    SayCommand.ChangeCanExecute();

                                    SpecialWords sw = new SpecialWords(word);

                                    Span s = new Span() { Text = sw.GetDashedWord() + " ", TextColor = Color.Red };
                                    s.GestureRecognizers.Add(new TapGestureRecognizer()
                                    {
                                        NumberOfTapsRequired = 1,
                                        Command = new Command(WordTapped),
                                        CommandParameter = indexSpecialWords
                                    });
                                    FText.Spans.Add(s);
                                    lstSpecialWords.Add(sw);
                                    indexSpecialWords++;
                                });
                                lstWords.Add(new SpecialWords(word));
                                
                            }
                            else
                            {
                                MainThread.BeginInvokeOnMainThread(() =>
                                {
                                    FText.Spans.Add(new Span() { Text = word + " " });
                                });
                            }
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        UserDialogs.Instance.Alert("Error: " + ex.Message, "ERROR", "OK");
                    }
                    finally
                    {
                        SetWaiting(false);
                    }
                });
                
            }
        }

        private void WordTapped(object o)
        {
            if (lstSpecialWords != null)
            {
                SpecialWords sw = lstSpecialWords[(int)o];
                sw.SetLetterPlaces();

                WordDetails wp = new WordDetails(sw);
                
                Application.Current.MainPage.Navigation.PushModalAsync(wp);
            }
        }

        private void SayItCommandAction(object o)
        {
            Task.Run(() =>
            {
                ButtonEnabled = false;
                Task.WaitAll(TextToSpeech.SpeakAsync(selectedText));
                ButtonEnabled = true;
            });
        }

        private bool CanSayItCommandAction(object o)
        {
            if(string.IsNullOrEmpty(selectedText))
            {
                ButtonSayEnabled = false;
                return false;
            }
            else
            {
                ButtonSayEnabled = true;
                return true;
            }
        }

        

        private void CheckCommandAction(object o)
        {

        }

        private bool CanCheckCommandAction(object o)
        {
            return true;
        }
    }
}
