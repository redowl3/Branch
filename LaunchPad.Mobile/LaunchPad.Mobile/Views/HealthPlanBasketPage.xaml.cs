
using FormsControls.Base;
using LaunchPad.Mobile.Services;
using LaunchPad.Mobile.ViewModels;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LaunchPad.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HealthPlanBasketPage : AnimationPage
    {
        public HealthPlanBasketPage()
        {
            InitializeComponent();
            HealthPlanBasketViewModel.BadgeCountAction += AddOrUpdateBadge;
        }
        private void AddOrUpdateBadge(int obj)
        {
            if (ToolbarItems.Count > 0)
                DependencyService.Get<IToolbarItemBadgeService>().SetBadge(this, ToolbarItems.First(), $"{obj}", Color.White, Color.Black);
        }
    }
}