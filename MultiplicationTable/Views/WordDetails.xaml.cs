using Acr.UserDialogs;
using MultiplicationTable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MultiplicationTable.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WordDetails : ContentPage
    {
        private SpecialWords _sw;

        public event Action<SpecialWords> TypingWordFinished;
        public event Action<SpecialWords> TypingCancelled;

        public WordDetails()
        {
            InitializeComponent();
           
        }

        public void InitializeLayout(SpecialWords sw)
        {
            _sw = sw;

            GenerateLayout(sw);
        }

        private void RemoveLayout()
        {
            List<View> lstItemsToRemove = new List<View>();
            foreach (var item in sLayout.Children)
            {
                if(item is Picker || item is Label)
                {
                    lstItemsToRemove.Add(item);
                }
            }
            foreach(var Item in lstItemsToRemove)
            {
                sLayout.Children.Remove(Item);
            }
               
        }

        private void GenerateLayout(SpecialWords sw)
        {
            string word = sw.ProperWord;
            int index = 0;
            
            foreach(Char c in word.ToCharArray())
            {
                if (sw.lstch.Count > 0 && sw.lstch.Contains(index))
                {
                    sLayout.Children.Add(GeneratePicker(new string[] { "h", "ch" }));
                }
                else if (sw.lstchLarge.Count > 0 && sw.lstchLarge.Contains(index))
                {
                    sLayout.Children.Add(GeneratePicker(new string[] { "H", "Ch" }));
                }
                else if (sw.lsth.Count > 0 && sw.lsth.Contains(index))
                {
                    if (index == 0 || (word[index-1]!='c' && word[index-1]!='C'))
                    {
                        sLayout.Children.Add(GeneratePicker(new string[] { "h", "ch" }));
                    }
                    
                }
                else if (sw.lsthLarge.Count > 0 && sw.lsthLarge.Contains(index))
                {
                    sLayout.Children.Add(GeneratePicker(new string[] { "H", "Ch" }));
                }
                else if (sw.lstrz.Count > 0 && sw.lstrz.Contains(index))
                {
                    sLayout.Children.Add(GeneratePicker(new string[] { "rz", "ż" }));
                }
                else if (sw.lstrzLarge.Count > 0 && sw.lstrzLarge.Contains(index))
                {
                    sLayout.Children.Add(GeneratePicker(new string[] { "Rz", "Ż" }));
                }
                else if (sw.lstu.Count > 0 && sw.lstu.Contains(index))
                {
                    sLayout.Children.Add(GeneratePicker(new string[] { "u", "ó" }));
                }
                else if (sw.lstuLarge.Count > 0 && sw.lstuLarge.Contains(index))
                {
                    sLayout.Children.Add(GeneratePicker(new string[] { "U", "Ó" }));
                }
                else if (sw.lstÓ.Count > 0 && sw.lstÓ.Contains(index))
                {
                    sLayout.Children.Add(GeneratePicker(new string[] { "ó", "u" }));
                }
                else if (sw.lstż.Count > 0 && sw.lstż.Contains(index))
                {
                    sLayout.Children.Add(GeneratePicker(new string[] { "ż", "rz" }));
                }
                else if (sw.lstżLarge.Count > 0 && sw.lstżLarge.Contains(index))
                {
                    sLayout.Children.Add(GeneratePicker(new string[] { "Ż", "Rz" }));
                }
                else
                {
                    if (c != 'z')
                    {
                        Label l = new Label();
                        l.Text = c.ToString();
                        l.FontSize = 20;
                        sLayout.Children.Add(l);
                    }
                    else
                    {
                        if(index>0)
                        {
                            if(word[index-1]!='r' && word[index-1]!='R')
                            {
                                Label l = new Label();
                                l.Text = c.ToString();
                                l.FontSize = 20;
                                
                                sLayout.Children.Add(l);
                            }
                        }
                        else
                        {
                            if (c=='z' || c=='Z')
                            {
                                Label l = new Label();
                                l.Text = c.ToString();
                                l.FontSize = 20;
                                sLayout.Children.Add(l);
                            }
                        }
                    }
                }
                index++;
            }
            
        }

        private Picker GeneratePicker(string[] options)
        {
            Picker p = new Picker();
            p.FontSize = 18;
            p.WidthRequest = 30;
            foreach(string o in options)
            {
                p.Items.Add(o);
            }
            return p;
        }

        private void OK_Clicked(object sender, EventArgs e)
        {
            bool error = false;
            _sw.UserTappedWord = string.Empty;
            foreach(View v in sLayout.Children)
            {
                if(v is Picker)
                {
                    Picker p = (Picker)v;
                    if(p.SelectedItem==null)
                    {
                        error = true;
                        break;
                    }
                }
            }

            if(error)
            {
                UserDialogs.Instance.Alert("Error","ERROR");
            }
            else
            {
                foreach(View v in sLayout.Children)
                {
                    if(v is Label)
                    {
                        Label l = (Label)v;
                        _sw.UserTappedWord = _sw.UserTappedWord + l.Text;
                    }
                    if(v is Picker)
                    {
                        Picker p = (Picker)v;
                        if (p.SelectedItem != null)
                        {
                            _sw.UserTappedWord = _sw.UserTappedWord + p.SelectedItem.ToString();
                        }
                    }
                                       
                }

                if (Device.RuntimePlatform == Device.Android)
                {
                    _sw.UserTappedWord = Environment.NewLine + _sw.UserTappedWord + " ";
                }
                else
                {
                    _sw.UserTappedWord = _sw.UserTappedWord + " ";
                }
                

                if (TypingWordFinished != null)
                {
                    RemoveLayout();
                    TypingWordFinished(_sw);
                    Application.Current.MainPage.Navigation.PopModalAsync(true);
                }
            }

        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            if (TypingCancelled != null)
            {
                RemoveLayout();
                Application.Current.MainPage.Navigation.PopModalAsync(true);
                TypingCancelled(_sw);
            }
        }

        private void Say_Clicked(object sender, EventArgs e)
        {
            btnCancel.IsEnabled = false;
            btnOk.IsEnabled = false;
            btnSay.IsEnabled = false;
            Task.Run(() =>
            {
                
                Task.WaitAll(TextToSpeech.SpeakAsync(_sw.ProperWord));
                
            });
            btnCancel.IsEnabled = true;
            btnOk.IsEnabled = true;
            btnSay.IsEnabled = true;
        }
    }
}