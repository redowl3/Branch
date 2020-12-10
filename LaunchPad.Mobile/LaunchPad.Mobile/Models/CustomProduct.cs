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

        public CustomProductAdditionalInfo SelectedAdditionalInfo { get; set; }

        public CustomProduct()
        {
            AdditionalInformations = new List<CustomProductAdditionalInfo>();
            SelectedAdditionalInfo = new CustomProductAdditionalInfo();
        }
    }

    public class CustomProductAdditionalInfo : INotifyPropertyChanged
    {
        public string Id { get; set; }
        public ProductAdditionalInformation AdditionalInformation { get; set; }

        private bool _isSelected=false;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public Color BackgroundColor { get; set; }

        public ICommand ViewCommand => new Command<CustomProductAdditionalInfo>((param) =>
          {
              IsSelected = !IsSelected;
              ItemSelectedCommand.Execute(param);
          });
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

        public CustomProductAdditionalInfo()
        {
            Id = Guid.NewGuid().ToString();
        }
    }

    public class FilterOption : INotifyPropertyChanged
    {
        public string Id { get; set; }
        public string Option { get; set; }
        private bool _isSelected = false;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        private Color _backgroundColor;
        public Color BackgroundColor
        {
            get => _backgroundColor;
            set => SetProperty(ref _backgroundColor, value);
        }

        public ICommand SelectCommand => new Command<CustomProductAdditionalInfo>((param) =>
        {
            IsSelected = !IsSelected;
            if (IsSelected)
            {
                BackgroundColor = Color.Black;
            }
            else
            {
                BackgroundColor = Color.Transparent;
            }
        });

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

        public FilterOption()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
