// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Xamarin.Forms.Xaml;

namespace LaunchPad.Mobile.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    /// 
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string SettingsKey = "settings_key";
        private const string UsernameKey = "current_user_name";
        private const string SalonnameKey = "salon_name_key";
        private const string ClientNameKey = "client_name_key";
        private static readonly string SettingsDefault = string.Empty;

        #endregion


        public static string GeneralSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsKey, value);
            }
        }
        public static string CurrentUserName
        {
            get
            {
                return AppSettings.GetValueOrDefault(UsernameKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UsernameKey, value);
            }
        }
        public static string SalonName
        {
            get
            {
                return AppSettings.GetValueOrDefault(SalonnameKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SalonnameKey, value);
            }
        }
        public static string ClientName
        {
            get
            {
                return AppSettings.GetValueOrDefault(ClientNameKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(ClientNameKey, value);
            }
        }

    }
}
