using System.Globalization;
using System.Windows.Data;
using Xceed.Wpf.Toolkit;

namespace LEIAO.Feature.LogService.View.Converter
{
    public class Bool2WindowStateConverter : IValueConverter
    {
        public object? Convert(object? value,
                               Type targetType,
                               object? parameter,
                               CultureInfo culture)
        {
            if (value is bool bl)
            {
                return !bl ? WindowState.Open : WindowState.Closed;
            }

            return WindowState.Closed;
        }

        public object? ConvertBack(object? value,
                                   Type targetType,
                                   object? parameter,
                                   CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}