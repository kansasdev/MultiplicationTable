using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MultiplicationTable.Services;
using MultiplicationTable.Views;
using System.Threading;
using System.Globalization;
using MultiplicationTable.Resx;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

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

    class BindableStackLayout : StackLayout
    {
        public static readonly BindableProperty ItemsProperty =
            BindableProperty.Create(nameof(Items), typeof(ObservableCollection<View>), typeof(BindableStackLayout), null,
                propertyChanged: (b, o, n) =>
                {
                    (n as ObservableCollection<View>).CollectionChanged += (coll, arg) =>
                    {
                        switch (arg.Action)
                        {
                            case NotifyCollectionChangedAction.Add:
                                foreach (var v in arg.NewItems)
                                    (b as BindableStackLayout).Children.Add((View)v);
                                break;
                            case NotifyCollectionChangedAction.Remove:
                                foreach (var v in arg.NewItems)
                                    (b as BindableStackLayout).Children.Remove((View)v);
                                break;
                            case NotifyCollectionChangedAction.Move:
                            //Do your stuff
                            break;
                            case NotifyCollectionChangedAction.Replace:
                            //Do your stuff
                            break;
                        }
                    };
                });


        public ObservableCollection<View> Items
        {
            get { return (ObservableCollection<View>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }
    }
}
