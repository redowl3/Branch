using IIAADataModels.Transfer;
using IIAADataModels.Transfer.Survey;
using LaunchPad.Client;
using LaunchPad.Mobile.Helpers;
using LaunchPad.Mobile.Services;
using LaunchPad.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace LaunchPad.Mobile.ViewModels
{
    public class LetsRecapSurveyPageViewModel:ViewModelBase
    {
        private IDatabaseServices DatabaseServices => DependencyService.Get<IDatabaseServices>();
        private IToastServices ToastServices => DependencyService.Get<IToastServices>();
        private bool _page1Visible;
        public bool Page1Visible
        {
            get => _page1Visible;
            set => SetProperty(ref _page1Visible, value);
        }
        private string _summaryText;
        public string SummaryText
        {
            get => _summaryText;
            set => SetProperty(ref _summaryText, value);
        }

        private bool _page2Visible;
        public bool Page2Visible
        {
            get => _page2Visible;
            set => SetProperty(ref _page2Visible, value);
        }
        public ICommand Page1CompletedCommand => new Command(() =>
          {
              Page1Visible = false;
              Page2Visible = true;
          }); 
        public ICommand Page2CompletedCommand => new Command(() =>
          {
              PostSurveyResponseAsync();
          });

        public LetsRecapSurveyPageViewModel()
        {
            Page1Visible = true;
            Page2Visible = false;
        }
        private async void PostSurveyResponseAsync()
        {
            try
            {
                var dbSurveyResponse = await DatabaseServices.Get<List<FormResponse>>("SurveyResponse");
                var consumer = await DatabaseServices.Get<Consumer>("current_consumer" + Settings.CurrentTherapistId);
                if (consumer != null && consumer.Id != Guid.Empty)
                {
                    var list = new List<FormResponse>();
                    foreach (var item in dbSurveyResponse)
                    {
                        if (list.Count(a => a.FormId == item.FormId) == 0)
                        {
                            var formResponse = new FormResponse
                            {
                                Id = Guid.NewGuid(),
                                Created = DateTime.Now,
                                Version = item.Version,
                                FormId = item.FormId
                            };
                            formResponse.Answers = new List<FormQuestionResponse>();
                            formResponse.Answers.AddRange(item.Answers);
                            list.Add(formResponse);
                        }
                        else
                        {
                            list.Where(a => a.FormId == item.FormId).ForEach(x => x.Answers.AddRange(item.Answers));
                        }
                    }
                    var saloConsumer = new SalonConsumer
                    {
                        Id = consumer.Id,
                        Firstname = consumer.Firstname,
                        Lastname = consumer.Lastname,
                        Email = consumer.Email,
                        Mobile = consumer.Mobile,
                        TherapistId = new Guid(Settings.CurrentTherapistId),
                        DateOfBirth = consumer.DateOfBirth,
                        CurrentConsultation = new Consultation
                        {
                            Id = Guid.NewGuid(),
                            SurveyResponses = new List<FormResponse>(list)
                        }
                    };

                    var isCompleted = await ApiServices.Client.PostAsync<bool>("salon/consumer/consultation", saloConsumer);
                    if (isCompleted)
                    {
                        await Task.Run(async () =>
                        {
                            await DatabaseServices.InsertData<List<FormResponse>>("survey_response_" + consumer.Id + "_" + Settings.CurrentTherapistId, list);
                            App.ConcernsAndSkinCareSurveyViewModel = new ConcernsAndSkinCareSurveyViewModel();
                            App.HealthQuestionsSurveyViewModel = new HealthQuestionsSurveyViewModel();
                            App.LifestylesSurveyViewModel = new LifestylesSurveyViewModel();
                            await SecureStorage.SetAsync("SurveyDone" + Settings.ClientId, "true");
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                Application.Current.MainPage.Navigation.PushAsync(new SalonProductsPage());
                                Application.Current.MainPage.Navigation.PopModalAsync();
                            });
                        });
                    }
                    else
                    {
                        ToastServices.ShowToast("Survey Post request failed");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
