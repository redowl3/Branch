using IIAADataModels.Transfer;
using LaunchPad.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LaunchPad.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserHealthPlanPage : ContentPage
    {
        public List<Product> Products = new List<Product>();
        public UserHealthPlanPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            FeedTab.BackgroundColor = Color.White;
            FortifyTab.BackgroundColor = FinishTab.BackgroundColor = Color.FromHex("#bdbdbd");
            
        }
        private void Feed_tapped(object sender, System.EventArgs e)
        {
            FeedTab.BackgroundColor = Color.White;
            FortifyTab.BackgroundColor = FinishTab.BackgroundColor = Color.FromHex("#bdbdbd");
            (this.BindingContext as UserHealthPlanPageViewModel)?.FeedCommand.Execute(null);
        }

        private void fortify_tapped(object sender, System.EventArgs e)
        {
            FortifyTab.BackgroundColor = Color.White;
            FeedTab.BackgroundColor = FinishTab.BackgroundColor = Color.FromHex("#bdbdbd");
            (this.BindingContext as UserHealthPlanPageViewModel)?.FortifyCommand.Execute(null);
        }

        private void finish_tapped(object sender, System.EventArgs e)
        {
            FinishTab.BackgroundColor = Color.White;
            FortifyTab.BackgroundColor = FeedTab.BackgroundColor = Color.FromHex("#bdbdbd");
            (this.BindingContext as UserHealthPlanPageViewModel)?.FinishCommand.Execute(null);
        }
    }
}