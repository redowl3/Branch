using FormsControls.Base;
using LaunchPad.Mobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace LaunchPad.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SurveyPage : AnimationPage
    {
        public SurveyPage()
        {
            InitializeComponent();
        }

        public SurveyPage(PageAnimation pageAnimation)
        {
            PageAnimation = pageAnimation;
            InitializeComponent();
        }
    }
}