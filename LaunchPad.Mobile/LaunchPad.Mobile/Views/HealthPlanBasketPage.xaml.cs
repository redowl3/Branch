
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
        private bool isExpanded;
        public HealthPlanBasketPage()
        {
            InitializeComponent();
            isExpanded = true;
            HealthPlanBasketViewModel.BadgeCountAction += AddOrUpdateBadge;
        }
        private void AddOrUpdateBadge(int obj)
        {
            if (ToolbarItems.Count > 0)
                DependencyService.Get<IToolbarItemBadgeService>().SetBadge(this, ToolbarItems.First(), $"{obj}", Color.White, Color.Black);
        }
        void Handle_Tapped(object sender, System.EventArgs e)
        {
            if (!isExpanded)
                AnimateIn();
            else
                AnimateOut();

            isExpanded = !isExpanded;
        }
        private void AnimateIn()
        {
            var rect = new Rectangle(-(GridLayout.Width - Sidebar.Width), Sidebar.Y, Sidebar.Width, Sidebar.Height);
            Sidebar.LayoutTo(rect, 400, Easing.Linear);
        }
        private void AnimateOut()
        {
            var rect = new Rectangle(GridLayout.Width, Sidebar.Y, Sidebar.Width, Sidebar.Height);
            Sidebar.LayoutTo(rect, 400, Easing.Linear);
        }
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (isExpanded)
            {
                AnimateOut();
                isExpanded = false;
            }
        }
    }
}