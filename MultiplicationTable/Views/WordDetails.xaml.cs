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
        
        public WordDetails(SpecialWords sw)
        {
            InitializeComponent();
            _sw = sw;
            
            GenerateLayout(sw);
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
                    else
                    {
                        
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
                    sLayout.Children.Add(GeneratePicker(new string[] { "Ó", "U" }));
                }
                else if (sw.lstż.Count > 0 && sw.lstż.Contains(index))
                {
                    sLayout.Children.Add(GeneratePicker(new string[] { "ż", "rz" }));
                }
                else
                {
                    Label l = new Label();
                    l.Text = c.ToString();
                    l.FontSize = 20;
                    sLayout.Children.Add(l);
                }
                index++;
            }
        }

        private Picker GeneratePicker(string[] options)
        {
            Picker p = new Picker();
            p.FontSize = 18;
            foreach(string o in options)
            {
                p.Items.Add(o);
            }
            return p;
        }

        private void OK_Clicked(object sender, EventArgs e)
        {
            bool error = false;
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
                if (TypingWordFinished != null)
                {
                    _sw.UserTappedWord.Trim();
                    TypingWordFinished(_sw);
                    Application.Current.MainPage.Navigation.PopModalAsync();
                }
            }

        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopModalAsync();
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