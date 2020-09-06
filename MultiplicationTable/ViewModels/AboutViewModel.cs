using MultiplicationTable.Services;
using System;
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
            
            if(Settings.WorkMode=="" || Settings.WorkMode=="Normal")
            {
                Marked = false;
            }
            else
            {
                Marked = true;
            }

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

        public ICommand OpenWebCommand { get; }
        public ICommand SaveSettings { get; }
    }
}