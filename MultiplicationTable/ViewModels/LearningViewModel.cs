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



        public LearningViewModel()
        {
            Title = "";
            
            ShuffleCommand = new Command(new Action<object>(ShuffleCommandAction));
            SayCommand = new Command(new Action<object>(SayCommandAction));
            CheckCommand = new Command(new Action<object>(CheckCommandAction));

            ReadDataFromSource();
        }

        private void ShuffleCommandAction(object o)
        {

        }

        private void SayCommandAction(object o)
        {
            if(String.IsNullOrEmpty(GeneratedWord))
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
                                    if(!CategoriesSource.Contains(elDescendant.Value))
                                    {
                                        MainThread.BeginInvokeOnMainThread(() =>
                                        {
                                            CategoriesSource.Add(elDescendant.Value);
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
