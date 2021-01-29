using FormsControls.Base;
using LaunchPad.Mobile.Helpers;
using LaunchPad.Mobile.Services;
using LaunchPad.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LaunchPad.Mobile.ViewModels
{
    public class LifestylesSurveyViewModel : ViewModelBase
    {
        private int Counter { get; set; }
        private int MaxCounter { get; set; }
        private IDatabaseServices DatabaseServices => DependencyService.Get<IDatabaseServices>();
        private ObservableCollection<IndexedQuestions> _lifeStylesQuestions = new ObservableCollection<IndexedQuestions>();
        public ObservableCollection<IndexedQuestions> LifeStylesQuestions
        {
            get => _lifeStylesQuestions;
            set => SetProperty(ref _lifeStylesQuestions, value);
        }

        private FlexBasis _basis = new FlexBasis(1f, true);
        public FlexBasis Basis
        {
            get => _basis;
            set => SetProperty(ref _basis, value);
        }


        private List<IndexModel> _surveyIndexList = new List<IndexModel>();
        public List<IndexModel> SurveyIndexList
        {
            get => _surveyIndexList;
            set => SetProperty(ref _surveyIndexList, value);
        }
        private bool _isDone;
        public bool IsDone
        {
            get => _isDone;
            set => SetProperty(ref _isDone, value);
        }
        public ICommand NextCommand => new Command(async() =>
        {
            try
            {
                if (Counter < MaxCounter)
                {
                    LifeStylesQuestions = new ObservableCollection<IndexedQuestions>();
                    ++Counter;
                    foreach (var survey in App.surveyPageViewModelInstance.SurveyCollection.Where(a => a.Form.Title.ToLower() == "you + your lifestyle"))
                    {
                        foreach (var page in survey.Form.Pages.Skip(Counter).Take(1))
                        {
                            var questions = await DatabaseServices.Get<List<CustomFormQuestion>>("survey_page" + page.Id + "_" + survey.Form.Id);

                            LifeStylesQuestions.Add(new IndexedQuestions
                            {
                                SurveyGuid = survey.Form.Id,
                                PageGuid = page.Id,
                                Questions = new ObservableCollection<CustomFormQuestion>(questions)
                            });
                        }
                    }
                    if (LifeStylesQuestions[0].Questions?.Count == 3)
                    {
                        Basis = new FlexBasis(0.333f, true);
                    }
                    else if (LifeStylesQuestions[0].Questions?.Count == 2)
                    {
                        Basis = new FlexBasis(0.5f, true);
                    }
                    else if (LifeStylesQuestions[0].Questions?.Count == 1)
                    {
                        Basis = new FlexBasis(1f, true);
                    }
                    LifeStylesQuestions[0].IsSelected = true;
                }
                else
                {
                    await SecureStorage.SetAsync("SurveyDone_" + Settings.ClientId + "_" + Settings.CurrentTherapistId, "true");
                   
                    Task.Run(() =>
                    {
                        App.ConcernsAndSkinCareSurveyViewModel = new ConcernsAndSkinCareSurveyViewModel();
                        App.HealthQuestionsSurveyViewModel = new HealthQuestionsSurveyViewModel();
                        App.LifestylesSurveyViewModel = new LifestylesSurveyViewModel();

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            Application.Current.MainPage = new AnimationNavigationPage(new SalonProductsPage());
                        });
                    });
                }
            }
            catch (Exception)
            {

            }
        });
        public LifestylesSurveyViewModel()
        {
            GetConcernsQuestionsAsync();
        }
        public void GetConcernsQuestionsAsync()
        {
            try
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (LifeStylesQuestions != null || LifeStylesQuestions.Count == 0)
                    {
                        foreach (var survey in App.surveyPageViewModelInstance.SurveyCollection.Where(a => a.Form.Title.ToLower() == "you + your lifestyle"))
                        {
                            foreach (var page in survey.Form.Pages.Take(1))
                            {
                                var questions = await DatabaseServices.Get<List<CustomFormQuestion>>("survey_page" + page.Id + "_" + survey.Form.Id);

                                LifeStylesQuestions.Add(new IndexedQuestions
                                {
                                    SurveyGuid = survey.Form.Id,
                                    PageGuid = page.Id,
                                    Questions = new ObservableCollection<CustomFormQuestion>(questions)
                                });
                            }
                            Counter = 0;
                            MaxCounter = survey.Form.Pages.Count - 1;
                        }
                      
                    }

                    LifeStylesQuestions[0].IsSelected = true;
                  
                });
            }
            catch (Exception)
            {

            }
        }
    }
}
