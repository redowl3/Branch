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
        public CompletedHealthPlanPageViewModel()
        {
             GetItemsAsync();
        }
        private void GetItemsAsync()
        {
            IsBusy = true;

            Task.Run(() => ExceptionHandler(async () =>
            {
                var basket = await DatabaseServices.Get<CustomBasket>("basketItems");
                var salon = await DatabaseServices.Get<Salon>("salon");
                if(basket!=null && basket.ItemsCollection.Count>0)
                {
                    AmountToBePaid= $"{basket.ItemsCollection.Sum(a => a.Variant.Price).ToString("F2")}";
                    BadgeCountAction?.Invoke(basket.ItemsCollection.Count);
                    foreach (var item in basket.Basket.Items)
                    {
                        foreach (var productCategory in salon.ProductCategories)
                        {
                            var products = productCategory.Products.Where(a => a.Id == item.ProductId).ToList();
                            if (products.Count>0)
                            {
                                foreach (var product in products)
                                {
                                    var completedHealthPlan = new CompletedHealthPlan();
                                    completedHealthPlan.ProgramName = productCategory.Subtitle;
                                    completedHealthPlan.Product = product;
                                    completedHealthPlan.VariantsList = new ObservableCollection<ProductVariant>();
                                    completedHealthPlan.ShouldShowVariant=completedHealthPlan.IsDropdownVisible = product.Variants?.Count > 0;
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
                }
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
