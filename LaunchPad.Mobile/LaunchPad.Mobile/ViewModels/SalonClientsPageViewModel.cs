using IIAADataModels.Transfer;
using LaunchPad.Mobile.Helpers;
using LaunchPad.Mobile.Services;
using LaunchPad.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace LaunchPad.Mobile.ViewModels
{
    public class SalonClientsPageViewModel : ViewModelBase
    {
        private IDatabaseServices DatabaseServices => DependencyService.Get<IDatabaseServices>();
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }
        private ObservableCollection<AlphabetsModel> _alphabetsCollection;
        public ObservableCollection<AlphabetsModel> AlphabetsCollection
        {
            get => _alphabetsCollection;
            set => SetProperty(ref _alphabetsCollection, value);
        }
         
        private ObservableCollection<Consumer> _consumerCollection;
        public ObservableCollection<Consumer> ConsumerCollection
        {
            get => _consumerCollection;
            set => SetProperty(ref _consumerCollection, value);
        }
        
        private ObservableCollection<RecentConsumer> _recentConsumerCollection;
        public ObservableCollection<RecentConsumer> RecentConsumerCollection
        {
            get => _recentConsumerCollection;
            set => SetProperty(ref _recentConsumerCollection, value);
        }

        private Consumer _selectedConsumer;
        public Consumer SelectedConsumer
        {
            get => _selectedConsumer;
            set => SetProperty(ref _selectedConsumer, value);
        }

        public ICommand SelectConsumerCommand => new Command<Consumer>((param) => GoToSalonProductPageAsync(param));
        public ICommand NewClientCommand => new Command(() => GoToCLientRegistrationPage());
        public ICommand RefreshCommand => new Command(() => FetchConsumerCollectionAsync());

        public SalonClientsPageViewModel()
        {
            AlphabetsCollection = new ObservableCollection<AlphabetsModel>();
            char[] az = Enumerable.Range('a', 'z' - 'a' + 1).Select(i => (Char)i).ToArray();
            foreach (var c in az)
            {
                AlphabetsCollection.Add(new AlphabetsModel
                {
                    Alphabet = c.ToString(),
                    AlphabetSelectedCommand = new Command<string>((param) =>
                    {
                        FilterByAlphabetAsync(param);
                    }),
                    RemoveSelectedCommand = new Command<string>((param) =>
                    {
                        FilterByAlphabetAsync(param);
                    })
                });
            }
            FetchConsumerCollectionAsync();

        }

        private void FetchConsumerCollectionAsync()
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                
                Device.BeginInvokeOnMainThread(async() =>
                {
                    var consumers = await DatabaseServices.Get<List<Consumer>>("consumers");
                    if (consumers.Count > 0)
                    {
                        ConsumerCollection = new ObservableCollection<Consumer>(consumers);
                    }
                    var recentConsumers = await DatabaseServices.Get<List<RecentConsumer>>("recentconsumers");
                    if (recentConsumers.Count > 0)
                    {

                        RecentConsumerCollection = new ObservableCollection<RecentConsumer>(recentConsumers);
                    }
                });
               
            }
            catch (Exception)
            {

            }
            IsBusy = false;
        }

        private async void FilterByAlphabetAsync(string param)
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                if (!string.IsNullOrEmpty(param))
                {
                    AlphabetsCollection.Where(a => a.Alphabet != param).ForEach(x => x.IsSelected = false);
                    var consumers = await DatabaseServices.Get<List<Consumer>>("consumers");
                    if (consumers.Count > 0)
                    {
                        ConsumerCollection = new ObservableCollection<Consumer>(consumers.Where(a => a.Lastname.ToLower().StartsWith(param.ToLower())));
                    }
                }
                else
                {
                    AlphabetsCollection.ForEach(x => x.IsSelected = false);
                    var consumers = await DatabaseServices.Get<List<Consumer>>("consumers");
                    if (consumers.Count > 0)
                    {
                        ConsumerCollection = new ObservableCollection<Consumer>(consumers);
                    }
                }
               
            }
            catch (Exception ex)
            {

            }
            IsBusy = false;
        }

        private void GoToSalonProductPageAsync(Consumer param)
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                Task.Run(async () =>
                {
                    if (param != null)
                    {
                        var isStored = await DatabaseServices.InsertData<Consumer>("current_consumer", param);
                        if (isStored)
                        {
                            Settings.ClientId = param.Id.ToString();
                            Settings.ClientFirstName = $"{param.Firstname}";
                            Settings.ClientName = $"{param.Firstname} {param.Lastname}";
                            Settings.ClientHeader = $"{param.Firstname}'s Skin Healt Plan";
                            var recentConsumer = new RecentConsumer
                            {
                                Consumer=param,
                                LastLoginDateTime=DateTime.Now.ToString("dd/MM/yyyy - hh:mm tt")
                            };

                            var recentConsumerList = await DatabaseServices.Get<List<RecentConsumer>>("recentconsumers");
                            if (recentConsumerList.Count == 0)
                            {
                                recentConsumerList.Add(recentConsumer);
                            }
                            else
                            {
                                var consumer = recentConsumerList.FirstOrDefault(a => a.Consumer.Id == param.Id);
                                if (consumer != null)
                                {
                                    recentConsumerList.Remove(consumer);
                                    recentConsumerList.Insert(0, recentConsumer);
                                }
                                else
                                {
                                    recentConsumerList.Add(recentConsumer);
                                }
                            }

                            await DatabaseServices.InsertData<List<RecentConsumer>>("recentconsumers",recentConsumerList);
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                Application.Current.MainPage.Navigation.PushAsync(new SalonProductsPage());
                            });
                        }
                    }
                });

            }
            catch (Exception)
            {

            }
            IsBusy = false;
        }

        private void GoToCLientRegistrationPage()
        {
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Application.Current.MainPage.Navigation.PushAsync(new ClientRegistrationPage());
                });
            }
            catch (Exception)
            {

            }
        }


    }

    public class RecentConsumer
    {
        public Consumer Consumer { get; set; }
        public string LastLoginDateTime { get; set; }
    }
    public class AlphabetsModel:ViewModelBase
    {
        public string Alphabet { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public ICommand SelectCommand => new Command<string>((param) =>
          {
              IsSelected = true;
              AlphabetSelectedCommand.Execute(param);
          }); 
        public ICommand RemoveSelectionCommand => new Command<string>((param) =>
          {
              IsSelected = false;
              RemoveSelectedCommand.Execute(param);
          });
        public ICommand AlphabetSelectedCommand { get; set; }
        public ICommand RemoveSelectedCommand { get; set; }
    }
}
