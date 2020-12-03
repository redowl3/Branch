using IIAADataModels.Transfer;
using LaunchPad.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LaunchPad.Mobile.ViewModels
{
    public class UserHealthPlanPageViewModel : ViewModelBase
    {
        private ObservableCollection<CustomProduct> _products;
        public ObservableCollection<CustomProduct> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }

        public UserHealthPlanPageViewModel()
        {
            Products = new ObservableCollection<CustomProduct>();
            Task.Run(() => FetchFeedsAsync());

        }

        private void FetchFeedsAsync()
        {
            try
            {
                Products = new ObservableCollection<CustomProduct>
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
                            Name="Skin Accumax & Trade;",
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
                            Name="Skin Accumax & Trade;",
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
                            Name="Skin Accumax & Trade;",
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
                            Name="Skin Accumax & Trade;",
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
                            Name="Skin Accumax & Trade;",
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
                            Name="Skin Accumax & Trade;",
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
                    }

            };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
