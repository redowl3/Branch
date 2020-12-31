using FormsControls.Base;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LaunchPad.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashPage : AnimationPage
    {
        public SplashPage()
        {
            InitializeComponent();
        }
    }
}