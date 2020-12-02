using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LaunchPad.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserHealthPlanPage : ContentPage
    {
        public UserHealthPlanPage()
        {
            InitializeComponent();
        }

        private void Feed_tapped(object sender, System.EventArgs e)
        {
            FeedTab.BackgroundColor = Color.White;
            FortifyTab.BackgroundColor = FinishTab.BackgroundColor = Color.FromHex("#a6a6a6");
        }

        private void fortify_tapped(object sender, System.EventArgs e)
        {
            FortifyTab.BackgroundColor = Color.White;
            FeedTab.BackgroundColor = FinishTab.BackgroundColor = Color.FromHex("#a6a6a6");
        }

        private void finish_tapped(object sender, System.EventArgs e)
        {
            FinishTab.BackgroundColor = Color.White;
            FortifyTab.BackgroundColor = FeedTab.BackgroundColor = Color.FromHex("#a6a6a6");
        }
    }
}