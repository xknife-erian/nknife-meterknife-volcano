using System.Globalization;
using System.Windows.Data;
using NLog;

namespace LEIAO.Feature.LogService.View.Converter
{
    public class Logger2ToolTipConverter : IValueConverter
    {
        public object? Convert(object? value,
                               Type targetType,
                               object? parameter,
                               CultureInfo culture)
        {
            if (value is LogEventInfo log)
                return $"{log.TimeStamp}\n{log.Level}\n{log.FormattedMessage}\n{log.LoggerName}";

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