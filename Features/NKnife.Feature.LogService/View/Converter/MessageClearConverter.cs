using System.Globalization;
using System.Windows.Data;

namespace LEIAO.Feature.LogService.View.Converter
{
    public class MessageClearConverter : IValueConverter
    {
        public object? Convert(object? value,
                               Type targetType,
                               object? parameter,
                               CultureInfo culture)
        {
            if (value is string log)
                return log.Replace("\r\n", " ");

            return value;
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