using FormsControls.Base;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace LaunchPad.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientRegistrationPage : AnimationPage
    {
        public ClientRegistrationPage()
        {
            InitializeComponent();
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var screenWidth = mainDisplayInfo.Width;
            if (screenWidth <= 2050)
            {
                MidDobContainerGrid.ColumnSpacing = 10;
            }
            else
            {
                MidDobContainerGrid.ColumnSpacing = 30;
            }
            this.FirstnameEntry.Focused += (s, e) => { SetLayoutPosition(onFocus: true); };
            this.FirstnameEntry.Unfocused += (s, e) => { SetLayoutPosition(onFocus: false); };
            this.LastnameEntry.Focused += (s, e) => { SetLayoutPosition(onFocus: true); };
            this.LastnameEntry.Unfocused += (s, e) => { SetLayoutPosition(onFocus: false); };
            this.EmailEntry.Focused += (s, e) => { SetLayoutPosition(onFocus: true); };
            this.EmailEntry.Unfocused += (s, e) => { SetLayoutPosition(onFocus: false); }; 
            this.ConfirmEmailEntry.Focused += (s, e) => { SetLayoutPosition(onFocus: true); };
            this.ConfirmEmailEntry.Unfocused += (s, e) => { SetLayoutPosition(onFocus: false); }; 
            this.DDEntry.Focused += (s, e) => { SetLayoutPosition(onFocus: true); };
            this.DDEntry.Unfocused += (s, e) => { SetLayoutPosition(onFocus: false); };
            this.MMEntry.Focused += (s, e) => { SetLayoutPosition(onFocus: true); };
            this.MMEntry.Unfocused += (s, e) => { SetLayoutPosition(onFocus: false); };
            this.YYEntry.Focused += (s, e) => { SetLayoutPosition(onFocus: true); };
            this.YYEntry.Unfocused += (s, e) => { SetLayoutPosition(onFocus: false); };
            this.MobileEntry.Focused += (s, e) => { SetLayoutPosition(onFocus: true); };
            this.MobileEntry.Unfocused += (s, e) => { SetLayoutPosition(onFocus: false); };
            this.PostcodeEntry.Focused += (s, e) => { SetLayoutPosition(onFocus: true); };
            this.PostcodeEntry.Unfocused += (s, e) => { SetLayoutPosition(onFocus: false); };
            this.AddressLine1Entry.Focused += (s, e) => { SetLayoutPosition(onFocus: true); };
            this.AddressLine1Entry.Unfocused += (s, e) => { SetLayoutPosition(onFocus: false); };
            this.AddressLine2Entry.Focused += (s, e) => { SetLayoutPosition(onFocus: true); };
            this.AddressLine2Entry.Unfocused += (s, e) => { SetLayoutPosition(onFocus: false); };
            this.AddressLine3Entry.Focused += (s, e) => { SetLayoutPosition(onFocus: true); };
            this.AddressLine3Entry.Unfocused += (s, e) => { SetLayoutPosition(onFocus: false); };
            this.CityEntry.Focused += (s, e) => { SetLayoutPosition(onFocus: true); };
            this.CityEntry.Unfocused += (s, e) => { SetLayoutPosition(onFocus: false); };
            this.CountyEntry.Focused += (s, e) => { SetLayoutPosition(onFocus: true); };
            this.CountyEntry.Unfocused += (s, e) => { SetLayoutPosition(onFocus: false); };
        }
        void SetLayoutPosition(bool onFocus)
        {
            if (onFocus)
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    this.CenteredStackLayout.TranslateTo(0, -100, 50);
                }
                else if (Device.RuntimePlatform == Device.Android)
                {
                    this.CenteredStackLayout.TranslateTo(0, -100, 50);
                }
            }
            else
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    this.CenteredStackLayout.TranslateTo(0, 0, 50);
                }
                else if (Device.RuntimePlatform == Device.Android)
                {
                    this.CenteredStackLayout.TranslateTo(0, 0, 50);
                }
            }
        }
    }
}