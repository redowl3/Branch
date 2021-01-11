using LaunchPad.Mobile.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace LaunchPad.Mobile.CustomLayouts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TitleBarLayoutV1 : ContentView
    {
        public TitleBarLayoutV1()
        {
            InitializeComponent();
            LoggedInUserDetailLabel.Text = $"{Settings.CurrentUserName} | {Settings.SalonName}";
        }
    }
}