using IIAADataModels.Transfer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace LaunchPad.Mobile.Models
{
    public class CustomProduct
    {
        public Product Product { get; set; }

        public Uri ImageUrl { get; set; }

        public List<CustomProductAdditionalInfo> AdditionalInformations { get; set; }

        public CustomProduct()
        {
            AdditionalInformations = new List<CustomProductAdditionalInfo>();
        }
    }

    public class CustomProductAdditionalInfo : INotifyPropertyChanged
    {
        public ProductAdditionalInformation AdditionalInformation { get; set; }

        private bool _isSelected=false;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public ICommand ItemSelectedCommand { get;set; }

        #region # INotifyPropertyChanged Impl #
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value,
                                      [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
