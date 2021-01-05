using FFImageLoading.Forms;
using FormsControls.Base;
using LaunchPad.Mobile.CustomLayouts;
using LaunchPad.Mobile.Models;
using LaunchPad.Mobile.Services;
using LaunchPad.Mobile.ViewModels;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace LaunchPad.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompletedHealthPlanPage : AnimationPage
    {
        public CompletedHealthPlanPage()
        {
            InitializeComponent();
            CompletedHealthPlanPageViewModel.BadgeCountAction += AddOrUpdateBadge;
        }

        private void AddOrUpdateBadge(int obj)
        {
            if (ToolbarItems.Count > 0)
                DependencyService.Get<IToolbarItemBadgeService>().SetBadge(this, ToolbarItems.First(), $"{obj}", Color.White, Color.Black);
        }

        private void scan_event(object sender, System.EventArgs e)
        {
            var senderElement = sender as CachedImage;
            var parent = senderElement.Parent as Grid;
            if (parent != null)
            {
                var scannerStack=parent.Children[1] as StackLayout;
                scannerStack.Children.Add(new ScannerViewLayout());
                var param = (e as TappedEventArgs).Parameter as CompletedHealthPlan;
                if (param != null)
                {
                    param.ScanCommand.Execute(param.Product);
                }
            }

        }
    }
}