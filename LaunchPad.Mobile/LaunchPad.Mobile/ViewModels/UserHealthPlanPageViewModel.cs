using IIAADataModels.Transfer;
using LaunchPad.Client;
using LaunchPad.Mobile.Models;
using LaunchPad.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LaunchPad.Mobile.ViewModels
{
    public class UserHealthPlanPageViewModel : ViewModelBase
    {
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

        private List<string> _filterOptionList;
        public List<string> FilterOptionList
        {
            get => _filterOptionList;
            set => SetProperty(ref _filterOptionList, value);
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
               FinishOddProducts?.Clear();
               FinishEvenProducts?.Clear();
               FeedsEvenProducts?.Clear();
               FeedsOddsProducts?.Clear();
           });
        public Command FilterOptionCommand => new Command(() => GoToFilterAsync());
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

        private async void FetchFeedContentAsync()
        {
            try
            {
                if (Salon == null || Salon.Id == Guid.Empty)
                {
                    Salon = await ApiServices.Client.GetAsync<Salon>("salon");
                }
                if (Salon.Products != null && Salon.Products.Count > 0)
                {
                    NoFeedsFound = false;
                    foreach (var item in Salon.Products)
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
                                        AdditionalInformations = product.AdditionalInformation.Select(a =>new CustomProductAdditionalInfo
                                        {
                                            AdditionalInformation=a
                                        }).ToList()
                                    });
                                }
                                else
                                {
                                    FeedsOddsProducts.Add(new CustomProduct
                                    {
                                        Product = product,
                                        ImageUrl = product.ImageUrls.FirstOrDefault() != null ? new Uri(product.ImageUrls.FirstOrDefault()) : null
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

                #region # Static data #

                //IsLoading = true;
                //await Task.Delay(2000);
                //var products = new ObservableCollection<CustomProduct>
                //{
                //        new CustomProduct
                //        {
                //             Product=new Product
                //            {
                //                Id=Guid.NewGuid(),
                //                Name="Skin Accumax & Trade",
                //                Summary="A nutritional supplement which works from within for clear, flawless skin.",
                //                ImageUrls=new List<string>
                //                {
                //                    "https://via.placeholder.com/150"
                //                },
                //                Properties=new List<ProductProperty>
                //                {
                //                    new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    },
                //                     new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    },
                //                      new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    },
                //                       new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    }

                //                },
                //                SkinConcerns=new List<string>
                //                {
                //                     "Redness/Sensitivity",
                //            "Lack of Radiance"
                //                }
                //            },

                //             ImageUrl=new Uri("https://via.placeholder.com/150")
                //        },
                //         new CustomProduct
                //        {
                //             Product=new Product
                //            {
                //                Id=Guid.NewGuid(),
                //                Name="Hydrating Clay Masque",
                //                Summary="Assists in absorbing excess oils and micro-exfoliating the skin.",
                //                ImageUrls=new List<string>
                //                {
                //                    "https://via.placeholder.com/150"
                //                },
                //                Properties=new List<ProductProperty>
                //                {
                //                    new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    },
                //                     new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    },
                //                      new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    },
                //                       new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    }
                //                },
                //                SkinConcerns=new List<string>
                //                {
                //                     "Redness/Sensitivity",
                //            "Lack of Radiance"
                //                }
                //            },

                //             ImageUrl=new Uri("https://via.placeholder.com/150")
                //        }
                //         ,
                //          new CustomProduct
                //        {
                //             Product=new Product
                //            {
                //                Id=Guid.NewGuid(),
                //                Name="Skin Accumax & Trade",
                //                Summary="A nutritional supplement which works from within for clear, flawless skin.",
                //                ImageUrls=new List<string>
                //                {
                //                    "https://via.placeholder.com/150"
                //                },
                //                Properties=new List<ProductProperty>
                //                {
                //                    new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    },
                //                     new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    },
                //                      new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    },
                //                       new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    }

                //                },
                //                SkinConcerns=new List<string>
                //                {
                //                     "Redness/Sensitivity",
                //            "Lack of Radiance"
                //                }
                //            },

                //             ImageUrl=new Uri("https://via.placeholder.com/150")
                //        },
                //         new CustomProduct
                //        {
                //             Product=new Product
                //            {
                //                Id=Guid.NewGuid(),
                //                Name="Hydrating Clay Masque",
                //                Summary="Assists in absorbing excess oils and micro-exfoliating the skin.",
                //                ImageUrls=new List<string>
                //                {
                //                    "https://via.placeholder.com/150"
                //                },
                //                Properties=new List<ProductProperty>
                //                {
                //                    new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    },
                //                     new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    },
                //                      new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    },
                //                       new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    }
                //                },
                //                SkinConcerns=new List<string>
                //                {
                //                     "Redness/Sensitivity",
                //            "Lack of Radiance"
                //                }
                //            },

                //             ImageUrl=new Uri("https://via.placeholder.com/150")
                //        },
                //          new CustomProduct
                //        {
                //             Product=new Product
                //            {
                //                Id=Guid.NewGuid(),
                //                Name="Skin Accumax & Trade",
                //                Summary="A nutritional supplement which works from within for clear, flawless skin.",
                //                ImageUrls=new List<string>
                //                {
                //                    "https://via.placeholder.com/150"
                //                },
                //                Properties=new List<ProductProperty>
                //                {
                //                    new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    },
                //                     new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    },
                //                      new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    },
                //                       new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    }

                //                },
                //                SkinConcerns=new List<string>
                //                {
                //                     "Redness/Sensitivity",
                //            "Lack of Radiance"
                //                }
                //            },

                //             ImageUrl=new Uri("https://via.placeholder.com/150")
                //        },
                //         new CustomProduct
                //        {
                //             Product=new Product
                //            {
                //                Id=Guid.NewGuid(),
                //                Name="Hydrating Clay Masque",
                //                Summary="Assists in absorbing excess oils and micro-exfoliating the skin.",
                //                ImageUrls=new List<string>
                //                {
                //                    "https://via.placeholder.com/150"
                //                },
                //                Properties=new List<ProductProperty>
                //                {
                //                    new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    },
                //                     new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    },
                //                      new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    },
                //                       new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    }
                //                },
                //                SkinConcerns=new List<string>
                //                {
                //                     "Redness/Sensitivity",
                //            "Lack of Radiance"
                //                }
                //            },

                //             ImageUrl=new Uri("https://via.placeholder.com/150")
                //        }
                //};

                //foreach (var item in products)
                //{
                //    var index = products.IndexOf(item);
                //    if (index % 2 == 0)
                //    {
                //        FeedsEvenProducts.Add(item);
                //    }
                //    else
                //    {
                //        FeedsOddsProducts.Add(item);
                //    }
                //}

                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            IsLoading = false;
            FeedContentVisible = true;
        }
        private async void FetchFortifyContentAsync()
        {
            try
            {
                IsLoading = true;
                if (Salon == null || Salon.Id == Guid.Empty)
                {
                    Salon = await ApiServices.Client.GetAsync<Salon>("salon");
                }
                else
                {
                    await Task.Delay(2000);
                }
                if (Salon.Products != null && Salon.Products.Count > 0)
                {
                    NoFortifyFound = false;
                    foreach (var item in Salon.Products.Where(item => item.Name.ToLower() == "fortify"))
                    {
                        foreach (var product in item.Products)
                        {
                            var index = item.Products.IndexOf(product);
                            if (index % 2 == 0)
                            {
                                FortifyEvenProducts.Add(new CustomProduct
                                {
                                    Product = product,
                                    ImageUrl = product.ImageUrls.FirstOrDefault() != null ? new Uri(product.ImageUrls.FirstOrDefault()) : null
                                });
                            }
                            else
                            {
                                FortifyOddProducts.Add(new CustomProduct
                                {
                                    Product = product,
                                    ImageUrl = product.ImageUrls.FirstOrDefault() != null ? new Uri(product.ImageUrls.FirstOrDefault()) : null
                                });
                            }

                        }
                    }
                }
                else
                {
                    NoFortifyFound = true;
                }

                #region # Static  Data #

                //    Products = new ObservableCollection<CustomProduct>
                //{
                //        new CustomProduct
                //        {
                //             Product=new Product
                //            {
                //                Id=Guid.NewGuid(),
                //                Name="Skin Accumax & Trade",
                //                Summary="A nutritional supplement which works from within for clear, flawless skin.",
                //                ImageUrls=new List<string>
                //                {
                //                    "https://via.placeholder.com/150"
                //                },
                //                Properties=new List<ProductProperty>
                //                {
                //                    new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    },
                //                     new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    },
                //                      new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    },
                //                       new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    }

                //                },
                //                SkinConcerns=new List<string>
                //                {
                //                     "Redness/Sensitivity",
                //            "Lack of Radiance"
                //                }
                //            },

                //             ImageUrl=new Uri("https://via.placeholder.com/150")
                //        },
                //         new CustomProduct
                //        {
                //             Product=new Product
                //            {
                //                Id=Guid.NewGuid(),
                //                Name="Hydrating Clay Masque",
                //                Summary="Assists in absorbing excess oils and micro-exfoliating the skin.",
                //                ImageUrls=new List<string>
                //                {
                //                    "https://via.placeholder.com/150"
                //                },
                //                Properties=new List<ProductProperty>
                //                {
                //                    new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    },
                //                     new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    },
                //                      new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    },
                //                       new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    }
                //                },
                //                SkinConcerns=new List<string>
                //                {
                //                     "Redness/Sensitivity",
                //            "Lack of Radiance"
                //                }
                //            },

                //             ImageUrl=new Uri("https://via.placeholder.com/150")
                //        }
                //        // ,
                //        //  new CustomProduct
                //        //{
                //        //     Product=new Product
                //        //    {
                //        //        Id=Guid.NewGuid(),
                //        //        Name="Skin Accumax & Trade",
                //        //        Summary="A nutritional supplement which works from within for clear, flawless skin.",
                //        //        ImageUrls=new List<string>
                //        //        {
                //        //            "https://via.placeholder.com/150"
                //        //        },
                //        //        Properties=new List<ProductProperty>
                //        //        {
                //        //            new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            },
                //        //             new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            },
                //        //              new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            },
                //        //               new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            }

                //        //        },
                //        //        SkinConcerns=new List<string>
                //        //        {
                //        //             "Redness/Sensitivity",
                //        //    "Lack of Radiance"
                //        //        }
                //        //    },

                //        //     ImageUrl=new Uri("https://via.placeholder.com/150")
                //        //},
                //        // new CustomProduct
                //        //{
                //        //     Product=new Product
                //        //    {
                //        //        Id=Guid.NewGuid(),
                //        //        Name="Hydrating Clay Masque",
                //        //        Summary="Assists in absorbing excess oils and micro-exfoliating the skin.",
                //        //        ImageUrls=new List<string>
                //        //        {
                //        //            "https://via.placeholder.com/150"
                //        //        },
                //        //        Properties=new List<ProductProperty>
                //        //        {
                //        //            new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            },
                //        //             new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            },
                //        //              new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            },
                //        //               new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            }
                //        //        },
                //        //        SkinConcerns=new List<string>
                //        //        {
                //        //             "Redness/Sensitivity",
                //        //    "Lack of Radiance"
                //        //        }
                //        //    },

                //        //     ImageUrl=new Uri("https://via.placeholder.com/150")
                //        //},
                //        //  new CustomProduct
                //        //{
                //        //     Product=new Product
                //        //    {
                //        //        Id=Guid.NewGuid(),
                //        //        Name="Skin Accumax & Trade",
                //        //        Summary="A nutritional supplement which works from within for clear, flawless skin.",
                //        //        ImageUrls=new List<string>
                //        //        {
                //        //            "https://via.placeholder.com/150"
                //        //        },
                //        //        Properties=new List<ProductProperty>
                //        //        {
                //        //            new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            },
                //        //             new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            },
                //        //              new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            },
                //        //               new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            }

                //        //        },
                //        //        SkinConcerns=new List<string>
                //        //        {
                //        //             "Redness/Sensitivity",
                //        //    "Lack of Radiance"
                //        //        }
                //        //    },

                //        //     ImageUrl=new Uri("https://via.placeholder.com/150")
                //        //},
                //        // new CustomProduct
                //        //{
                //        //     Product=new Product
                //        //    {
                //        //        Id=Guid.NewGuid(),
                //        //        Name="Hydrating Clay Masque",
                //        //        Summary="Assists in absorbing excess oils and micro-exfoliating the skin.",
                //        //        ImageUrls=new List<string>
                //        //        {
                //        //            "https://via.placeholder.com/150"
                //        //        },
                //        //        Properties=new List<ProductProperty>
                //        //        {
                //        //            new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            },
                //        //             new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            },
                //        //              new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            },
                //        //               new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            }
                //        //        },
                //        //        SkinConcerns=new List<string>
                //        //        {
                //        //             "Redness/Sensitivity",
                //        //    "Lack of Radiance"
                //        //        }
                //        //    },

                //        //     ImageUrl=new Uri("https://via.placeholder.com/150")
                //        //}
                //};


                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            IsLoading = false;
            FortifyContentVisible = true;
        }
        private async void FetchFinishContentsAsync()
        {
            try
            {
                IsLoading = true;                
                if (Salon == null)
                {
                    Salon = await ApiServices.Client.GetAsync<Salon>("salon");
                }
                else
                {
                    await Task.Delay(2000);
                }
                if (Salon.Products != null && Salon.Products.Count > 0)
                {
                    NoFinishFound = false;
                    foreach (var item in Salon.Products.Where(item => item.Name.ToLower() == "finish"))
                    {
                        foreach (var product in item.Products)
                        {
                            var index = item.Products.IndexOf(product);
                            if (index % 2 == 0)
                            {
                                FinishEvenProducts.Add(new CustomProduct
                                {
                                    Product = product,
                                    ImageUrl = product.ImageUrls.FirstOrDefault() != null ? new Uri(product.ImageUrls.FirstOrDefault()) : null
                                });
                            }
                            else
                            {
                                FinishOddProducts.Add(new CustomProduct
                                {
                                    Product = product,
                                    ImageUrl = product.ImageUrls.FirstOrDefault() != null ? new Uri(product.ImageUrls.FirstOrDefault()) : null
                                });
                            }

                        }
                    }
                }
                else
                {
                    NoFinishFound = true;
                }

                #region # Static data #

                //    Products = new ObservableCollection<CustomProduct>
                //{
                //        new CustomProduct
                //        {
                //             Product=new Product
                //            {
                //                Id=Guid.NewGuid(),
                //                Name="Skin Accumax & Trade",
                //                Summary="A nutritional supplement which works from within for clear, flawless skin.",
                //                ImageUrls=new List<string>
                //                {
                //                    "https://via.placeholder.com/150"
                //                },
                //                Properties=new List<ProductProperty>
                //                {
                //                    new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    },
                //                     new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    },
                //                      new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    },
                //                       new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    }

                //                },
                //                SkinConcerns=new List<string>
                //                {
                //                     "Redness/Sensitivity",
                //            "Lack of Radiance"
                //                }
                //            },

                //             ImageUrl=new Uri("https://via.placeholder.com/150")
                //        },
                //         new CustomProduct
                //        {
                //             Product=new Product
                //            {
                //                Id=Guid.NewGuid(),
                //                Name="Hydrating Clay Masque",
                //                Summary="Assists in absorbing excess oils and micro-exfoliating the skin.",
                //                ImageUrls=new List<string>
                //                {
                //                    "https://via.placeholder.com/150"
                //                },
                //                Properties=new List<ProductProperty>
                //                {
                //                    new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    },
                //                     new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    },
                //                      new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    },
                //                       new ProductProperty
                //                    {
                //                        ImageUrl="https://via.placeholder.com/64",
                //                        Detail="Vegan"
                //                    }
                //                },
                //                SkinConcerns=new List<string>
                //                {
                //                     "Redness/Sensitivity",
                //            "Lack of Radiance"
                //                }
                //            },

                //             ImageUrl=new Uri("https://via.placeholder.com/150")
                //        }
                //        // ,
                //        //  new CustomProduct
                //        //{
                //        //     Product=new Product
                //        //    {
                //        //        Id=Guid.NewGuid(),
                //        //        Name="Skin Accumax & Trade",
                //        //        Summary="A nutritional supplement which works from within for clear, flawless skin.",
                //        //        ImageUrls=new List<string>
                //        //        {
                //        //            "https://via.placeholder.com/150"
                //        //        },
                //        //        Properties=new List<ProductProperty>
                //        //        {
                //        //            new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            },
                //        //             new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            },
                //        //              new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            },
                //        //               new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            }

                //        //        },
                //        //        SkinConcerns=new List<string>
                //        //        {
                //        //             "Redness/Sensitivity",
                //        //    "Lack of Radiance"
                //        //        }
                //        //    },

                //        //     ImageUrl=new Uri("https://via.placeholder.com/150")
                //        //},
                //        // new CustomProduct
                //        //{
                //        //     Product=new Product
                //        //    {
                //        //        Id=Guid.NewGuid(),
                //        //        Name="Hydrating Clay Masque",
                //        //        Summary="Assists in absorbing excess oils and micro-exfoliating the skin.",
                //        //        ImageUrls=new List<string>
                //        //        {
                //        //            "https://via.placeholder.com/150"
                //        //        },
                //        //        Properties=new List<ProductProperty>
                //        //        {
                //        //            new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            },
                //        //             new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            },
                //        //              new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            },
                //        //               new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            }
                //        //        },
                //        //        SkinConcerns=new List<string>
                //        //        {
                //        //             "Redness/Sensitivity",
                //        //    "Lack of Radiance"
                //        //        }
                //        //    },

                //        //     ImageUrl=new Uri("https://via.placeholder.com/150")
                //        //},
                //        //  new CustomProduct
                //        //{
                //        //     Product=new Product
                //        //    {
                //        //        Id=Guid.NewGuid(),
                //        //        Name="Skin Accumax & Trade",
                //        //        Summary="A nutritional supplement which works from within for clear, flawless skin.",
                //        //        ImageUrls=new List<string>
                //        //        {
                //        //            "https://via.placeholder.com/150"
                //        //        },
                //        //        Properties=new List<ProductProperty>
                //        //        {
                //        //            new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            },
                //        //             new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            },
                //        //              new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            },
                //        //               new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            }

                //        //        },
                //        //        SkinConcerns=new List<string>
                //        //        {
                //        //             "Redness/Sensitivity",
                //        //    "Lack of Radiance"
                //        //        }
                //        //    },

                //        //     ImageUrl=new Uri("https://via.placeholder.com/150")
                //        //},
                //        // new CustomProduct
                //        //{
                //        //     Product=new Product
                //        //    {
                //        //        Id=Guid.NewGuid(),
                //        //        Name="Hydrating Clay Masque",
                //        //        Summary="Assists in absorbing excess oils and micro-exfoliating the skin.",
                //        //        ImageUrls=new List<string>
                //        //        {
                //        //            "https://via.placeholder.com/150"
                //        //        },
                //        //        Properties=new List<ProductProperty>
                //        //        {
                //        //            new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            },
                //        //             new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            },
                //        //              new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            },
                //        //               new ProductProperty
                //        //            {
                //        //                ImageUrl="https://via.placeholder.com/64",
                //        //                Detail="Vegan"
                //        //            }
                //        //        },
                //        //        SkinConcerns=new List<string>
                //        //        {
                //        //             "Redness/Sensitivity",
                //        //    "Lack of Radiance"
                //        //        }
                //        //    },

                //        //     ImageUrl=new Uri("https://via.placeholder.com/150")
                //        //}
                //};

                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            IsLoading = false;
            FinishContentVisible = true;
        }

        private async void GoToFilterAsync()
        {
            try
            {
                FilterOptionList = new List<string>
                {
                    "Skin Concern",
                    "Wellbeing Range",
                    "Skin Range",
                    "Vegan",
                    "Vegetarian",
                    "Retin",
                    "Retin A"
                };

                Application.Current.MainPage.Navigation.PushModalAsync(new FilterPage(this));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
