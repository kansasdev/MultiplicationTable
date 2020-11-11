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
using PCLStorage;
using MultiplicationTable.Services;
using MultiplicationTable.Resx;

namespace MultiplicationTable.ViewModels
{
    public class DictViewModel : BaseViewModel
    {
        public Command ShuffleCommand { get; }
        public Command SayCommand { get; }
        public Command CheckCommand { get; }

        private string txtXml;
        private XDocument xDoc;
        private XDocument xDocUser;
        private string selectedText;

        private List<SpecialWords> lstSpecialWords;

        private List<SpecialWords> lstAnsweredWords;

        private int dictsDone = 0;

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
                ButtonSayEnabled = !state;
           
        }

        public DictViewModel()
        {
            Title = Language.txtTitleDictation;
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

            //CHECK IF USER ADDED USER DEFINED DICTATIONS
            Task t = new Task(async () =>
            {
                try
                {
                    IFolder folder = PCLStorage.FileSystem.Current.LocalStorage;
                    if (await PCLHelper.IsFolderExistAsync("UserXML", folder))
                    {
                        IFolder destFolder = await folder.GetFolderAsync("UserXML");
                        if (await PCLHelper.IsFileExistAsync("UserDict.xml", destFolder))
                        {
                            IFile file = await destFolder.GetFileAsync("UserDict.xml");
                            string content = await file.ReadAllTextAsync();
                            if (!string.IsNullOrEmpty(content))
                            {
                                xDocUser = XDocument.Parse(content);
                            }

                        }
                    }
                }
                catch(Exception ex)
                {
                    UserDialogs.Instance.Alert(Language.txtErrLoadingUserData + " " + ex.Message, Language.btnOk);
                }
            });

            t.RunSynchronously();

        }

