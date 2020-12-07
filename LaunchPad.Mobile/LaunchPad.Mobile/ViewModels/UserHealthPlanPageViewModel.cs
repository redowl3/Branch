using IIAADataModels.Transfer;
using LaunchPad.Client;
using LaunchPad.Mobile.Models;
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
        private ObservableCollection<CustomProduct> _fortifyProducts;
        public ObservableCollection<CustomProduct> FortifyProducts
        {
            get => _fortifyProducts;
            set => SetProperty(ref _fortifyProducts, value);
        }
        private ObservableCollection<CustomProduct> _finishProducts;
        public ObservableCollection<CustomProduct> FinishProducts
        {
            get => _finishProducts;
            set => SetProperty(ref _finishProducts, value);
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
        private bool _isLoading=true;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }
        public Command FeedCommand => new Command(() =>
          {
              FortifyContentVisible = false;
              FeedContentVisible = true;
              FinishContentVisible = false;
              FetchFeedContentAsync();
              FortifyProducts?.Clear();
              FinishProducts?.Clear();
          }); 
        public Command FortifyCommand => new Command(() =>
           {
               FortifyContentVisible = true;
               FeedContentVisible = false;
               FinishContentVisible = false;
               FetchFortifyContentAsync();
               FeedsEvenProducts?.Clear();
               FeedsOddsProducts?.Clear();
               FinishProducts?.Clear();
           }); 
        public Command FinishCommand => new Command(() =>
           {
               FortifyContentVisible = false;
               FeedContentVisible = false;
               FinishContentVisible = true;
               FetchFinishContentsAsync();
               FinishProducts?.Clear();
               FeedsEvenProducts?.Clear();
               FeedsOddsProducts?.Clear();
           });
        public UserHealthPlanPageViewModel()
        {
            FeedsEvenProducts = new ObservableCollection<CustomProduct>();
            FeedsOddsProducts = new ObservableCollection<CustomProduct>();
            FortifyProducts = new ObservableCollection<CustomProduct>();
            FinishProducts = new ObservableCollection<CustomProduct>();
            FetchFeedContentAsync();
        }

        private async void FetchFeedContentAsync()
        {
            try
            {
                //if (Salon == null || Salon.Id==Guid.Empty)
                //{
                //    Salon = await ApiServices.Client.GetAsync<Salon>("salon");
                //}
                //if (Salon.Products != null && Salon.Products.Count > 0)
                //{
                //    foreach (var item in Salon.Products)
                //    {
                //        if (item.Name.ToLower() == "feed")
                //        {
                //            foreach (var product in item.Products)
                //            {
                //                var index = item.Products.IndexOf(product);
                //                if (index % 2 == 0)
                //                {
                //                    FeedsEvenProducts.Add(new CustomProduct
                //                    {
                //                        Product = product,
                //                        ImageUrl = product.ImageUrls.FirstOrDefault() != null ? new Uri(product.ImageUrls.FirstOrDefault()) : null
                //                    });
                //                }
                //                else
                //                {
                //                    FeedsOddsProducts.Add(new CustomProduct
                //                    {
                //                        Product = product,
                //                        ImageUrl = product.ImageUrls.FirstOrDefault() != null ? new Uri(product.ImageUrls.FirstOrDefault()) : null
                //                    });
                //                }

                //            }

                //            FeedContentVisible = true;
                //        }
                //    }
                //}

                IsLoading = true;
                await Task.Delay(2000);
                var products = new ObservableCollection<CustomProduct>
                {
                        new CustomProduct
                        {
                             Product=new Product
                            {
                                Id=Guid.NewGuid(),
                                Name="Skin Accumax & Trade",
                                Summary="A nutritional supplement which works from within for clear, flawless skin.",
                                ImageUrls=new List<string>
                                {
                                    "https://via.placeholder.com/150"
                                },
                                Properties=new List<ProductProperty>
                                {
                                    new ProductProperty
                                    {
                                        ImageUrl="https://via.placeholder.com/64",
                                        Detail="Vegan"
                                    },
                                     new ProductProperty
                                    {
                                        ImageUrl="https://via.placeholder.com/64",
                                        Detail="Vegan"
                                    },
                                      new ProductProperty
                                    {
                                        ImageUrl="https://via.placeholder.com/64",
                                        Detail="Vegan"
                                    },
                                       new ProductProperty
                                    {
                                        ImageUrl="https://via.placeholder.com/64",
                                        Detail="Vegan"
                                    }

                                },
                                SkinConcerns=new List<string>
                                {
                                     "Redness/Sensitivity",
                            "Lack of Radiance"
                                }
                            },

                             ImageUrl=new Uri("https://via.placeholder.com/150")
                        },
                         new CustomProduct
                        {
                             Product=new Product
                            {
                                Id=Guid.NewGuid(),
                                Name="Hydrating Clay Masque",
                                Summary="Assists in absorbing excess oils and micro-exfoliating the skin.",
                                ImageUrls=new List<string>
                                {
                                    "https://via.placeholder.com/150"
                                },
                                Properties=new List<ProductProperty>
                                {
                                    new ProductProperty
                                    {
                                        ImageUrl="https://via.placeholder.com/64",
                                        Detail="Vegan"
                                    },
                                     new ProductProperty
                                    {
                                        ImageUrl="https://via.placeholder.com/64",
                                        Detail="Vegan"
                                    },
                                      new ProductProperty
                                    {
                                        ImageUrl="https://via.placeholder.com/64",
                                        Detail="Vegan"
                                    },
                                       new ProductProperty
                                    {
                                        ImageUrl="https://via.placeholder.com/64",
                                        Detail="Vegan"
                                    }
                                },
                                SkinConcerns=new List<string>
                                {
                                     "Redness/Sensitivity",
                            "Lack of Radiance"
                                }
                            },

                             ImageUrl=new Uri("https://via.placeholder.com/150")
                        }
                         ,
                          new CustomProduct
                        {
                             Product=new Product
                            {
                                Id=Guid.NewGuid(),
                                Name="Skin Accumax & Trade",
                                Summary="A nutritional supplement which works from within for clear, flawless skin.",
                                ImageUrls=new List<string>
                                {
                                    "https://via.placeholder.com/150"
                                },
                                Properties=new List<ProductProperty>
                                {
                                    new ProductProperty
                                    {
                                        ImageUrl="https://via.placeholder.com/64",
                                        Detail="Vegan"
                                    },
                                     new ProductProperty
                                    {
                                        ImageUrl="https://via.placeholder.com/64",
                                        Detail="Vegan"
                                    },
                                      new ProductProperty
                                    {
                                        ImageUrl="https://via.placeholder.com/64",
                                        Detail="Vegan"
                                    },
                                       new ProductProperty
                                    {
                                        ImageUrl="https://via.placeholder.com/64",
                                        Detail="Vegan"
                                    }

                                },
                                SkinConcerns=new List<string>
                                {
                                     "Redness/Sensitivity",
                            "Lack of Radiance"
                                }
                            },

                             ImageUrl=new Uri("https://via.placeholder.com/150")
                        },
                         new CustomProduct
                        {
                             Product=new Product
                            {
                                Id=Guid.NewGuid(),
                                Name="Hydrating Clay Masque",
                                Summary="Assists in absorbing excess oils and micro-exfoliating the skin.",
                                ImageUrls=new List<string>
                                {
                                    "https://via.placeholder.com/150"
                                },
                                Properties=new List<ProductProperty>
                                {
                                    new ProductProperty
                                    {
                                        ImageUrl="https://via.placeholder.com/64",
                                        Detail="Vegan"
                                    },
                                     new ProductProperty
                                    {
                                        ImageUrl="https://via.placeholder.com/64",
                                        Detail="Vegan"
                                    },
                                      new ProductProperty
                                    {
                                        ImageUrl="https://via.placeholder.com/64",
                                        Detail="Vegan"
                                    },
                                       new ProductProperty
                                    {
                                        ImageUrl="https://via.placeholder.com/64",
                                        Detail="Vegan"
                                    }
                                },
                                SkinConcerns=new List<string>
                                {
                                     "Redness/Sensitivity",
                            "Lack of Radiance"
                                }
                            },

                             ImageUrl=new Uri("https://via.placeholder.com/150")
                        },
                          new CustomProduct
                        {
                             Product=new Product
                            {
                                Id=Guid.NewGuid(),
                                Name="Skin Accumax & Trade",
                                Summary="A nutritional supplement which works from within for clear, flawless skin.",
                                ImageUrls=new List<string>
                                {
                                    "https://via.placeholder.com/150"
                                },
                                Properties=new List<ProductProperty>
                                {
                                    new ProductProperty
                                    {
                                        ImageUrl="https://via.placeholder.com/64",
                                        Detail="Vegan"
                                    },
                                     new ProductProperty
                                    {
                                        ImageUrl="https://via.placeholder.com/64",
                                        Detail="Vegan"
                                    },
                                      new ProductProperty
                                    {
                                        ImageUrl="https://via.placeholder.com/64",
                                        Detail="Vegan"
                                    },
                                       new ProductProperty
                                    {
                                        ImageUrl="https://via.placeholder.com/64",
                                        Detail="Vegan"
                                    }

                                },
                                SkinConcerns=new List<string>
                                {
                                     "Redness/Sensitivity",
                            "Lack of Radiance"
                                }
                            },

                             ImageUrl=new Uri("https://via.placeholder.com/150")
                        },
                         new CustomProduct
                        {
                             Product=new Product
                            {
                                Id=Guid.NewGuid(),
                                Name="Hydrating Clay Masque",
                                Summary="Assists in absorbing excess oils and micro-exfoliating the skin.",
                                ImageUrls=new List<string>
                                {
                                    "https://via.placeholder.com/150"
                                },
                                Properties=new List<ProductProperty>
                                {
                                    new ProductProperty
                                    {
                                        ImageUrl="https://via.placeholder.com/64",
                                        Detail="Vegan"
                                    },
                                     new ProductProperty
                                    {
                                        ImageUrl="https://via.placeholder.com/64",
                                        Detail="Vegan"
                                    },
                                      new ProductProperty
                                    {
                                        ImageUrl="https://via.placeholder.com/64",
                                        Detail="Vegan"
                                    },
                                       new ProductProperty
                                    {
                                        ImageUrl="https://via.placeholder.com/64",
                                        Detail="Vegan"
                                    }
                                },
                                SkinConcerns=new List<string>
                                {
                                     "Redness/Sensitivity",
                            "Lack of Radiance"
                                }
                            },

                             ImageUrl=new Uri("https://via.placeholder.com/150")
                        }
                };

                foreach (var item in products)
                {
                    var index = products.IndexOf(item);
                    if (index % 2 == 0)
                    {
                        FeedsEvenProducts.Add(item);
                    }
                    else
                    {
                        FeedsOddsProducts.Add(item);
                    }
                }
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
                if (Salon == null||Salon.Id==Guid.Empty)
                {
                    Salon = await ApiServices.Client.GetAsync<Salon>("salon");
                }
                if (Salon.Products != null && Salon.Products.Count > 0)
                {
                    foreach (var item in Salon.Products)
                    {
                        if (item.Name.ToLower() == "fortify")
                        {
                            foreach (var product in item.Products)
                            {
                                FortifyProducts.Add(new CustomProduct
                                {
                                    Product = product,
                                    ImageUrl = product.ImageUrls.FirstOrDefault() != null ? new Uri(product.ImageUrls.FirstOrDefault()) : null
                                });
                            }
                        }
                    }
                }
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        private async void FetchFinishContentsAsync()
        {
            try
            {
                if (Salon == null)
                {
                    Salon = await ApiServices.Client.GetAsync<Salon>("salon");
                }
                if (Salon.Products != null && Salon.Products.Count > 0)
                {
                    foreach (var item in Salon.Products)
                    {
                        if (item.Name.ToLower() == "feed")
                        {
                            foreach (var product in item.Products)
                            {
                                //FeedsProducts.Add(new CustomProduct
                                //{
                                //    Product = product,
                                //    ImageUrl = product.ImageUrls.FirstOrDefault() != null ? new Uri(product.ImageUrls.FirstOrDefault()) : null
                                //});
                            }

                            FeedContentVisible = true;
                        }
                        if (item.Name.ToLower() == "fortify")
                        {
                            foreach (var product in item.Products)
                            {
                                FortifyProducts.Add(new CustomProduct
                                {
                                    Product = product,
                                    ImageUrl = product.ImageUrls.FirstOrDefault() != null ? new Uri(product.ImageUrls.FirstOrDefault()) : null
                                });
                            }
                        }
                        if (item.Name.ToLower() == "finish")
                        {
                            foreach (var product in item.Products)
                            {
                                FinishProducts.Add(new CustomProduct
                                {
                                    Product = product,
                                    ImageUrl = product.ImageUrls.FirstOrDefault() != null ? new Uri(product.ImageUrls.FirstOrDefault()) : null
                                });
                            }
                        }

                    }
                }
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
