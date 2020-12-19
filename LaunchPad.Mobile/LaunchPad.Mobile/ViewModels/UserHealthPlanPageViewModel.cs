using IIAADataModels.Transfer;
using LaunchPad.Client;
using LaunchPad.Mobile.Models;
using LaunchPad.Mobile.Services;
using LaunchPad.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace LaunchPad.Mobile.ViewModels
{
    public class UserHealthPlanPageViewModel : ViewModelBase
    {
        private IDatabaseServices DatabaseServices => DependencyService.Get<IDatabaseServices>();
        public static Action<int> CartItemAdded;
        public static void OnCartItemAdded(int param)
        {
            if (CartItemAdded != null)
            {
                CartItemAdded(param);
            }
        }
        private Salon Salon = new Salon();
        private ObservableCollection<CustomProduct> _feedsEvenProducts;
        public ObservableCollection<CustomProduct> FeedsEvenProducts
        {
            get => _feedsEvenProducts;
            set => SetProperty(ref _feedsEvenProducts, value);
        }

        private ObservableCollection<CustomProduct> _feedsOddsProducts;
        public ObservableCollection<CustomProduct> FeedsOddsProducts
        {
            get => _feedsOddsProducts;
            set => SetProperty(ref _feedsOddsProducts, value);
        }
        private ObservableCollection<CustomProduct> _fortifyEvenProducts;
        public ObservableCollection<CustomProduct> FortifyEvenProducts
        {
            get => _fortifyEvenProducts;
            set => SetProperty(ref _fortifyEvenProducts, value);
        }
        private ObservableCollection<CustomProduct> _fortifyOddProducts;
        public ObservableCollection<CustomProduct> FortifyOddProducts
        {
            get => _fortifyOddProducts;
            set => SetProperty(ref _fortifyOddProducts, value);
        }
        private ObservableCollection<CustomProduct> _finishEvenProducts;
        public ObservableCollection<CustomProduct> FinishEvenProducts
        {
            get => _finishEvenProducts;
            set => SetProperty(ref _finishEvenProducts, value);
        }

        private ObservableCollection<CustomProduct> _finishOddProducts;
        public ObservableCollection<CustomProduct> FinishOddProducts
        {
            get => _finishOddProducts;
            set => SetProperty(ref _finishOddProducts, value);
        }

        private ObservableCollection<FilterOption> _feedfilterOptionList;
        public ObservableCollection<FilterOption> FeedFilterOptionList
        {
            get => _feedfilterOptionList;
            set => SetProperty(ref _feedfilterOptionList, value);
        }
        private ObservableCollection<FilterOption> _fortifyFilterOptionList;
        public ObservableCollection<FilterOption> FortifyFilterOptionList
        {
            get => _fortifyFilterOptionList;
            set => SetProperty(ref _fortifyFilterOptionList, value);
        }
        private ObservableCollection<FilterOption> _finishFilterOptionList;
        public ObservableCollection<FilterOption> FinishFilterOptionList
        {
            get => _finishFilterOptionList;
            set => SetProperty(ref _finishFilterOptionList, value);
        }
        private bool _feedContentVisible;
        public bool FeedContentVisible
        {
            get => _feedContentVisible;
            set => SetProperty(ref _feedContentVisible, value);
        }
        private bool _fortifyContentVisible;
        public bool FortifyContentVisible
        {
            get => _fortifyContentVisible;
            set => SetProperty(ref _fortifyContentVisible, value);
        }
        private bool _finishContentVisible;
        public bool FinishContentVisible
        {
            get => _finishContentVisible;
            set => SetProperty(ref _finishContentVisible, value);
        }
        private bool _isLoading = true;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private bool _noFeedsFound;
        public bool NoFeedsFound
        {
            get => _noFeedsFound;
            set => SetProperty(ref _noFeedsFound, value);
        }
        private bool _noFortifyFound;
        public bool NoFortifyFound
        {
            get => _noFortifyFound;
            set => SetProperty(ref _noFortifyFound, value);
        }
        private bool _noFinishFound;
        public bool NoFinishFound
        {
            get => _noFinishFound;
            set => SetProperty(ref _noFinishFound, value);
        }
        private bool _detailStackVisible = false;
        public bool DetailStackVisible
        {
            get => _detailStackVisible;
            set => SetProperty(ref _detailStackVisible, value);
        }
        private CustomProductAdditionalInfo _selectedAdditionalInfo = new CustomProductAdditionalInfo();
        public CustomProductAdditionalInfo SelectedAdditionalInfo
        {
            get => _selectedAdditionalInfo;
            set => SetProperty(ref _selectedAdditionalInfo, value);
        }
        public Command FeedCommand => new Command(() =>
          {
              FortifyContentVisible = false;
              FinishContentVisible = false;
              FetchFeedContentAsync();
              FortifyEvenProducts?.Clear();
              FortifyOddProducts?.Clear();
              FinishOddProducts?.Clear();
              FinishEvenProducts?.Clear();
          });
        public Command FortifyCommand => new Command(() =>
           {
               FeedContentVisible = false;
               FinishContentVisible = false;
               FetchFortifyContentAsync();
               FeedsEvenProducts?.Clear();
               FeedsOddsProducts?.Clear();
               FinishOddProducts?.Clear();
               FinishEvenProducts?.Clear();
           });
        public Command FinishCommand => new Command(() =>
           {
               FortifyContentVisible = false;
               FeedContentVisible = false;
               FetchFinishContentsAsync();
               FortifyEvenProducts?.Clear();
               FortifyOddProducts?.Clear();
               FeedsEvenProducts?.Clear();
               FeedsOddsProducts?.Clear();
           });
        public Command FilterOptionCommand => new Command(() => GoToFilterAsync());
        public Command FilterCommand => new Command(() =>
        {
            if (FeedContentVisible) FilterFeedsAsync();
            if (FortifyContentVisible) FilterFortifyAsync();
            if (FinishContentVisible) FilterFinishsAsync();
        });

        public Command AddToPlanCommand => new Command<CustomProduct>((param) =>AddToHealthPlanAsync(param));
        public Command ViewPlanCommand => new Command(() =>ViewHealthPlanAsync());

        public UserHealthPlanPageViewModel()
        {
            FeedsEvenProducts = new ObservableCollection<CustomProduct>();
            FeedsOddsProducts = new ObservableCollection<CustomProduct>();
            FortifyEvenProducts = new ObservableCollection<CustomProduct>();
            FortifyOddProducts = new ObservableCollection<CustomProduct>();
            FinishEvenProducts = new ObservableCollection<CustomProduct>();
            FinishEvenProducts = new ObservableCollection<CustomProduct>();
            FinishOddProducts = new ObservableCollection<CustomProduct>();
            FetchFeedContentAsync();
        }

        internal void RefreshBadgeCountAsync()
        {
            GetCartItemAsync();
        }
        private async void GetCartItemAsync()
        {
            try
            {
                var cartItems = await DatabaseServices.Get<List<Product>>("cartitems");
                if (cartItems.Count > 0)
                {
                    CartItemAdded?.Invoke(cartItems.Count);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void FetchFeedContentAsync()
        {
            try
            {
                if (Salon == null || Salon.Id == Guid.Empty)
                {
                    Salon = await DatabaseServices.Get<Salon>("salon");
                    if (Salon == null || Salon.Id==Guid.Empty)
                    {
                        Salon = await ApiServices.Client.GetAsync<Salon>("salon");
                        await DatabaseServices.InsertData("salon", Salon);
                    }
                  
                }
                if (Salon.ProductCategories != null && Salon.ProductCategories.Count > 0)
                {
                    NoFeedsFound = false;
                    foreach (var item in Salon.ProductCategories)
                    {
                        if (item.Name.ToLower() == "feed")
                        {
                            foreach (var product in item.Products)
                            {
                                var index = item.Products.IndexOf(product);
                                if (index % 2 == 0)
                                {
                                    FeedsEvenProducts.Add(new CustomProduct
                                    {
                                        Product = product,
                                        ImageUrl = product.ImageUrls.FirstOrDefault() != null ? new Uri(product.ImageUrls.FirstOrDefault()) : null,
                                        AdditionalInformations = product.AdditionalInformation.Select(a => new CustomProductAdditionalInfo
                                        {
                                            AdditionalInformation = a,
                                            ItemSelectedCommand = new Command<CustomProductAdditionalInfo>((param) => RefreshFeedProductListAsync(param, false))
                                        }).ToList(),
                                        AddPlanCommand = new Command<CustomProduct>((param) => AddToHealthPlanAsync(param))
                                    });
                                }
                                else
                                {
                                    FeedsOddsProducts.Add(new CustomProduct
                                    {
                                        Product = product,
                                        ImageUrl = product.ImageUrls.FirstOrDefault() != null ? new Uri(product.ImageUrls.FirstOrDefault()) : null,
                                        AdditionalInformations = product.AdditionalInformation.Select(a => new CustomProductAdditionalInfo
                                        {
                                            AdditionalInformation = a,
                                            ItemSelectedCommand = new Command<CustomProductAdditionalInfo>((param) => RefreshFeedProductListAsync(param, true))
                                        }).ToList(),
                                        AddPlanCommand = new Command<CustomProduct>((param) => AddToHealthPlanAsync(param))
                                    });
                                }

                            }

                            FeedContentVisible = true;
                        }
                    }
                }
                else
                {
                    NoFeedsFound = true;
                }

               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            IsLoading = false;
            FeedContentVisible = true;
        }

        private void RefreshFeedProductListAsync(CustomProductAdditionalInfo a, bool isOdd)
        {
            if (!isOdd)
            {
                FeedsEvenProducts.ForEach(item => item.AdditionalInformations.Where(t => t.Id != a.Id).ForEach(x => x.IsSelected = false));
            }
            else
            {
                FeedsOddsProducts.ForEach(item => item.AdditionalInformations.Where(t => t.Id != a.Id).ForEach(x => x.IsSelected = false));
            }

            SelectedAdditionalInfo = a;
        }

        private async void FetchFortifyContentAsync()
        {
            try
            {
                IsLoading = true;
                if (Salon == null || Salon.Id == Guid.Empty)
                {
                    Salon = await DatabaseServices.Get<Salon>("salon");
                    if (Salon == null)
                    {
                        Salon = await ApiServices.Client.GetAsync<Salon>("salon");
                        await DatabaseServices.InsertData("salon", Salon);
                    }

                }
                if (Salon.ProductCategories != null && Salon.ProductCategories.Count > 0)
                {
                    NoFortifyFound = false;
                    foreach (var item in Salon.ProductCategories.Where(item => item.Name.ToLower() == "fortify"))
                    {
                        foreach (var product in item.Products)
                        {
                            var index = item.Products.IndexOf(product);
                            if (index % 2 == 0)
                            {
                                FortifyEvenProducts.Add(new CustomProduct
                                {
                                    Product = product,
                                    ImageUrl = product.ImageUrls.FirstOrDefault() != null ? new Uri(product.ImageUrls.FirstOrDefault()) : null,
                                    AdditionalInformations = product.AdditionalInformation.Select(a => new CustomProductAdditionalInfo
                                    {
                                        AdditionalInformation = a,
                                        ItemSelectedCommand = new Command<CustomProductAdditionalInfo>((param) => RefreshFortifyProductListAsync(param, false))
                                    }).ToList(),
                                    AddPlanCommand = new Command<CustomProduct>((param) => AddToHealthPlanAsync(param))
                                });
                            }
                            else
                            {
                                FortifyOddProducts.Add(new CustomProduct
                                {
                                    Product = product,
                                    ImageUrl = product.ImageUrls.FirstOrDefault() != null ? new Uri(product.ImageUrls.FirstOrDefault()) : null,
                                    AdditionalInformations = product.AdditionalInformation.Select(a => new CustomProductAdditionalInfo
                                    {
                                        AdditionalInformation = a,
                                        ItemSelectedCommand = new Command<CustomProductAdditionalInfo>((param) => RefreshFortifyProductListAsync(param, true))
                                    }).ToList(),
                                    AddPlanCommand = new Command<CustomProduct>((param) => AddToHealthPlanAsync(param))
                                });
                            }

                        }
                    }
                }
                else
                {
                    NoFortifyFound = true;
                }

               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            IsLoading = false;
            FortifyContentVisible = true;
        }

        private void RefreshFortifyProductListAsync(CustomProductAdditionalInfo a, bool isOdd)
        {
            if (!isOdd)
            {
                FortifyEvenProducts.ForEach(item => item.AdditionalInformations.Where(t => t.Id != a.Id).ForEach(x => x.IsSelected = false));
            }
            else
            {
                FortifyOddProducts.ForEach(item => item.AdditionalInformations.Where(t => t.Id != a.Id).ForEach(x => x.IsSelected = false));
            }

            SelectedAdditionalInfo = a;
        }
        private async void FetchFinishContentsAsync()
        {
            try
            {
                IsLoading = true;
                if (Salon == null || Salon.Id == Guid.Empty)
                {
                    Salon = await DatabaseServices.Get<Salon>("salon");
                    if (Salon == null)
                    {
                        Salon = await ApiServices.Client.GetAsync<Salon>("salon");
                        await DatabaseServices.InsertData("salon", Salon);
                    }
                }
                if (Salon.ProductCategories != null && Salon.ProductCategories.Count > 0)
                {
                    NoFinishFound = false;
                    foreach (var item in Salon.ProductCategories.Where(item => item.Name.ToLower() == "finish"))
                    {
                        foreach (var product in item.Products)
                        {
                            var index = item.Products.IndexOf(product);
                            if (index % 2 == 0)
                            {
                                FinishEvenProducts.Add(new CustomProduct
                                {
                                    Product = product,
                                    ImageUrl = product.ImageUrls.FirstOrDefault() != null ? new Uri(product.ImageUrls.FirstOrDefault()) : null,
                                    AdditionalInformations = product.AdditionalInformation.Select(a => new CustomProductAdditionalInfo
                                    {
                                        AdditionalInformation = a,
                                        ItemSelectedCommand = new Command<CustomProductAdditionalInfo>((param) => RefreshFinishProductListAsync(param, false))
                                    }).ToList(),
                                    AddPlanCommand=new Command<CustomProduct>((param)=>AddToHealthPlanAsync(param))
                                });
                            }
                            else
                            {
                                FinishOddProducts.Add(new CustomProduct
                                {
                                    Product = product,
                                    ImageUrl = product.ImageUrls.FirstOrDefault() != null ? new Uri(product.ImageUrls.FirstOrDefault()) : null,
                                    AdditionalInformations = product.AdditionalInformation.Select(a => new CustomProductAdditionalInfo
                                    {
                                        AdditionalInformation = a,
                                        ItemSelectedCommand = new Command<CustomProductAdditionalInfo>((param) => RefreshFinishProductListAsync(param, true))
                                    }).ToList(),
                                    AddPlanCommand = new Command<CustomProduct>((param) => AddToHealthPlanAsync(param))
                                });
                            }

                        }
                    }
                }
                else
                {
                    NoFinishFound = true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            IsLoading = false;
            FinishContentVisible = true;
        }
        private void RefreshFinishProductListAsync(CustomProductAdditionalInfo a, bool isOdd)
        {
            if (!isOdd)
            {
                FinishEvenProducts.ForEach(item => item.AdditionalInformations.Where(t => t.Id != a.Id).ForEach(x => x.IsSelected = false));
            }
            else
            {
                FinishOddProducts.ForEach(item => item.AdditionalInformations.Where(t => t.Id != a.Id).ForEach(x => x.IsSelected = false));
            }

            SelectedAdditionalInfo = a;
        }


        private async void GoToFilterAsync()
        {
            try
            {
                if (FeedContentVisible)
                {
                    if (FeedFilterOptionList == null)
                    {
                        var filterOptions = new List<FilterOption>();
                        foreach (var item in Salon.ProductCategories.Where(x => x.Name.ToLower() == "feed"))
                        {
                            foreach (var product in item.Products)
                            {
                                foreach (var property in product.Properties)
                                {
                                    if (filterOptions.Count(x => x.Option.ToLower() == property.Detail.ToLower()) > 0) continue;
                                    filterOptions.Add(new FilterOption
                                    {
                                        Option = property.Detail,
                                        BackgroundColor = Color.Transparent
                                    });
                                }

                            }
                        }
                        FeedFilterOptionList = new ObservableCollection<FilterOption>(filterOptions);
                    }


                    await Application.Current.MainPage.Navigation.PushModalAsync(new FeedFilterPage(this));
                }
                else
                {
                    if (FortifyContentVisible)
                    {
                        if (FortifyFilterOptionList == null)
                        {
                            var filterOptions = new List<FilterOption>();
                            foreach (var item in Salon.ProductCategories.Where(x => x.Name.ToLower() == "fortify"))
                            {
                                foreach (var product in item.Products)
                                {
                                    foreach (var property in product.Properties)
                                    {
                                        if (filterOptions.Count(x => x.Option.ToLower() == property.Detail.ToLower()) > 0) continue;
                                        filterOptions.Add(new FilterOption
                                        {
                                            Option = property.Detail,
                                            BackgroundColor = Color.Transparent
                                        });
                                    }
                                }
                            }
                            FortifyFilterOptionList = new ObservableCollection<FilterOption>(filterOptions.Distinct());
                        }

                        await Application.Current.MainPage.Navigation.PushModalAsync(new FortifyFilterPage(this));
                    }
                    else if (FinishContentVisible)
                    {
                        if (FinishFilterOptionList != null)
                        {
                            await Application.Current.MainPage.Navigation.PushModalAsync(new FinishFilterPage(this));
                        }
                        else
                        {
                            var filterOptions = new List<FilterOption>();
                            foreach (var item in Salon.ProductCategories.Where(x => x.Name.ToLower() == "finish"))
                            {
                                foreach (var product in item.Products)
                                {
                                    foreach (var property in product.Properties)
                                    {
                                        if (filterOptions.Count(x => x.Option.ToLower() == property.Detail.ToLower()) > 0) continue;
                                        filterOptions.Add(new FilterOption
                                        {
                                            Option = property.Detail,
                                            BackgroundColor = Color.Transparent
                                        });
                                    }
                                }
                            }
                            FinishFilterOptionList = new ObservableCollection<FilterOption>(filterOptions.Distinct());
                            await Application.Current.MainPage.Navigation.PushModalAsync(new FinishFilterPage(this));
                        }
                      
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void FilterFinishsAsync()
        {
            try
            {

                Application.Current.MainPage.Navigation.PopModalAsync();
                IsLoading = true;
                var selectedOptions = FinishFilterOptionList.Where(a => a.IsSelected).ToList();
                if (selectedOptions.Count == 0)
                {
                    FetchFinishContentsAsync();
                }
                else
                {
                    var salonProduct = Salon.ProductCategories.First(a => a.Name.ToLower() == "finish");
                    FinishEvenProducts = new ObservableCollection<CustomProduct>();
                    FinishOddProducts = new ObservableCollection<CustomProduct>();
                    var filteredList = new List<Product>();
                    foreach (var item in salonProduct.Products)
                    {
                        var data = item.Properties.Select(a => a.Detail).Intersect(selectedOptions.Select(a => a.Option)).ToList();
                        if (data.Count == selectedOptions.Count)
                        {
                            filteredList.Add(item);
                        }
                    }

                    foreach (var product in filteredList)
                    {
                        var index = filteredList.IndexOf(product);
                        if (index % 2 == 0)
                        {
                            FinishEvenProducts.Add(new CustomProduct
                            {
                                Product = product,
                                ImageUrl = product.ImageUrls.FirstOrDefault() != null ? new Uri(product.ImageUrls.FirstOrDefault()) : null,
                                AdditionalInformations = product.AdditionalInformation.Select(a => new CustomProductAdditionalInfo
                                {
                                    AdditionalInformation = a,
                                    ItemSelectedCommand = new Command<CustomProductAdditionalInfo>((param) => RefreshFeedProductListAsync(param, false))
                                }).ToList(),
                                AddPlanCommand = new Command<CustomProduct>((param) => AddToHealthPlanAsync(param))
                            });
                        }
                        else
                        {
                            FinishOddProducts.Add(new CustomProduct
                            {
                                Product = product,
                                ImageUrl = product.ImageUrls.FirstOrDefault() != null ? new Uri(product.ImageUrls.FirstOrDefault()) : null,
                                AdditionalInformations = product.AdditionalInformation.Select(a => new CustomProductAdditionalInfo
                                {
                                    AdditionalInformation = a,
                                    ItemSelectedCommand = new Command<CustomProductAdditionalInfo>((param) => RefreshFeedProductListAsync(param, true))
                                }).ToList(),
                                AddPlanCommand = new Command<CustomProduct>((param) => AddToHealthPlanAsync(param))
                            });
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            IsLoading = false;
        }

        private void FilterFortifyAsync()
        {
            try
            {
                Application.Current.MainPage.Navigation.PopModalAsync();
                IsLoading = true;
                var selectedOptions = FortifyFilterOptionList.Where(a => a.IsSelected).ToList();
                if (selectedOptions.Count == 0)
                {
                    FetchFortifyContentAsync();
                }
                else
                {
                    var salonProduct = Salon.ProductCategories.First(a => a.Name.ToLower() == "fortify");
                    FortifyEvenProducts = new ObservableCollection<CustomProduct>();
                    FortifyOddProducts = new ObservableCollection<CustomProduct>();
                    var filteredList = new List<Product>();
                    foreach (var item in salonProduct.Products)
                    {
                        var data = item.Properties.Select(a => a.Detail).Intersect(selectedOptions.Select(a => a.Option)).ToList();
                        if (data.Count == selectedOptions.Count)
                        {
                            filteredList.Add(item);
                        }
                    }

                    foreach (var product in filteredList)
                    {
                        var index = filteredList.IndexOf(product);
                        if (index % 2 == 0)
                        {
                            FortifyEvenProducts.Add(new CustomProduct
                            {
                                Product = product,
                                ImageUrl = product.ImageUrls.FirstOrDefault() != null ? new Uri(product.ImageUrls.FirstOrDefault()) : null,
                                AdditionalInformations = product.AdditionalInformation.Select(a => new CustomProductAdditionalInfo
                                {
                                    AdditionalInformation = a,
                                    ItemSelectedCommand = new Command<CustomProductAdditionalInfo>((param) => RefreshFeedProductListAsync(param, false))
                                }).ToList(),
                                AddPlanCommand = new Command<CustomProduct>((param) => AddToHealthPlanAsync(param))
                            });
                        }
                        else
                        {
                            FortifyOddProducts.Add(new CustomProduct
                            {
                                Product = product,
                                ImageUrl = product.ImageUrls.FirstOrDefault() != null ? new Uri(product.ImageUrls.FirstOrDefault()) : null,
                                AdditionalInformations = product.AdditionalInformation.Select(a => new CustomProductAdditionalInfo
                                {
                                    AdditionalInformation = a,
                                    ItemSelectedCommand = new Command<CustomProductAdditionalInfo>((param) => RefreshFeedProductListAsync(param, true))
                                }).ToList(),
                                AddPlanCommand = new Command<CustomProduct>((param) => AddToHealthPlanAsync(param))
                            });
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            IsLoading = false;
        }

        private void FilterFeedsAsync()
        {
            try
            {
                Application.Current.MainPage.Navigation.PopModalAsync();
                IsLoading = true;
                var selectedOptions = FeedFilterOptionList.Where(a => a.IsSelected).ToList();
                if (selectedOptions.Count == 0)
                {
                    FetchFeedContentAsync();
                }
                else
                {
                    var salonProduct = Salon.ProductCategories.First(a => a.Name.ToLower() == "feed");
                    FeedsEvenProducts = new ObservableCollection<CustomProduct>();
                    FeedsOddsProducts = new ObservableCollection<CustomProduct>();
                    var filteredList = new List<Product>();
                    foreach (var item in salonProduct.Products)
                    {
                        var data = item.Properties.Select(a => a.Detail).Intersect(selectedOptions.Select(a => a.Option)).ToList();
                        if (data.Count == selectedOptions.Count)
                        {
                            filteredList.Add(item);
                        }
                    }

                    foreach (var product in filteredList)
                    {
                        var index = filteredList.IndexOf(product);
                        if (index % 2 == 0)
                        {
                            FeedsEvenProducts.Add(new CustomProduct
                            {
                                Product = product,
                                ImageUrl = product.ImageUrls.FirstOrDefault() != null ? new Uri(product.ImageUrls.FirstOrDefault()) : null,
                                AdditionalInformations = product.AdditionalInformation.Select(a => new CustomProductAdditionalInfo
                                {
                                    AdditionalInformation = a,
                                    ItemSelectedCommand = new Command<CustomProductAdditionalInfo>((param) => RefreshFeedProductListAsync(param, false))
                                }).ToList(),
                                AddPlanCommand = new Command<CustomProduct>((param) => AddToHealthPlanAsync(param))
                            });
                        }
                        else
                        {
                            FeedsOddsProducts.Add(new CustomProduct
                            {
                                Product = product,
                                ImageUrl = product.ImageUrls.FirstOrDefault() != null ? new Uri(product.ImageUrls.FirstOrDefault()) : null,
                                AdditionalInformations = product.AdditionalInformation.Select(a => new CustomProductAdditionalInfo
                                {
                                    AdditionalInformation = a,
                                    ItemSelectedCommand = new Command<CustomProductAdditionalInfo>((param) => RefreshFeedProductListAsync(param, true))
                                }).ToList(),
                                AddPlanCommand = new Command<CustomProduct>((param) => AddToHealthPlanAsync(param))
                            });
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            IsLoading = false;
        }


        private async void AddToHealthPlanAsync(CustomProduct product)
        {
            try
            {
                var cartItems = await DatabaseServices.Get<List<Product>>("cartitems");
                if (cartItems.Count > 0)
                {
                    if (cartItems.Count(async => async.Id == product.Product.Id) ==0)
                    {
                        cartItems.Add(product.Product);
                        await DatabaseServices.InsertData("cartitems", cartItems);
                    }
                }
                else
                {
                    cartItems.Add(product.Product);
                    await DatabaseServices.InsertData("cartitems", cartItems);
                }

                CartItemAdded?.Invoke(cartItems.Count);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "/n/n" + ex.StackTrace);
            }
        }

        private void ViewHealthPlanAsync()
        {
            Application.Current.MainPage.Navigation.PushAsync(new HealthPlanBasketPage());
        }

    }
}
