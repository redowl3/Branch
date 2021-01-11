using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using FFImageLoading.Forms.Platform;
using Plugin.CurrentActivity;
using Xamarin.Forms;

namespace LaunchPad.Mobile.Droid
{
    [Activity(Label = "Eventis", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true,ScreenOrientation =ScreenOrientation.Landscape|ScreenOrientation.ReverseLandscape, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
           
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Forms.SetFlags("SwipeView_Experimental");
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            global::Xamarin.Forms.FormsMaterial.Init(this, savedInstanceState);
            CachedImageRenderer.Init(true);
            CachedImageRenderer.InitImageViewHandler();
            FormsControls.Droid.Main.Init(this);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}