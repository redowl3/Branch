using FormsControls.Base;
using IIAADataModels.Transfer;
using LaunchPad.Client;
using LaunchPad.Mobile.Services;
using LaunchPad.Mobile.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LaunchPad.Mobile.ViewModels
{
    public class SplashPageViewModel:ViewModelBase
    {
        private IDatabaseServices DatabaseServices => DependencyService.Get<IDatabaseServices>();
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public SplashPageViewModel()
        {
            GetSalonAsync();
        }
        private void GetSalonAsync()
        {
            IsBusy = true;

            Task.Run(()=>ExceptionHandler(async() =>
            {
                var salon=await ApiServices.Client.GetAsync<Salon>("salon");
                if (salon != null || salon.Id != Guid.Empty)
                {
                    await DatabaseServices.InsertData("salon", salon);
                }
                Device.BeginInvokeOnMainThread(() => {
                    IsBusy = false;
                    Application.Current.MainPage=new AnimationNavigationPage(new SalonProductsPage());
                });
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
