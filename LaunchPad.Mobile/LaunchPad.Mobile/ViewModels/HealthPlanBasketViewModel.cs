using IIAADataModels.Transfer;
using LaunchPad.Client;
using LaunchPad.Mobile.Models;
using LaunchPad.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace LaunchPad.Mobile.ViewModels
{
    public class HealthPlanBasketViewModel:ViewModelBase
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
        private ObservableCollection<HealthPlan> _healthPlanCollection=new ObservableCollection<HealthPlan>();
        public ObservableCollection<HealthPlan> HealthPlanCollection
        {
            get => _healthPlanCollection;
            set => SetProperty(ref _healthPlanCollection, value);
        }

        private string _subTotal=$"£0.00";
        public string SubTotal
        {
            get => _subTotal;
            set => SetProperty(ref _subTotal, value);
        }
        #region # Constructor #
        public HealthPlanBasketViewModel()
        {
            GetHealthPlanAsync();
        }

        #endregion

        #region # Methods #

        private async void GetHealthPlanAsync()
        {
            try
            {
                var cartItems = await DatabaseServices.Get<List<Product>>("cartitems");
                if (cartItems.Count > 0)
                {
                    decimal sumTotal = 0;
                    BadgeCountAction?.Invoke(cartItems.Count);
                    if (Salon == null || Salon.Id == Guid.Empty)
                    {
                        Salon = await DatabaseServices.Get<Salon>("salon");
                        if (Salon == null || Salon.Id == Guid.Empty)
                        {
                            Salon = await ApiServices.Client.GetAsync<Salon>("salon");
                            await DatabaseServices.InsertData("salon", Salon);
                        }
                    }

                    var categoryList = Salon.ProductCategories.Select(a => a.Subtitle).ToList();
                    
                    foreach (var productCategory in Salon.ProductCategories)
                    {
                        var healthPlan = new HealthPlan();
                        healthPlan.ProgramName = productCategory.Subtitle;
                        healthPlan.ProductWithLevelTypeList = new ObservableCollection<ProductWithLevelType>();
                        var groupByClassification = productCategory.Products.Where(a=>cartItems.Count(x=>x.Id==a.Id)>0).GroupBy(a => a.Classification).ToList();
                        foreach (var item in groupByClassification)
                        {
                            var temp = new ProductWithLevelType
                            {
                                Classification = item.Key,
                                ProgramName=productCategory.Subtitle,
                                Products = new ObservableCollection<CustomProduct>(item.Select(a => new CustomProduct
                                {
                                    Product = a,
                                    ShouldShowVariant = a.Variants.Count > 0,
                                    VariantsList = new ObservableCollection<ProductVariant>(a.Variants),
                                    SelectedVariant = a.Variants.FirstOrDefault(),
                                    ImageUrl = new Uri(a.ImageUrls.First()),
                                    RefreshPriceCommand=new Command(()=>RefreshPriceAsync())
                                }).ToList())
                            };

                            sumTotal=sumTotal+temp.Products.Sum(x => x.SelectedVariant.Price);
                            healthPlan.ProductWithLevelTypeList.Add(temp);                            
                        }

                        HealthPlanCollection.Add(healthPlan);
                        //SubTotal = $"£{sumTotal.ToString("F2")}";
                    }
                }
                // Program name

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "/n/n" + ex.StackTrace);
            }
        }

        private void RefreshPriceAsync()
        {
            try
            {
                decimal sumTotal = 0;
                foreach (var item in HealthPlanCollection)
                {
                    foreach (var temp in item.ProductWithLevelTypeList)
                    {
                        sumTotal = sumTotal + temp.Products.Sum(a => a.SelectedVariant.Price);
                    }
                }

                //SubTotal = $"£{sumTotal.ToString("F2")}";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n\n" + ex.StackTrace);
            }
        }
        #endregion
    }
}
