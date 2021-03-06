﻿using MultiplicationTable.Models;
using MultiplicationTable.Resx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MultiplicationTable.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Dictation,Title=Language.txtTitleDictation},
                new HomeMenuItem {Id = MenuItemType.Multiplication, Title=Language.txtTitleMathOps },
                new HomeMenuItem {Id=MenuItemType.EnglishWords,Title=Language.txtTitleWording},
                new HomeMenuItem {Id = MenuItemType.Configuration, Title=Language.txtTitleConfiguation }
            };

            ListViewMenu.ItemsSource = menuItems;
            ListViewMenu.SelectedItem = menuItems[0];

            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };

            
        }
    }
}