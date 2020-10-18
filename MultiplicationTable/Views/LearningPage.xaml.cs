using MultiplicationTable.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MultiplicationTable.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LearningPage : ContentPage
    {
        private LearningViewModel viewModel;
        public LearningPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new LearningViewModel();
        }
    }
}