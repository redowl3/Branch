using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace LaunchPad.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinishFilterPage : ContentPage
    {
        public FinishFilterPage(ViewModels.UserHealthPlanPageViewModel userHealthPlanPageViewModel)
        {
            InitializeComponent();
            this.BindingContext = userHealthPlanPageViewModel;
        }

        private void CloseThis(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}