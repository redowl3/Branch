using FormsControls.Base;
using IIAADataModels.Transfer;
using LaunchPad.Mobile.Services;
using LaunchPad.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LaunchPad.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserHealthPlanPage : AnimationPage
    {
        public List<Product> Products = new List<Product>();
        public UserHealthPlanPage()
        {
            InitializeComponent();
            FeedTab.BackgroundColor = Color.White;
            FortifyTab.BackgroundColor = FinishTab.BackgroundColor = Color.FromHex("#bdbdbd");
            UserHealthPlanPageViewModel.CartItemAdded += AddOrUpdateBadge;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(1000);
            (this.BindingContext as UserHealthPlanPageViewModel)?.RefreshBadgeCountAsync();
        }

        private void AddOrUpdateBadge(int obj)
        {
            if (ToolbarItems.Count > 0)
                DependencyService.Get<IToolbarItemBadgeService>().SetBadge(this, ToolbarItems.First(), $"{obj}", Color.White, Color.Black);
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