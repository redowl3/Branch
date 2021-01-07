using FormsControls.Base;
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
        public static Action AppInSleepMode;
        public static void OnAppInSleepMode()
        {
            AppInSleepMode?.Invoke();
        }
        public App()
        {
            InitializeComponent();

            MainPage = new AnimationNavigationPage(new SplashPage())
            {
                BarBackgroundColor=Color.Black,
                BarTextColor=Color.White
            };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            AppInSleepMode?.Invoke();
        }

        protected override void OnResume()
        {
        }
    }
}
