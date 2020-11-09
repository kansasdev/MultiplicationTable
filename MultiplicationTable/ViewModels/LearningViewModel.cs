using Acr.UserDialogs;
using MultiplicationTable.Models;
using MultiplicationTable.Resx;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Linq;
using MultiplicationTable.Services;
using PCLStorage;

namespace MultiplicationTable.ViewModels
{
    

    public class LearningViewModel : BaseViewModel
    {
        public ICommand HintACommand { protected set; get; }
        public ICommand HintBCommand { protected set; get; }
        public ICommand HintCCommand { protected set; get; }

        public ICommand ShuffleCommand { protected set; get; }
        public ICommand SayCommand { protected set; get; }
        public ICommand CheckCommand { protected set; get; }

        private ObservableCollection<string> categoriesSource;
        public ObservableCollection<string> CategoriesSource
        {
            get { return categoriesSource; }
            set { SetProperty(ref categoriesSource, value); }
        }

        private ObservableCollection<LearningWord> learningWords;
        public ObservableCollection<LearningWord> LearningWords
        {
            get { return learningWords; }
            set
            {
                SetProperty(ref learningWords, value);
            }
        }

        private string selectedCategory;
        public string SelectedCategory
        {
            get
            {
                return selectedCategory;
            }
            set
            {
                SetProperty(ref selectedCategory, value);
            }
        }

        private string properCorrespondingWord;
        private string firstWrongCorrespondingWord;
        private string secondWrongCorrespondingWord;
        private string generatedWord;
        public string GeneratedWord
        {
            get { return generatedWord; }
            set
            {
                SetProperty(ref generatedWord, value);
            }
        }

        private string hintA;
        public string HintA
        {
            get { return hintA; }
            set { SetProperty(ref hintA, value); }
        }

        private string hintB;
        public string HintB
        {
            get { return hintB; }
            set { SetProperty(ref hintB, value); }
        }

