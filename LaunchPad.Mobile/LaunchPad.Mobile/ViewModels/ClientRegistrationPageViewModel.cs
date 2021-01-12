using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace LaunchPad.Mobile.ViewModels
{
    public class ClientRegistrationPageViewModel:ViewModelBase
    {
        private string _firstname;
        public string Firstname
        {
            get => _firstname;
            set => SetProperty(ref _firstname, value);
        }
        
        private string _lastname;
        public string Lastname
        {
            get => _lastname;
            set => SetProperty(ref _lastname, value);
        }
        
        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        } 
        
        private string _mobile;
        public string Mobile
        {
            get => _mobile;
            set => SetProperty(ref _mobile, value);
        }
        
        private string _addressLine1;
        public string AddressLine1
        {
            get => _addressLine1;
            set => SetProperty(ref _addressLine1, value);
        } 
        private string _addressLine2;
        public string AddressLine2
        {
            get => _addressLine2;
            set => SetProperty(ref _addressLine2, value);
        }
        private string _addressLine3;
        public string AddressLine3
        {
            get => _addressLine3;
            set => SetProperty(ref _addressLine3, value);
        }
        private string _city;
        public string City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        } 
        
        private string _county;
        public string County
        {
            get => _county;
            set => SetProperty(ref _county, value);
        }
        
        private string _country;
        public string Country
        {
            get => _country;
            set => SetProperty(ref _country, value);
        }

        public ICommand AddClientCommand => new Command(() => AddClientAsync());

        public ClientRegistrationPageViewModel()
        {

        }
        private void AddClientAsync()
        {
            
        }

    }
}
