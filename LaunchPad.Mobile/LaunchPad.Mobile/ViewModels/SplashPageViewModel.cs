using FormsControls.Base;
using IIAADataModels.Transfer;
using LaunchPad.Client;
using LaunchPad.Mobile.Helpers;
using LaunchPad.Mobile.Models;
using LaunchPad.Mobile.Services;
using LaunchPad.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LaunchPad.Mobile.ViewModels
{
    public class SplashPageViewModel : ViewModelBase
    {
        public static Action<int> UpdateProgress;
        public static void OnUpdateProgress(int param)
        {
            UpdateProgress?.Invoke(param);
        }
        private IDatabaseServices DatabaseServices => DependencyService.Get<IDatabaseServices>();
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }
        private double _progress = 0.0;
        public double Progress
        {
            get => _progress;
            set => SetProperty(ref _progress, value);
        }

        private bool _progressBarVisible;
        public bool ProgressBarVisible
        {
            get => _progressBarVisible;
            set => SetProperty(ref _progressBarVisible, value);
        }


        public SplashPageViewModel()
        {
            GetInitialDataAsync();
        }
        private async void GetInitialDataAsync()
        {
            IsBusy = true;
            await Task.Delay(1000);
            Device.BeginInvokeOnMainThread(() => ExceptionHandler(async () =>
            {
                ProgressBarVisible = true;
                await Task.Delay(200);
                Progress = 0.10;
                var salon = await ApiServices.Client.GetAsync<Salon>("salon");
                Progress = 0.25;
                if (salon != null || salon.Id != Guid.Empty)
                {
                    App.SalonName = salon.Name;
                    Settings.SalonName = salon.Name;
                    var result = await DatabaseServices.InsertData("salon", salon);
                    if (result)
                    {
                        Console.WriteLine("salon stored to local cache");
                    }

                }
                var consumers = await ApiServices.Client.GetAsync<List<Consumer>>("Salon/Consumers");
                Progress = 0.50;
                if (consumers?.Count>0)
                {
                    var result = await DatabaseServices.InsertData("consumers", consumers);
                    if (result)
                    {
                        Console.WriteLine("consumers stored to local cache");
                    }

                }

                Progress = 0.75;
                IsBusy = false;
                var locationStatus = await Permissions.CheckStatusAsync<Permissions.LocationAlways>();

                if (locationStatus != PermissionStatus.Granted)
                {
                    locationStatus = await Permissions.RequestAsync<Permissions.LocationAlways>();
                }
                var locationInUseStatus = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

                if (locationInUseStatus != PermissionStatus.Granted)
                {
                    locationInUseStatus = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                }
                var camerastatus = await Permissions.CheckStatusAsync<Permissions.Camera>();

                if (camerastatus != PermissionStatus.Granted)
                {
                    camerastatus = await Permissions.RequestAsync<Permissions.Camera>();
                }

                var flashLightStatus = await Permissions.CheckStatusAsync<Permissions.Flashlight>();
                if (flashLightStatus != PermissionStatus.Granted)
                {
                    await Task.Delay(500);
                    flashLightStatus = await Permissions.RequestAsync<Permissions.Flashlight>();
                }

                Progress = 1;
                await Task.Delay(100);
                Settings.ClientName = "Sarah Smith";
                var currentTherapist = await SecureStorage.GetAsync("currentTherapist");
                if (!string.IsNullOrEmpty(currentTherapist))
                {
                    Application.Current.MainPage = new AnimationNavigationPage(new SalonClientsPage());
                }
                else
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new SignInPage());
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
