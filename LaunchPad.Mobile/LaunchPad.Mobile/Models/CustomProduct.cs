using IIAADataModels.Transfer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace LaunchPad.Mobile.Models
{
    public class CustomProduct: INotifyPropertyChanged
    {
        public Product Product { get; set; }

        public Uri ImageUrl { get; set; }

        public List<CustomProductAdditionalInfo> AdditionalInformations { get; set; }

        public CustomProductAdditionalInfo SelectedAdditionalInfo { get; set; }

        public ICommand AddPlanCommand { get; set; }

        private ObservableCollection<ProductVariant> _variantsList;
        public ObservableCollection<ProductVariant> VariantsList
        {
            get => _variantsList;
            set => SetProperty(ref _variantsList, value);
        }

        private ProductVariant _selectedVariant;
        public ProductVariant SelectedVariant
        {
            get => _selectedVariant;
            set
            {
                SetProperty(ref _selectedVariant, value);
                if(SelectedVariant!=null)
                {
                    Price = $"£{SelectedVariant.Price.ToString("F2")}";
                    if (SelectedVariant.PrescribingOptions != null && SelectedVariant.PrescribingOptions.Count > 0)
                    {
                        ShouldShowSubVariant = true;
                        PrescribingOptions = new ObservableCollection<ProductVariantPrescribingOption>(SelectedVariant.PrescribingOptions);
                        SelectedOption = PrescribingOptions.FirstOrDefault();
                    }

                    RefreshPriceCommand?.Execute(null);
                }
            }
        }
        private ObservableCollection<ProductVariantPrescribingOption> _prescribingOptions;
        public ObservableCollection<ProductVariantPrescribingOption> PrescribingOptions
        {
            get => _prescribingOptions;
            set => SetProperty(ref _prescribingOptions, value);
        }

        private ProductVariantPrescribingOption _selectedOption;
        public ProductVariantPrescribingOption SelectedOption
        {
            get => _selectedOption;
            set => SetProperty(ref _selectedOption, value);
        }

        private string _price;
        public string Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        private bool _shouldShowSubVariant=false;
        public bool ShouldShowSubVariant
        {
            get => _shouldShowSubVariant;
            set => SetProperty(ref _shouldShowSubVariant, value);
        }
        
        private bool _shouldShowVariant=false;
        public bool ShouldShowVariant
        {
            get => _shouldShowVariant;
            set => SetProperty(ref _shouldShowVariant, value);
        }

        public ICommand RefreshPriceCommand { get; set; }
        public CustomProduct()
        {
            AdditionalInformations = new List<CustomProductAdditionalInfo>();
            SelectedAdditionalInfo = new CustomProductAdditionalInfo();
            VariantsList = new ObservableCollection<ProductVariant>();
            PrescribingOptions = new ObservableCollection<ProductVariantPrescribingOption>();
        }
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

    public class HealthPlan:INotifyPropertyChanged
    {
        public string ProgramName { get; set; }


        private ObservableCollection<ProductWithLevelType> _productWithLevelTypeList;
        public ObservableCollection<ProductWithLevelType> ProductWithLevelTypeList
        {
            get => _productWithLevelTypeList;
            set => SetProperty(ref _productWithLevelTypeList, value);
        }

        private bool _isSelected=false;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public Command ToggleCommand => new Command(() =>
          {
              IsSelected = !IsSelected;
          });
        public HealthPlan()
        {
            ProductWithLevelTypeList = new ObservableCollection<ProductWithLevelType>();
        }

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

    public class CustomVariant : INotifyPropertyChanged
    {
        private ProductVariant _variant;
        public ProductVariant Variant
        {
            get => _variant;
            set => SetProperty(ref _variant, value);
        }

        public Guid ProductId { get; set; }

        public bool IsSelected { get; set; }

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

    public class ProductWithLevelType :INotifyPropertyChanged
    {
        public string Classification { get; set; }
        public string ProgramName { get; set; }

        private ObservableCollection<CustomProduct> _products;
        public ObservableCollection<CustomProduct> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }

        public ProductWithLevelType()
        {
            Products = new ObservableCollection<CustomProduct>();
        }

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
