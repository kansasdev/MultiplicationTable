using MultiplicationTable.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;

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

        }


    }
}
