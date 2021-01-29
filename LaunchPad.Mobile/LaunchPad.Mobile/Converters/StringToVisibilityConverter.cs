using System;
using System.Globalization;
using Xamarin.Forms;

namespace LaunchPad.Mobile.Converters
{
    public class StringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;
            else
            {
                var text = value.ToString();
                if (!string.IsNullOrEmpty(text))
                {
                    return true;
                }
                else
                    return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