        private string hintC;
        public string HintC
        {
            get { return hintC; }
            set { SetProperty(ref hintC, value); }
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

        private bool buttonEnabled;
        public bool ButtonEnabled
        {
            get { return buttonEnabled; }
            set
            {
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

        private ImageSource imageEmbeddedSource;
        public ImageSource ImageEmbeddedSource
        {
            get
            {
                return imageEmbeddedSource;
            }
            set
            {
                SetProperty(ref imageEmbeddedSource, value);
            }
        }

       
        private bool imageVisible;
        public bool ImageVisible
        {
            get
            {
                return imageVisible;

            }
            set
            {
                SetProperty(ref imageVisible, value);
            }
        }

        public LearningViewModel()
        {
            Title = "";
            EquationResults.Resest();

            ShuffleCommand = new Command(new Action<object>(ShuffleCommandAction));
            SayCommand = new Command(new Action<object>(SayCommandAction));
            CheckCommand = new Command(new Action<object>(CheckCommandAction));
            HintACommand = new Command(new Action<object>(CheckHintAAction));
            HintBCommand = new Command(new Action<object>(CheckHintBAction));
            HintCCommand = new Command(new Action<object>(CheckHintCAction));

            ReadDataFromSource();
        }

        private void ShuffleCommandAction(object o)
        {
            ImageVisible = false;
             if(!string.IsNullOrEmpty(SelectedCategory)&&LearningWords!=null)
             {
                List<LearningWord> lstLw = LearningWords.Where(q => q.PolishCategory == SelectedCategory).ToList();
                List<LearningWord> lstBadLw = new List<LearningWord>(lstLw);
                Random r = new Random();
                int idx = r.Next(0, lstLw.Count());
                LearningWord lw = lstLw.ElementAt(idx);

                GeneratedWord = lw.Polish;
                properCorrespondingWord = lw.English;

                lstBadLw.RemoveAt(idx);

                int firstWrongIdx = r.Next(0, lstBadLw.Count());
                firstWrongCorrespondingWord = lstBadLw[firstWrongIdx].English;

                lstBadLw.RemoveAt(firstWrongIdx);

                int secondWrongIdx = r.Next(0, lstBadLw.Count());
                secondWrongCorrespondingWord = lstBadLw[secondWrongIdx].English;
                

                //ustawienie buttonow;
                int okIdx = r.Next(0, 3);
                if(okIdx==0)
                {
                    HintA = properCorrespondingWord;
                    HintC = secondWrongCorrespondingWord;
                    HintB = firstWrongCorrespondingWord;
                }
                if(okIdx==1)
                {
                    HintB = properCorrespondingWord;
                    HintA = firstWrongCorrespondingWord;
                    HintC = secondWrongCorrespondingWord;
                }
                if(okIdx==2)
                {
                    HintC = properCorrespondingWord;
                    HintA = firstWrongCorrespondingWord;
                    HintB = secondWrongCorrespondingWord;
                }

              
             }
             else
             {
                UserDialogs.Instance.Alert(Language.txtLearningSetCategory, Language.txtErrorTitle);
             }
        }

        private void CheckHintAAction(object o)
        {
            if (!string.IsNullOrEmpty(GeneratedWord))
            {
                if (HintA == properCorrespondingWord)
                {
                    EquationResults.AddOkAnswer();
                    ImageVisible = true;
                    ImageEmbeddedSource = ImageSource.FromResource("MultiplicationTable.thumbs_up_1.png", typeof(MultiplicationTable.ImageResourceExtension).GetTypeInfo().Assembly);

                }
                else
                {
                    EquationResults.AddBadAnswer();
                    ImageVisible = true;
                    ImageEmbeddedSource = ImageSource.FromResource("MultiplicationTable.waaa_1.png", typeof(MultiplicationTable.ImageResourceExtension).GetTypeInfo().Assembly);
                }
                Title = Language.txtMultResult + ", " + Language.txtMultOk + ": " + EquationResults.GetOkAnswers() + " " + Language.txtMultNo + ": " + EquationResults.GetBadAnswers() + " " + Language.txtMultTotal + ": " + EquationResults.GetTotalAnswers();
            }
        }

        private void CheckHintBAction(object o)
        {
            if (!string.IsNullOrEmpty(GeneratedWord))
            {
                if (HintB == properCorrespondingWord)
                {
                    EquationResults.AddOkAnswer();
                    ImageVisible = true;
                    ImageEmbeddedSource = ImageSource.FromResource("MultiplicationTable.thumbs_up_1.png", typeof(MultiplicationTable.ImageResourceExtension).GetTypeInfo().Assembly);
                }
                else
                {
                    EquationResults.AddBadAnswer();
                    ImageVisible = true;
                    ImageEmbeddedSource = ImageSource.FromResource("MultiplicationTable.waaa_1.png", typeof(MultiplicationTable.ImageResourceExtension).GetTypeInfo().Assembly);
                }
                Title = Language.txtMultResult + ", " + Language.txtMultOk + ": " + EquationResults.GetOkAnswers() + " " + Language.txtMultNo + ": " + EquationResults.GetBadAnswers() + " " + Language.txtMultTotal + ": " + EquationResults.GetTotalAnswers();
            }
        }

        private void CheckHintCAction(object o)
        {
            if (!string.IsNullOrEmpty(GeneratedWord))
            {
                if (HintC == properCorrespondingWord)
                {
                    EquationResults.AddOkAnswer();
                    ImageVisible = true;
                    ImageEmbeddedSource = ImageSource.FromResource("MultiplicationTable.thumbs_up_1.png", typeof(MultiplicationTable.ImageResourceExtension).GetTypeInfo().Assembly);

                }
                else
                {
                    EquationResults.AddBadAnswer();
                    ImageVisible = true;
                    ImageEmbeddedSource = ImageSource.FromResource("MultiplicationTable.waaa_1.png", typeof(MultiplicationTable.ImageResourceExtension).GetTypeInfo().Assembly);
                }
                Title = Language.txtMultResult + ", " + Language.txtMultOk + ": " + EquationResults.GetOkAnswers() + " " + Language.txtMultNo + ": " + EquationResults.GetBadAnswers() + " " + Language.txtMultTotal + ": " + EquationResults.GetTotalAnswers();
            }
        }

        private void SayCommandAction(object o)
        {
            if(!String.IsNullOrEmpty(GeneratedWord))
            {
                Task.Run(new Action(SayAction));
            }
        }

        private void CheckCommandAction(object o)
        {

        }

        private void SayAction()
        {
            SetFormState(false);
            TextToSpeech.SpeakAsync(GeneratedWord).GetAwaiter().GetResult();
            SetFormState(true);
        }

        private void CheckCommandAction()
        {

        }

        private void SetFormState(bool state)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                WaitIndicator = !state;
                ButtonEnabled = state;
                ButtonSayEnabled = state;
            });
        }

