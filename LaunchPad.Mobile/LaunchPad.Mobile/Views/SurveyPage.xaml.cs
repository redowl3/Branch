using FormsControls.Base;
using LaunchPad.Mobile.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LaunchPad.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SurveyPage : AnimationPage
    {
        private Color ActiveColor = Color.FromHex("#c7c9cb");
        private Color DisableColor = Color.FromHex("#f4f4f5");
        public static Action StartCounter;
        public static Action BackFromMedicalQuestionnare;
        public static void OnBackFromMedicalQuestionnare()
        {
            BackFromMedicalQuestionnare?.Invoke();
        }
        public static void OnStartCounter()
        {
            StartCounter?.Invoke();
        }
        public SurveyPage()
        {
            InitializeComponent();
            ConcernPageSurveyContainer.IsVisible = false;
            HealthQuestionSurveyContainer.IsVisible = true;
            LifestylesSurveyContainer.IsVisible = false;
            ConcernsAndSkinCareSurveyViewModel.Next += NextToConcernsPage;
            HealthQuestionsSurveyViewModel.Next += NextToHealthPage;
            LifestylesSurveyViewModel.Next += NextToLifeStyle;
        }

        private void GoBack()
        {
            if (LetsRecapSurveyContainer.IsVisible)
            {
                LifeStyleButtonClicked(null, null);
            }
            else if (LifestylesSurveyContainer.IsVisible)
            {
                ConcernsButtonClicked(null, null);
            }else if (ConcernPageSurveyContainer.IsVisible)
            {
                HealthButtonClicked(null, null);
            }
            else
            {
                BackFromMedicalQuestionnare.Invoke();
            }
        }

        private void NextToConcernsPage()
        {
            LifeStyleButtonClicked(null, null);
        }
        private void NextToHealthPage()
        {
            ConcernsButtonClicked(null, null);
        }
        private void NextToLifeStyle()
        {
            LetsRecapButtonClicked(null, null);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            SurveyPageViewModel.GoBack += GoBack;
            // App.surveyPageViewModelInstance?.SelectSurveyAsync();
        }
        public SurveyPage(PageAnimation pageAnimation)
        {
            PageAnimation = pageAnimation;
            InitializeComponent();
        }

        private void ConcernsButtonClicked(object sender, System.EventArgs e)
        {
            if (ConcernPageSurveyContainer.IsVisible)
            {
                StartCounter?.Invoke();
                return;
            }
            ConcernPageSurveyContainer.IsVisible = true;
            HealthQuestionSurveyContainer.IsVisible = false;
            LifestylesSurveyContainer.IsVisible = false;
            LetsRecapSurveyContainer.IsVisible = false;
            ConcernSurveyBoxView.BackgroundColor = ActiveColor;
            HealthSurveyBoxView.BackgroundColor = LifeStyleSurveyBoxView.BackgroundColor=LetsRecapSurveyContainer.BackgroundColor = DisableColor;
        }
        private void HealthButtonClicked(object sender, System.EventArgs e)
        {
            if (HealthQuestionSurveyContainer.IsVisible)
            {
                StartCounter?.Invoke();
                return;
            }
            ConcernPageSurveyContainer.IsVisible = false;
            HealthQuestionSurveyContainer.IsVisible = true;
            LifestylesSurveyContainer.IsVisible = false;
            LetsRecapSurveyContainer.IsVisible = false;
            HealthSurveyBoxView.BackgroundColor = ActiveColor;
            ConcernSurveyBoxView.BackgroundColor = LifeStyleSurveyBoxView.BackgroundColor=LetsRecapSurveyContainer.BackgroundColor = DisableColor;
        }
        private void LifeStyleButtonClicked(object sender, System.EventArgs e)
        {
            if (LifestylesSurveyContainer.IsVisible)
            {
                StartCounter?.Invoke();
                return;
            }
            ConcernPageSurveyContainer.IsVisible = false;
            HealthQuestionSurveyContainer.IsVisible = false;
            LifestylesSurveyContainer.IsVisible = true;
            LetsRecapSurveyContainer.IsVisible = false;
            LifeStyleSurveyBoxView.BackgroundColor = ActiveColor;
            ConcernSurveyBoxView.BackgroundColor = HealthSurveyBoxView.BackgroundColor=LetsRecapSurveyContainer.BackgroundColor = DisableColor;
        }

        private void NextButtonClicked(object sender, System.EventArgs e)
        {
            if (ConcernPageSurveyContainer.IsVisible)
            {
                //(ConcernPageSurveyContainer.BindingContext as ConcernsAndSkinCareSurveyViewModel)?.NextCommand.Execute(null);
            }
            if (HealthQuestionSurveyContainer.IsVisible)
            {
                (HealthQuestionSurveyContainer.BindingContext as HealthQuestionsSurveyViewModel)?.NextCommand.Execute(null);
            }
            if (LifestylesSurveyContainer.IsVisible)
            {
                (LifestylesSurveyContainer.BindingContext as LifestylesSurveyViewModel)?.NextCommand.Execute(null);
            }
            if (LetsRecapSurveyContainer.IsVisible)
            {
                (LetsRecapSurveyContainer.BindingContext as LifestylesSurveyViewModel)?.NextCommand.Execute(null);
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            SurveyPageViewModel.GoBack -= GoBack;
            Task.Run(() =>
            {
                App.ConcernsAndSkinCareSurveyViewModel = new ConcernsAndSkinCareSurveyViewModel();
                App.HealthQuestionsSurveyViewModel = new HealthQuestionsSurveyViewModel();
                App.LifestylesSurveyViewModel = new LifestylesSurveyViewModel();
            });
        }

        private void LetsRecapButtonClicked(object sender, EventArgs e)
        {
            if (LetsRecapSurveyContainer.IsVisible)
            {
                StartCounter?.Invoke();
                return;
            }
            ConcernPageSurveyContainer.IsVisible = false;
            HealthQuestionSurveyContainer.IsVisible = false;
            LifestylesSurveyContainer.IsVisible = false;
            LetsRecapSurveyContainer.IsVisible = true;
            LetsRecapSurveyBoxView.BackgroundColor = ActiveColor;
            ConcernSurveyBoxView.BackgroundColor = HealthSurveyBoxView.BackgroundColor=LifeStyleSurveyBoxView.BackgroundColor = DisableColor;
        }
    }
}