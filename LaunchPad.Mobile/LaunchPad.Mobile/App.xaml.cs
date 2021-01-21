﻿using FormsControls.Base;
using LaunchPad.Mobile.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
[assembly: ExportFont("UniNeueBook.ttf", Alias = "BoldFont")]
[assembly: ExportFont("UniNeueRegular.ttf", Alias = "RegularFont")]
namespace LaunchPad.Mobile
{
    public partial class App : Application
    {
        public static string UserName { get; set; }
        public static string SalonName { get; set; }
        public static Page CurrentPage { get; set; }
        public static Page NextPage { get; set; }
        public App()
        {
            InitializeComponent();
            Device.SetFlags(new string[] { "RadioButton_Experimental"});
            CurrentPage = new AnimationNavigationPage(new SplashPage());
            MainPage = CurrentPage;
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
