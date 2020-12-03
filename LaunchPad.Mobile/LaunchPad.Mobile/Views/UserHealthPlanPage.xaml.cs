using IIAADataModels.Transfer;
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
            Products = new List<Product>
            {
                new Product
                {
                    Id=Guid.NewGuid(),
                    Name="Skin Accumax &trade;",
                    Summary="A nutritional supplement which works from within for clear, flawless skin.",
                    ImageUrls=new List<string>
                    {
                        "https://via.placeholder.com/150"
                    }
                }
            };
        }

        private void fortify_tapped(object sender, System.EventArgs e)
        {
            FortifyTab.BackgroundColor = Color.White;
            FeedTab.BackgroundColor = FinishTab.BackgroundColor = Color.FromHex("#bdbdbd");
        }

        private void finish_tapped(object sender, System.EventArgs e)
        {
            FinishTab.BackgroundColor = Color.White;
            FortifyTab.BackgroundColor = FeedTab.BackgroundColor = Color.FromHex("#bdbdbd");
        }
    }
}