using IIAADataModels.Transfer;
using LaunchPad.Client;
using LaunchPad.Mobile.Helpers;
using LaunchPad.Mobile.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LaunchPad.Mobile.ViewModels
{
    public class ClientRegistrationPageViewModel : ViewModelBase
    {
        private IDatabaseServices DatabaseServices => DependencyService.Get<IDatabaseServices>();
        private IToastServices ToastServices => DependencyService.Get<IToastServices>();
        private string _firstname;
        public string Firstname
        {
            get => _firstname;
            set
            {
                SetProperty(ref _firstname, value);
                CanExecute();
            }
        }
        private string _lastname;
        public string Lastname
        {
            get => _lastname;
            set
            {
                SetProperty(ref _lastname, value);
                CanExecute();
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                SetProperty(ref _email, value);
                CanExecute();
            }
        }

        private string _confirmEmail;
        public string ConfirmEmail
        {
            get => _confirmEmail;
            set
            {
                SetProperty(ref _confirmEmail, value);
                CanExecute();
            }
        }

        private string _mobile;
        public string Mobile
        {
            get => _mobile;
            set
            {
                SetProperty(ref _mobile, value);
                CanExecute();
            }
        }

        private string _addressLine1;
        public string AddressLine1
        {
            get => _addressLine1;
            set
            {
                SetProperty(ref _addressLine1, value);
                CanExecute();
            }
        }
        private string _addressLine2;
        public string AddressLine2
        {
            get => _addressLine2;
            set
            {
                SetProperty(ref _addressLine2, value);
                CanExecute();
            }
        }
        private string _addressLine3;
        public string AddressLine3
        {
            get => _addressLine3;
            set
            {
                SetProperty(ref _addressLine3, value);
                CanExecute();
            }
        }
        private string _city;
        public string City
        {
            get => _city;
            set
            {
                SetProperty(ref _city, value);
                CanExecute();
            }
        }

        private string _county;
        public string County
        {
            get => _county;
            set
            {
                SetProperty(ref _county, value);
                CanExecute();
            }
        }

        private string _country;
        public string Country
        {
            get => _country;
            set
            {
                SetProperty(ref _country, value);
                CanExecute();
            }
        }

        private string _dd;
        public string DD
        {
            get => _dd;
            set
            {
                SetProperty(ref _dd, value);
                CanExecute();
            }
        }

        private string _mm;
        public string MM
        {
            get => _mm;
            set
            {
                SetProperty(ref _mm, value);
                CanExecute();
            }
        }

        private string _yy;
        public string YY
        {
            get => _yy;
            set
            {
                SetProperty(ref _yy, value);
                CanExecute();
            }
        }
        private string _postCode;
        public string PostCode
        {
            get => _postCode;
            set
            {
                SetProperty(ref _postCode, value);
                CanExecute();
            }
        }

        private List<string> _iddCodes;
        public List<string> IddCodes
        {
            get => _iddCodes;
            set
            {
                SetProperty(ref _iddCodes, value);
                CanExecute();
            }
        }

        private string _selectedIddCode;
        public string SelectedIddCode
        {
            get => _selectedIddCode;
            set
            {
                SetProperty(ref _selectedIddCode, value);
                CanExecute();
            }
        }

        private List<string> _countries;
        public List<string> Countries
        {
            get => _countries;
            set
            {
                SetProperty(ref _countries, value);
                CanExecute();
            }
        }
        public ICommand AddClientCommand => new Command(AddClientAsync);

        private void CanExecute()
        {
            var shouldEnabled = !string.IsNullOrEmpty(Firstname) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(ConfirmEmail) && !string.IsNullOrEmpty(Mobile) && !string.IsNullOrEmpty(PostCode) && !string.IsNullOrEmpty(AddressLine1)
                  && !string.IsNullOrEmpty(AddressLine2) && !string.IsNullOrEmpty(AddressLine3) && !string.IsNullOrEmpty(City) && !string.IsNullOrEmpty(County) && !string.IsNullOrEmpty(County);
            IsButtonEnabled = shouldEnabled;
        }

        private bool _isButtonEnabled;
        public bool IsButtonEnabled
        {
            get => _isButtonEnabled;
            set => SetProperty(ref _isButtonEnabled, value);
        }

        public ClientRegistrationPageViewModel()
        {
            Countries = new List<string>
            {
                "United Kingdom",
                "United States",
                "India"
            };
            Country = Countries[0];
            IddCodes = new List<string>
            {
                "+1",
                "+44",
                "+91"
            };

            SelectedIddCode = IddCodes[0];
        }
        private async void AddClientAsync(object obj)
        {
            try
            {
                var currentTherapistJson = await SecureStorage.GetAsync("currentTherapist");
                var currentTherapist = JsonConvert.DeserializeObject<Therapist>(currentTherapistJson);

                var consumerRequest = new SalonConsumer
                {
                    Id = Guid.NewGuid(),
                    Firstname = Firstname,
                    Lastname = Lastname,
                    Email = Email,
                    Mobile = $"{SelectedIddCode}-{Mobile}",
                    DateOfBirth = new DateTime(int.Parse(YY), int.Parse(MM), int.Parse(DD)),
                    TherapistId = currentTherapist.Id,
                    Addresses = new List<ConsumerAddress>
                    {
                        new ConsumerAddress
                        {
                            Id=Guid.NewGuid(),
                            Address1=AddressLine1,
                            Address2=AddressLine2,
                            Address3=AddressLine3,
                            City=City,
                            Country=Country,
                            County=County,
                            Postcode=PostCode,
                            ConsumerId=Guid.NewGuid()
                        }
                    }
                };

                var isCompleted = await ApiServices.Client.PostAsync<bool>("Salon/Consumer", consumerRequest);
                if (isCompleted)
                {
                    var consumers = await ApiServices.Client.GetAsync<List<Consumer>>("Salon/Consumers");
                    if (consumers?.Count > 0)
                    {
                        var isSaved = await DatabaseServices.InsertData("consumers", consumers);
                        if (isSaved)
                        {
                            ToastServices.ShowToast("Consumer has been added successfully");
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                Application.Current.MainPage.Navigation.PopAsync();
                            });
                        }

                    }
                }
                else
                {
                    ToastServices.ShowToast("Consumer registration failed");
                }
            }
            catch (Exception)
            {
                ToastServices.ShowToast("Something went wrong.Please try again");
            }
        }

    }
}
