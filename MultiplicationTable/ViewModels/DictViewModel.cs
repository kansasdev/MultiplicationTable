﻿using System;
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
using System.Collections.ObjectModel;

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

        private WordDetails wordDetails;       

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

            wordDetails = new WordDetails();
            wordDetails.TypingWordFinished += Wp_TypingWordFinished;
            wordDetails.TypingCancelled += Wp_TypingCancelled;

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
                Task.Run(() =>
                {
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
                        if (xDocUser != null && xDocUser.Descendants().Count() >= 1)
                        {
                            IEnumerable<XElement> deUser = from elUser in xDocUser.Descendants("Text") select elUser;
                            foreach (XElement elCurrent in deUser)
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

                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            foreach (string word in wordsAndPunctation)
                            {
                                if (word.Contains("Ó") || word.Contains("ó") || word.Contains("U") || word.Contains("u") || word.Contains("rz") || word.Contains("Rz") ||
                                    word.Contains("Ż") || word.Contains("ż") || word.Contains("h") || word.Contains("H") || word.Contains("Ch") || word.Contains("ch")
                                    )
                                {
                                    SayCommand.ChangeCanExecute();
                                    SpecialWords sw = new SpecialWords(word);

                            string text = string.Empty;
                            if(Device.RuntimePlatform == Device.Android)
                            {
                                
                                text = Environment.NewLine+ sw.GetDashedWord() + " ";
                            }
                            else
                            {
                                text = sw.GetDashedWord() + " ";
                            }

                                    Span s = new Span() { Text = text, TextColor = Color.Red, ForegroundColor = Color.FromHex("999999") };
                                    TapGestureRecognizer tgr = new TapGestureRecognizer();

                                    tgr.NumberOfTapsRequired = 1;
                                    int temp = indexSpecialWords;
                                    tgr.CommandParameter = temp;
                                    
                                    //tgr.Command = new Command(WordTapped);
                                    tgr.Tapped += Tgr_Tapped;
                                    s.GestureRecognizers.Add(tgr);
                                    FText.Spans.Add(s);
                                    
                                    sw.NumberWrongWordElement = indexSpecialWords;
                                    sw.DashedWord = s.Text;
                                    lstSpecialWords.Add(sw);
                                    indexSpecialWords++;

                                    sw.NumberAllWordsElement = indexAllWords;
                                    lstWords.Add(sw);
                                
                                    indexAllWords++;
                                }
                                else
                                {
                                    if(Device.RuntimePlatform == Device.Android)
                                    {
                                        FText.Spans.Add(new Span() { Text = " "+word + " ", ForegroundColor = Color.FromHex("999999") });

                                    }
                                    else
                                    {
                                         FText.Spans.Add(new Span() { Text = word + " ", ForegroundColor = Color.FromHex("999999") });

                                     }

                            indexAllWords++;
                                }
                            }
                       
                        });
                    }
                    catch (Exception ex)
                    {
                        UserDialogs.Instance.Alert(Language.txtErrorMessage + ex.Message, Language.txtErrorTitle, Language.txtErrorBtn);
                    }
                    finally
                    {
                        SetWaiting(false);
                    }
                });
                
            }
        }
                
        private void Tgr_Tapped(object sender, EventArgs e)
        {
            if (lstSpecialWords != null)
            {
                int specialWordTapped = (int)((Xamarin.Forms.TappedEventArgs)e).Parameter;
                if (Device.RuntimePlatform == Device.Android)
                {
                    SpecialWords sw = lstSpecialWords[specialWordTapped];
                                         

                        sw.SetLetterPlaces();
                    
                     if (Application.Current.MainPage.Navigation.ModalStack.Count() == 0)
                    {
                        wordDetails.InitializeLayout(sw);
                        Application.Current.MainPage.Navigation.PushModalAsync(wordDetails);
                    }                   
                    
                }
                else
                {


                    SpecialWords sw = lstSpecialWords[specialWordTapped];
                   
                    sw.SetLetterPlaces();
                    wordDetails.InitializeLayout(sw);
                    Application.Current.MainPage.Navigation.PushModalAsync(wordDetails);
                                        
                }
            }
        }

       

        private void Wp_TypingCancelled(SpecialWords obj)
        {
        }

        private void Wp_TypingWordFinished(SpecialWords obj)
        {
            

            string SpanText = FText.Spans[obj.NumberAllWordsElement].Text;
                      
                if(lstAnsweredWords==null)
                {
                    lstAnsweredWords = new List<SpecialWords>();
                }

           
            if (lstAnsweredWords.Where(q => q.NumberWrongWordElement == obj.NumberWrongWordElement).FirstOrDefault() == null)
            {
                lstAnsweredWords.Add(obj);
            }
            lstAnsweredWords.Where(q=>q.NumberWrongWordElement==obj.NumberWrongWordElement).FirstOrDefault().UserTappedWord = obj.UserTappedWord;
            FText.Spans[obj.NumberAllWordsElement].Text = obj.UserTappedWord;
                           
        }

        private void SayItCommandAction(object o)
        {
            Task.Run(() =>
            {
                ButtonEnabled = false;
                IEnumerable<Locale> locals = TextToSpeech.GetLocalesAsync().GetAwaiter().GetResult();
                Locale local = locals.FirstOrDefault(y => string.Equals(y.Country, "POL"));
                if(local==null)
                {
                    local = locals.FirstOrDefault(y => string.Equals(y.Country, "USA"));
                }

                    var mySpeechOptions = new SpeechOptions
                {
                    Volume = 1,
                    Pitch = 0.2f,
                    Locale = local
                };
                Task.WaitAll(TextToSpeech.SpeakAsync(selectedText,mySpeechOptions));
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
                            FText.Spans[sw.NumberAllWordsElement].FontAttributes = FontAttributes.None;
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
