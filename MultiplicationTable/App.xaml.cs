using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MultiplicationTable.Services;
using MultiplicationTable.Views;
using System.Threading;
using System.Globalization;
using MultiplicationTable.Resx;

namespace MultiplicationTable
{
    public partial class App : Application
    {
       
        public App()
        {
            InitializeComponent();

           

            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