        private void ShuffleCommandAction()
        {
            if(xDoc!=null)
            {
                //Task.Run(() =>
                //{
                    SetWaiting(true);
                    try
                    {
                        if (lstAnsweredWords != null)
                        {
                            lstAnsweredWords.Clear();
                        }

                        List<XElement> lst = new List<XElement>();
                        IEnumerable<XElement> de =
                        from el in xDoc.Descendants()
                        select el;
                        foreach (XElement el in de)
                        {
                            lst.Add(el);
                        }
                        if(xDocUser!=null && xDocUser.Descendants().Count()>=1)
                        {
                            IEnumerable<XElement> deUser = from elUser in xDocUser.Descendants("Text") select elUser;
                            foreach(XElement elCurrent in deUser)
                            {
                                lst.Add(elCurrent);
                            }
                        }

                        Random r = new Random();
                        int gettedDict = r.Next(1, lst.Count);

                        selectedText = lst[gettedDict].Value;
                        List<string> wordsAndPunctation = selectedText.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();

                        FText = new FormattedString();
                        int indexSpecialWords = 0;
                        int indexAllWords = 0;
                        List<SpecialWords> lstWords = new List<SpecialWords>();
                        lstSpecialWords.Clear();
                        foreach (string word in wordsAndPunctation)
                        {
                            if (word.Contains("Ó") || word.Contains("ó") || word.Contains("U") || word.Contains("u") || word.Contains("rz") || word.Contains("Rz") ||
                                word.Contains("Ż") || word.Contains("ż") || word.Contains("h") || word.Contains("H") || word.Contains("Ch") || word.Contains("ch")
                                )
                            {
                                SpecialWords sw = new SpecialWords(word);
                                //MainThread.BeginInvokeOnMainThread(() =>
                                //{
                                    SayCommand.ChangeCanExecute();
                                                                      

                                    Span s = new Span() { Text = sw.GetDashedWord() + " ", TextColor = Color.Red,ForegroundColor = Color.FromHex("999999")};
                                    s.GestureRecognizers.Add(new TapGestureRecognizer()
                                    {
                                        NumberOfTapsRequired = 1,
                                        Command = new Command(WordTapped),
                                        //CommandParameter = indexSpecialWords
                                        CommandParameter = word
                                    });;
                                    
                                    FText.Spans.Add(s);
                                    sw.NumberWrongWordElement = indexSpecialWords;
                                    sw.DashedWord = s.Text;
                                    lstSpecialWords.Add(sw);
                                    indexSpecialWords++;
                                //});
                                sw.NumberAllWordsElement = indexAllWords;
                                lstWords.Add(sw);
                                indexAllWords++;
                                
                            }
                            else
                            {
                                //MainThread.BeginInvokeOnMainThread(() =>
                                //{
                                    FText.Spans.Add(new Span() { Text = word + " ", ForegroundColor = Color.FromHex("999999") });
                                    indexAllWords++;
                                //});
                            }
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        UserDialogs.Instance.Alert(Language.txtErrorMessage + ex.Message, Language.txtErrorTitle, Language.txtErrorBtn);
                    }
                    finally
                    {
                        SetWaiting(false);
                    }
                //});
                
            }
        }

        private void WordTapped(object o)
        {
            List<Span> lst = FText.Spans.ToList();
           string w = (string)o;
            /*if (lstSpecialWords != null)
            {
                SpecialWords sw = lstSpecialWords[(int)o];
                sw.SetLetterPlaces();

                WordDetails wp = new WordDetails(sw);
                wp.TypingWordFinished += Wp_TypingWordFinished;                
                Application.Current.MainPage.Navigation.PushModalAsync(wp);
            }*/
        }

        private void Wp_TypingWordFinished(SpecialWords obj)
        {
            string SpanText = FText.Spans[obj.NumberAllWordsElement].Text;
            
                if(lstAnsweredWords==null)
                {
                    lstAnsweredWords = new List<SpecialWords>();
                }
               
                if(lstAnsweredWords.Count<=obj.NumberWrongWordElement)
                {
                    lstAnsweredWords.Add(obj);
                }
                else
                {
                    lstAnsweredWords.ElementAt(obj.NumberWrongWordElement).UserTappedWord = obj.UserTappedWord;
                }
                FText.Spans[obj.NumberAllWordsElement].Text = obj.UserTappedWord;
           
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
            int poprawne = 0;
            if (lstAnsweredWords != null)
            {
                if (lstAnsweredWords.Count == lstSpecialWords.Count)
                {
                    foreach(SpecialWords sw in lstAnsweredWords)
                    {
                        if(sw.UserTappedWord.Trim().Equals(sw.ProperWord))
                        {
                            FText.Spans[sw.NumberAllWordsElement].TextColor = Color.Green;
                            FText.Spans[sw.NumberAllWordsElement].FontAttributes = FontAttributes.None;
                            poprawne++;
                        }
                        else
                        {
                            FText.Spans[sw.NumberAllWordsElement].TextColor = Color.Red;
                            FText.Spans[sw.NumberAllWordsElement].FontAttributes = FontAttributes.Bold;
                        }
                    }

                    if(poprawne==lstSpecialWords.Count())
                    {
                        UserDialogs.Instance.Alert(Language.txtDictSuccessMesssage, Language.txtDictSuccessTitle);
                        dictsDone++;
                        Title = "OK " + dictsDone.ToString();
                        ShuffleCommandAction();
                    }
                    else
                    {
                        UserDialogs.Instance.Alert(string.Format("{0} - {1}, {2}-{3}, {4}-{5}",Language.txtDictOk, poprawne.ToString(), Language.txtDictBad, (lstSpecialWords.Count() - poprawne).ToString(),Language.txtDictSum, lstSpecialWords.Count().ToString()), "RESULT");

                    }

                }
                else
                {
                    UserDialogs.Instance.Alert(Language.txtDictErrorNotAllAnsweres, Language.txtDictErrorNotAllAnswersTitle);
                }
            }
            else
            {
                UserDialogs.Instance.Alert(Language.txtDictErrorNotAllAnsweres, Language.txtDictErrorNotAllAnswersTitle);
            }
        }

        private bool CanCheckCommandAction(object o)
        {
            return true;
        }
    }
}
