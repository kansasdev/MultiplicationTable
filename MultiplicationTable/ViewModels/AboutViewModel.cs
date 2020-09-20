﻿using MultiplicationTable.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MultiplicationTable.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "Settings";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://github.com/kansasdev"));

            TimeoutSource = new ObservableCollection<int>();
            for(int i=1;i<=60;i++)
            {
                TimeoutSource.Add(i);
            }
            SumSource = new ObservableCollection<int>();
            for (int i = 1; i <= 50; i++)
            {
                SumSource.Add(i);
            }
            DiffSource = new ObservableCollection<int>();
            for (int i = 1; i <= 50; i++)
            {
                DiffSource.Add(i);
            }
            MultSource = new ObservableCollection<int>();
            for (int i = 1; i <= 10; i++)
            {
                MultSource.Add(i);
            }

            if (Settings.WorkMode=="" || Settings.WorkMode=="Normal")
            {
                Marked = false;
            }
            else
            {
                Marked = true;
            }

            SelectedDiff = Settings.DiffMax;
            SelectedMult = Settings.MultMax;
            SelectedSum = Settings.SumMax;
            SelectedTimeout = Settings.Timeout;

            SaveSettings = new Command(() =>
            {
                if(Marked)
                {
                    Settings.WorkMode = "Quiz";
                }
                else
                {
                    Settings.WorkMode = "Normal";
                }
                Settings.Timeout = SelectedTimeout;
                Settings.SumMax = SelectedSum;
                Settings.DiffMax = SelectedDiff;
                Settings.MultMax = SelectedMult;

              
                
            });
        }

        private bool marked;
        public bool Marked
        {
            get
            {
                return marked;
            }
            set
            {
                SetProperty(ref marked, value);
            }
        }

        private ObservableCollection<int> timeoutSource;
        public ObservableCollection<int> TimeoutSource
        {
            get
            {
                return timeoutSource;
            }
            set
            {
                SetProperty(ref timeoutSource, value);
            }
        }

        private int selectedTimeout;
        public int SelectedTimeout
        {
            get
            {
                return selectedTimeout;
            }
            set
            {
                SetProperty(ref selectedTimeout, value);
            }
        }

        private ObservableCollection<int> multSource;
        public ObservableCollection<int> MultSource
        {
            get
            {
                return multSource;
            }
            set
            {
                SetProperty(ref multSource, value);
            }
        }

        private int selectedMult;
        public int SelectedMult
        {
            get
            {
                return selectedMult;
            }
            set
            {
                SetProperty(ref selectedMult, value);
            }
        }

        private ObservableCollection<int> diffSource;
        public ObservableCollection<int> DiffSource
        {
            get
            {
                return diffSource;
            }
            set
            {
                SetProperty(ref diffSource, value);
            }
        }

        private int selectedDiff;
        public int SelectedDiff
        {
            get
            {
                return selectedDiff;
            }
            set
            {
                SetProperty(ref selectedDiff, value);
            }
        }

        private ObservableCollection<int> sumSource;
        public ObservableCollection<int> SumSource
        {
            get
            {
                return sumSource;
            }
            set
            {
                SetProperty(ref sumSource, value);
            }
        }

        private int selectedSum;
        public int SelectedSum
        {
            get
            {
                return selectedSum;
            }
            set
            {
                SetProperty(ref selectedSum, value);
            }
        }

        public ICommand OpenWebCommand { get; }
        public ICommand SaveSettings { get; }
    }
}