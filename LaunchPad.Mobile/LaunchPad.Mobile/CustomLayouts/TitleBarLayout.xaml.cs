using LaunchPad.Mobile.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LaunchPad.Mobile.CustomLayouts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TitleBarLayout : ContentView
    {
        public TitleBarLayout()
        {
            InitializeComponent();
            LoggedInUserDetailLabel.Text = $"{Settings.CurrentUserName} | {Settings.SalonName}";
            PageHeaderTitle.Text = Settings.ClientHeader;
            CurrentClientName.Text = Settings.ClientName;
        }
    }
}