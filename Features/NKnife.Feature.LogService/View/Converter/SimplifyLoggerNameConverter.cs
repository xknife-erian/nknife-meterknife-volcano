using System.Globalization;
using System.Windows.Data;

namespace LEIAO.Feature.LogService.View.Converter
{
    public class SimplifyLoggerNameConverter : IValueConverter
    {
        public object? Convert(object? value,
                               Type targetType,
                               object? parameter,
                               CultureInfo culture)
        {
            if (value is string caller)
            {
                if (caller.Contains("LEIAO.Module"))
                    return caller.Replace("LEIAO.Module.", "");
                else if (caller.Contains("LEIAO.Kernel"))
                    return caller.Replace("LEIAO.Kernel.", "");
                else if (caller.Contains("LEIAO.Mercury"))
                    return caller.Replace("LEIAO.Mercury.", "");
                else if (caller.Contains("LEIAO.Device"))
                    return caller.Replace("LEIAO.Device.", "");
            }

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