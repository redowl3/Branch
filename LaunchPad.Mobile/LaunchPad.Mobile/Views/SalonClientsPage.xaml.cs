using FormsControls.Base;
using IIAADataModels.Transfer;
using LaunchPad.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LaunchPad.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SalonClientsPage : AnimationPage
    {
        public SalonClientsPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        private void item_tapped(object sender, System.EventArgs e)
        {
            try
            {
                var param = (e as TappedEventArgs)?.Parameter as Consumer;
                if (param != null)
                {
                    (this.BindingContext as SalonClientsPageViewModel)?.SelectConsumerCommand.Execute(param);
                }
            }
            catch (System.Exception)
            {

            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            (this.BindingContext as SalonClientsPageViewModel)?.RefreshCommand.Execute(null);
        }
    }
}