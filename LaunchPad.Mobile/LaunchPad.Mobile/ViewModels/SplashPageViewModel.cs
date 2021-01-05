using FormsControls.Base;
using IIAADataModels.Transfer;
using LaunchPad.Client;
using LaunchPad.Mobile.Services;
using LaunchPad.Mobile.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LaunchPad.Mobile.ViewModels
{
    public class SplashPageViewModel:ViewModelBase
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
                await Task.Delay(200);
                Progress = 0.25;
                var salon = await ApiServices.Client.GetAsync<Salon>("salon");
                Progress = 0.50;
                if (salon != null || salon.Id != Guid.Empty)
                {
                    var result=await DatabaseServices.InsertData("salon", salon);
                    if (result)
                    {
                        Console.WriteLine("salon stored to local cache");
                    }
                    
                }

                Progress = 0.75;
                IsBusy = false;
                var status = await Permissions.CheckStatusAsync<Permissions.Camera>();

                if (status != PermissionStatus.Granted)
                {
                    await Task.Delay(500);
                    status = await Permissions.RequestAsync<Permissions.Camera>();
                }

                var flashLightStatus = await Permissions.CheckStatusAsync<Permissions.Flashlight>();
                if (status != PermissionStatus.Granted)
                {
                    await Task.Delay(500);
                    status = await Permissions.RequestAsync<Permissions.Flashlight>();
                }

                Progress = 1;
                await Task.Delay(100);
                Application.Current.MainPage = new AnimationNavigationPage(new SalonProductsPage());
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