        private void ReadDataFromSource()
        {
            Task.Run(() =>
            {
                try
                {
                    SetFormState(false);
                    var assembly = IntrospectionExtensions.GetTypeInfo(typeof(DictViewModel)).Assembly;
                    Stream stream = assembly.GetManifestResourceStream("MultiplicationTable.LanguageWords.xml");
                    string txtXml = string.Empty;
                    using (var reader = new System.IO.StreamReader(stream))
                    {
                        txtXml = reader.ReadToEnd();
                    }
                    XDocument xDoc = XDocument.Parse(txtXml);
                    CategoriesSource = new ObservableCollection<string>();
                    LearningWords = new ObservableCollection<LearningWord>();

                    IEnumerable<XElement> de =
                    from el in xDoc.Descendants()
                    select el;
                    foreach (XElement el in de)
                    {
                        if(el.Name=="Word")
                        {
                            IEnumerable<XElement> desc =
                            from elDesc in el.Descendants()
                            select elDesc;

                            LearningWord lw = new LearningWord();
                            foreach(XElement elDescendant in desc)
                            {
                                if(elDescendant.Name=="en")
                                {
                                    lw.English = elDescendant.Value;
                                }
                                if(elDescendant.Name=="pl")
                                {
                                    lw.Polish = elDescendant.Value;
                                }   
                                if(elDescendant.Name=="category_pl")
                                {
                                    if(CategoriesSource.Where(q=>q==elDescendant.Value).FirstOrDefault()==null)
                                    {
                                        MainThread.BeginInvokeOnMainThread(() =>
                                        {
                                            //UWAGA -> na androidzie działa to inaczej !!, to jest wykonywane PO FAKCIE
                                            if (!CategoriesSource.Contains(elDescendant.Value))
                                            {
                                                CategoriesSource.Add(elDescendant.Value);
                                            }
                                        });
                                    }
                                    lw.PolishCategory = elDescendant.Value;
                                }
                                if(elDescendant.Name=="category_en")
                                {
                                    lw.EnglishCategory = elDescendant.Value;
                                }
                            }
                            MainThread.BeginInvokeOnMainThread(() =>
                            {
                                LearningWords.Add(lw);
                            });
                        }
                    }
                    //LOAD USER DATA XML
                    IFolder folder = PCLStorage.FileSystem.Current.LocalStorage;
                    if (PCLHelper.IsFolderExistAsync("UserXML", folder).GetAwaiter().GetResult())
                    {
                        IFolder destFolder = folder.GetFolderAsync("UserXML").GetAwaiter().GetResult();
                        if (PCLHelper.IsFileExistAsync("UserWord.xml", destFolder).GetAwaiter().GetResult())
                        {
                            IFile file = destFolder.GetFileAsync("UserDict.xml").GetAwaiter().GetResult();
                            string content = file.ReadAllTextAsync().GetAwaiter().GetResult();
                            XDocument xDocUser = XDocument.Parse(content);

                            IEnumerable<XElement> deUser =
                            from el in xDocUser.Descendants()
                            select el;
                            foreach (XElement el in deUser)
                            {
                                if (el.Name == "Word")
                                {
                                    IEnumerable<XElement> desc =
                                    from elDesc in el.Descendants()
                                    select elDesc;

                                    LearningWord lw = new LearningWord();
                                    foreach (XElement elDescendant in desc)
                                    {
                                        if (elDescendant.Name == "en")
                                        {
                                            lw.English = elDescendant.Value;
                                        }
                                        if (elDescendant.Name == "pl")
                                        {
                                            lw.Polish = elDescendant.Value;
                                        }
                                        if (elDescendant.Name == "category_pl")
                                        {
                                            if (!CategoriesSource.Contains(elDescendant.Value))
                                            {
                                                MainThread.BeginInvokeOnMainThread(() =>
                                                {
                                                    if (!CategoriesSource.Contains(elDescendant.Value))
                                                    {

                                                        CategoriesSource.Add(elDescendant.Value);
                                                    }
                                                });
                                            }
                                            lw.PolishCategory = elDescendant.Value;
                                        }
                                        if (elDescendant.Name == "category_en")
                                        {
                                            lw.EnglishCategory = elDescendant.Value;
                                        }
                                    }
                                    MainThread.BeginInvokeOnMainThread(() =>
                                    {
                                        LearningWords.Add(lw);
                                    });
                                }
                            }
                        }
                    }

                    SetFormState(true);
                }
                catch(Exception ex)
                {
                    UserDialogs.Instance.Alert(Language.txtErrorMessage + ex.Message, Language.txtErrorTitle);
                }
            });
        }

    }
}
