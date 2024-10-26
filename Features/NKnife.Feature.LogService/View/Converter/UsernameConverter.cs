using System.Globalization;
using System.Windows.Data;

namespace NKnife.Feature.LogService.View.Converter
{
    public class UsernameConverter : IValueConverter
    {
        public object? Convert(object? value,
                               Type targetType,
                               object? parameter,
                               CultureInfo culture)
        {
            if (value is string username)
            {
                return username;
            }

            return "System";
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