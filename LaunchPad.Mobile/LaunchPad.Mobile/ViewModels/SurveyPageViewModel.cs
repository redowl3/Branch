using FormsControls.Base;
using IIAADataModels.Transfer;
using IIAADataModels.Transfer.Survey;
using LaunchPad.Client;
using LaunchPad.Mobile.Helpers;
using LaunchPad.Mobile.Services;
using LaunchPad.Mobile.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace LaunchPad.Mobile.ViewModels
{
    public class SurveyPageViewModel : ViewModelBase
    {
        private IDatabaseServices DatabaseServices => DependencyService.Get<IDatabaseServices>();
        private ObservableCollection<Form> _surveyCollection;
        public ObservableCollection<Form> SurveyCollection
        {
            get => _surveyCollection;
            set => SetProperty(ref _surveyCollection, value);
        }

        private ObservableCollection<CustomFormPage> _pages;
        public ObservableCollection<CustomFormPage> Pages
        {
            get => _pages;
            set => SetProperty(ref _pages, value);
        }

        private ObservableCollection<CustomFormQuestion> _questions;
        public ObservableCollection<CustomFormQuestion> Questions
        {
            get => _questions;
            set => SetProperty(ref _questions, value);
        }
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }
        private bool _isContentLoading=true;
        public bool IsContentLoading
        {
            get => _isContentLoading;
            set => SetProperty(ref _isContentLoading, value);
        }
        public ICommand FinishCommand => new Command(() =>
        {
            Application.Current.MainPage = new AnimationNavigationPage(new SalonProductsPage());
        });
        public ICommand GoBackCommand => new Command(() =>
        {
            try
            {
                Task.Run(() =>
                {
                    SecureStorage.RemoveAll();
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Application.Current.MainPage = new AnimationNavigationPage(new SalonClientsPage());
                    });
                });

            }
            catch (Exception)
            {
            }
        });
        public ICommand HomeCommand => new Command(() =>
        {
            try
            {
                Task.Run(() =>
                {
                    SecureStorage.RemoveAll();
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Application.Current.MainPage = new AnimationNavigationPage(new SalonClientsPage());
                    });
                });

            }
            catch (Exception)
            {
            }
        });
        public ICommand SignOutCommand => new Command(() =>
        {
            try
            {
                Task.Run(() =>
                {
                    SecureStorage.RemoveAll();
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Application.Current.MainPage = new AnimationNavigationPage(new SignInPage());
                    });
                });

            }
            catch (Exception)
            {
            }
        });
        public SurveyPageViewModel()
        {
            GetSurveyDataAsync();
        }
        private async void GetSurveyDataAsync()
        {
            IsBusy = true;
            try
            {
                var salon = await DatabaseServices.Get<Salon>("salon");
                if (salon == null || salon.Id == Guid.Empty)
                {
                    salon = await ApiServices.Client.GetAsync<Salon>("salon");
                    await DatabaseServices.InsertData("salon", salon);
                }

                if (salon.Surveys?.Count > 0)
                {
                    SurveyCollection = new ObservableCollection<Form>(salon.Surveys);
                    Pages = new ObservableCollection<CustomFormPage>();
                    foreach (var item in salon.Surveys)
                    {
                        foreach (var page in item.Pages)
                        {
                            Pages.Add(new CustomFormPage
                            {
                                Id = page.Id,
                                Title = page.Title,
                                Questions = new List<FormQuestion>(page.Questions),
                                PageSelectedCommand = new Command<CustomFormPage>((param) =>
                                  {
                                      DisplaySurveyQuestionsAsync(param);
                                  })
                            });
                        }
                    }

                    if (Pages.Count > 0)
                    {
                        Pages[0].SelectedCommand.Execute(Pages[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            IsContentLoading = false;
        }

        private void DisplaySurveyQuestionsAsync(CustomFormPage param)
        {
            try
            {
                if (param != null)
                {
                    Questions = new ObservableCollection<CustomFormQuestion>(param.Questions.Select(a => new CustomFormQuestion
                    {
                        FormQuestion = a,
                        FormQuestionData = JsonConvert.DeserializeObject<FormQuestionData>(a.QuestionData.Replace(@"\", "")),
                        IsCheckBox = a.QuestionType == "check",
                        IsRadio = a.QuestionType == "radio",
                        IsTextArea = a.QuestionType == "textarea",
                        IsTextBox = a.QuestionType == "textbox",
                        IsYesNo = a.QuestionType == "yesno"
                    }));

                    Pages.Where(a => a.Id != param.Id).ForEach(a => a.IsSelected = false);
                }
              
            }
            catch (Exception ex)
            {

            }
        }
    }

    public class CustomFormPage : ViewModelBase
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<FormQuestion> Questions { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public ICommand SelectedCommand => new Command<CustomFormPage>((param) =>
          {
              IsSelected = true;
              PageSelectedCommand.Execute(param);
          });

        public ICommand PageSelectedCommand { get; set; }
        public CustomFormPage()
        {
            Questions = new List<FormQuestion>();
        }
    }

    public class CustomFormQuestion:ViewModelBase
    {
        public FormQuestion FormQuestion { get; set; }
        public FormQuestionData FormQuestionData { get; set; }
        public bool IsTextBox { get; set; }
        public bool IsTextArea { get; set; }
        public bool IsRadio { get; set; }
        public bool IsCheckBox { get; set; }
        public bool IsYesNo { get; set; }
    }
    public class Answer
    {
        public string ResponseText { get; set; }
        public string ResponseValue { get; set; }
        public bool Selected { get; set; }
        public string Id { get; set; }
    }

    public class FormQuestionData
    {
        public string QuestionText { get; set; }
        public List<Answer> Answers { get; set; }

        public FormQuestionData()
        {
            Answers = new List<Answer>();
        }
    }
}
