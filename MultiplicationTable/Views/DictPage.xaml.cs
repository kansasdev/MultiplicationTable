using MultiplicationTable.Resx;
using MultiplicationTable.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MultiplicationTable.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DictPage : ContentPage
    {
        private DictViewModel viewModel;
        public DictPage()
        {
            InitializeComponent();
               

            BindingContext = viewModel = new DictViewModel();
        }

       
    }
}