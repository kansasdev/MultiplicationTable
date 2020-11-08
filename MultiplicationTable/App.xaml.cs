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

    public class ExtLabel : Label
    {
        public static readonly BindableProperty FontNamedSizeProperty =
            BindableProperty.Create(
                "FontNamedSize", typeof(NamedSize), typeof(ExtLabel),
                defaultValue: default(NamedSize), propertyChanged: OnFontNamedSizeChanged);

        public NamedSize FontNamedSize
        {
            get { return (NamedSize)GetValue(FontNamedSizeProperty); }
            set { SetValue(FontNamedSizeProperty, value); }
        }

        private static void OnFontNamedSizeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((ExtLabel)bindable).OnFontNamedSizeChangedImpl((NamedSize)oldValue, (NamedSize)newValue);
        }

        protected virtual void OnFontNamedSizeChangedImpl(NamedSize oldValue, NamedSize newValue)
        {
            FontSize = Device.GetNamedSize(FontNamedSize, typeof(Label));
        }
    }

    public class ExtButton : Button
    {
        public static readonly BindableProperty FontNamedSizeProperty =
            BindableProperty.Create(
                "FontNamedSize", typeof(NamedSize), typeof(ExtButton),
                defaultValue: default(NamedSize), propertyChanged: OnFontNamedSizeChanged);

        public NamedSize FontNamedSize
        {
            get { return (NamedSize)GetValue(FontNamedSizeProperty); }
            set { SetValue(FontNamedSizeProperty, value); }
        }

        private static void OnFontNamedSizeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((ExtButton)bindable).OnFontNamedSizeChangedImpl((NamedSize)oldValue, (NamedSize)newValue);
        }

        protected virtual void OnFontNamedSizeChangedImpl(NamedSize oldValue, NamedSize newValue)
        {
            FontSize = Device.GetNamedSize(FontNamedSize, typeof(Label));
        }
    }
}
