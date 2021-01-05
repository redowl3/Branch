using IIAADataModels.Transfer;
using LaunchPad.Client;
using LaunchPad.Mobile.Models;
using LaunchPad.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace LaunchPad.Mobile.ViewModels
{
    public class CompletedHealthPlanPageViewModel:ViewModelBase
    {
        public static Action<int> BadgeCountAction;
        public static void OnBadgeCountAction(int param)
        {
            if (BadgeCountAction != null)
            {
                BadgeCountAction(param);
            }
        }
        private IDatabaseServices DatabaseServices => DependencyService.Get<IDatabaseServices>();
        private Salon Salon = new Salon();
        private ObservableCollection<CompletedHealthPlan> _completedHealthPlansCollection = new ObservableCollection<CompletedHealthPlan>();
        public ObservableCollection<CompletedHealthPlan> CompletedHealthPlansCollection
        {
            get => _completedHealthPlansCollection;
            set => SetProperty(ref _completedHealthPlansCollection, value);
        }
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }
        private string _amountPaid="0.00";
        public string AmountPaid
        {
            get => _amountPaid;
            set => SetProperty(ref _amountPaid, value);
        } 
        
        private string _amountToBePaid="0.00";
        public string AmountToBePaid
        {
            get => _amountToBePaid;
            set => SetProperty(ref _amountToBePaid, value);
        }

        private string _totalLoyaltyPoints;
        public string TotalLoyaltyPoints
        {
            get => _totalLoyaltyPoints;
            set => SetProperty(ref _totalLoyaltyPoints, value);
        }

        private ObservableCollection<StarRatings> _starRatingsCollection=new ObservableCollection<StarRatings>();
        public ObservableCollection<StarRatings> StarRatingsCollection
        {
            get => _starRatingsCollection;
            set => SetProperty(ref _starRatingsCollection, value);
        }
        private bool _isContentVisible = false;
        public bool IsContentVisible
        {
            get => _isContentVisible;
            set => SetProperty(ref _isContentVisible, value);
        }
        public CompletedHealthPlanPageViewModel()
        {
             GetItemsAsync();
        }
        private async void GetItemsAsync()
        {
            IsBusy = true;
            await Task.Delay(1000);
            IsContentVisible = true;
            Device.BeginInvokeOnMainThread(() => ExceptionHandler(async () =>
                {
                    var basket = await DatabaseServices.Get<CustomBasket>("basketItems");
                   
                    var salon = await DatabaseServices.Get<Salon>("salon");
                    if (basket != null && basket.ItemsCollection.Count > 0)
                    {                        
                        AmountToBePaid = $"{basket.ItemsCollection.Sum(a => a.Variant.Price).ToString("F2")}";
                        BadgeCountAction?.Invoke(basket.ItemsCollection.Count);
                        foreach (var item in basket.Basket.Items)
                        {
                            foreach (var productCategory in salon.ProductCategories)
                            {
                                var products = productCategory.Products.Where(a => a.Id == item.ProductId).ToList();
                                if (products.Count > 0)
                                {
                                    foreach (var product in products)
                                    {
                                        var completedHealthPlan = new CompletedHealthPlan();
                                        completedHealthPlan.CloseOtherScanExceptthisCommand = new Command<Product>((param) => CloseOtherScanExceptthis(param));
                                        completedHealthPlan.ProductScannedCommand = new Command<ZXing.Result>((param) => AddLoyalityPoints(param));
                                        completedHealthPlan.RemoveLoyaltyCommand = new Command<Product>((param) => RemoveLoyalityPoints(param));
                                        completedHealthPlan.ProgramName = productCategory.Subtitle;
                                        completedHealthPlan.Product = product;
                                        completedHealthPlan.VariantsList = new ObservableCollection<ProductVariant>();
                                        completedHealthPlan.ShouldShowVariant = completedHealthPlan.IsDropdownVisible = product.Variants?.Count > 0;
                                        foreach (var variant in product.Variants)
                                        {
                                            completedHealthPlan.VariantsList.Add(variant);
                                        }
                                        completedHealthPlan.SelectedVariant = product.Variants.FirstOrDefault(a => a.Id == item.VariantId);
                                        completedHealthPlan.ShouldShowSubVariant = completedHealthPlan.SelectedVariant?.PrescribingOptions?.Count > 0;
                                        if (completedHealthPlan.ShouldShowSubVariant)
                                        {
                                            completedHealthPlan.PrescribingOptions = new ObservableCollection<ProductVariantPrescribingOption>();
                                            foreach (var option in completedHealthPlan.SelectedVariant.PrescribingOptions)
                                            {
                                                completedHealthPlan.PrescribingOptions.Add(option);
                                            }
                                            completedHealthPlan.SelectedOption = completedHealthPlan.PrescribingOptions.First(a => a.Title.ToLower() == item.PrescribingOption.ToLower());
                                        }

                                        if (product.ImageUrls?.Count > 0)
                                        {
                                            completedHealthPlan.ImageUrl = new Uri(product.ImageUrls.FirstOrDefault());
                                        }

                                        CompletedHealthPlansCollection.Add(completedHealthPlan);
                                    }
                                }
                            }
                        }

                        foreach (var item in CompletedHealthPlansCollection.Where(a => a.IsProductScanned))
                        {
                            StarRatingsCollection.Add(new StarRatings
                            {
                                Star = "star.png",
                                WidthRequest = 14,
                                HeightRequest = 14,
                                Margin=new Thickness(0,-2,0,0)
                            });
                        }
                        foreach (var item in CompletedHealthPlansCollection.Where(a => !a.IsProductScanned))
                        {
                            StarRatingsCollection.Add(new StarRatings
                            {
                                Star = "icon_white_circle.png",
                                WidthRequest = 8,
                                HeightRequest = 8,
                                Margin=new Thickness(0,0,0,0)
                            });
                        }
                    }
                }));
        }

        private void RemoveLoyalityPoints(Product param)
        {
            Device.BeginInvokeOnMainThread(() => ExceptionHandler(async() =>
            {
                var basket = await DatabaseServices.Get<CustomBasket>("basketItems");
                CompletedHealthPlansCollection.Where(a => a.Product.Id == param.Id).ForEach(x =>
                {
                    var selectedVariant = basket.Basket.Items.First(t => t.ProductId == param.Id).VariantId;
                    var variant = _selectedProduct.Variants.First(temp => temp.Id == selectedVariant);
                    x.LoyalityPoints = 0;
                    x.IsProductScanned = false;
                });
                var sumOfLoyaltyPoits = CompletedHealthPlansCollection.Sum(a => a.LoyalityPoints);

                TotalLoyaltyPoints =sumOfLoyaltyPoits==0?"": $"+ {sumOfLoyaltyPoits}";
                StarRatingsCollection = new ObservableCollection<StarRatings>();
                foreach (var item in CompletedHealthPlansCollection.Where(a => a.IsProductScanned))
                {
                    StarRatingsCollection.Add(new StarRatings
                    {
                        Star = "star.png",
                        WidthRequest = 14,
                        HeightRequest = 14,
                        Margin = new Thickness(0, -2, 0, 0)
                    });
                }
                foreach (var item in CompletedHealthPlansCollection.Where(a => !a.IsProductScanned))
                {
                    StarRatingsCollection.Add(new StarRatings
                    {
                        Star = "icon_white_circle.png",
                        WidthRequest = 8,
                        HeightRequest = 8,
                        Margin = new Thickness(0, 0, 0, 0)
                    });
                }
            }));
        }

        private async void AddLoyalityPoints(ZXing.Result param)
        {
            var basket = await DatabaseServices.Get<CustomBasket>("basketItems");
            Device.BeginInvokeOnMainThread(() => ExceptionHandler(() =>
            {
                CompletedHealthPlansCollection.Where(a => a.Product.Id== basket.Basket.Items.First(x=>x.ProductId==_selectedProduct.Id).ProductId).ForEach(x =>
                {
                    var selectedVariant = basket.Basket.Items.First(t => t.ProductId == _selectedProduct.Id).VariantId;
                    var variant = _selectedProduct.Variants.First(temp => temp.Id == selectedVariant);
                    x.LoyalityPoints = variant.LoyaltyPoints;                   
                });
                TotalLoyaltyPoints = $"+ {CompletedHealthPlansCollection.Sum(a => a.LoyalityPoints)}";
                StarRatingsCollection = new ObservableCollection<StarRatings>();
                foreach (var item in CompletedHealthPlansCollection.Where(a => a.IsProductScanned))
                {
                    StarRatingsCollection.Add(new StarRatings
                    {
                        Star = "star.png",
                        WidthRequest = 14,
                        HeightRequest = 14,
                        Margin = new Thickness(0, -2, 0, 0)
                    });
                }
                foreach (var item in CompletedHealthPlansCollection.Where(a => !a.IsProductScanned))
                {
                    StarRatingsCollection.Add(new StarRatings
                    {
                        Star = "icon_white_circle.png",
                        WidthRequest = 8,
                        HeightRequest = 8,
                        Margin = new Thickness(0, 0, 0, 0)
                    });
                }
            }));
        }

        private Product _selectedProduct=new Product();
        private void CloseOtherScanExceptthis(Product param)
        {
            _selectedProduct = param;
            Device.BeginInvokeOnMainThread(() => ExceptionHandler(() =>
            {
                CompletedHealthPlansCollection.Where(a => a.Product.Id != param.Id).ForEach(x => x.IsScanning = false);
            }));
        }

        private void ExceptionHandler(Action action)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
