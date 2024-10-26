using System.Globalization;
using System.Windows;
using System.Windows.Data;
using NLog;

namespace NKnife.Feature.LogService.View.Converter
{
    public class Logger2VisibilityConverter : IMultiValueConverter
    {
        /// <inheritdoc />
        public object Convert(object[] values,
                              Type targetType,
                              object parameter,
                              CultureInfo culture)
        {
            if(values is not [LogLevel level, _, _, _, _, _, _])
                return Visibility.Collapsed;
            var ordinal = level.Ordinal;
                    
            if (values[1] is bool and true && ordinal == 0)
            {
                return Visibility.Visible;
            }
            if (values[2] is bool and true && ordinal == 1)
            {
                return Visibility.Visible;
            }
            if (values[3] is bool and true && ordinal == 2)
            {
                return Visibility.Visible;
            }
            if (values[4] is bool and true && ordinal == 3)
            {
                return Visibility.Visible;
            }
            if (values[5] is bool and true && ordinal == 4)
            {
                return Visibility.Visible;
            }
            if (values[6] is bool and true && ordinal == 5)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        /// <inheritdoc />
        public object[] ConvertBack(object value,
                                    Type[] targetTypes,
                                    object parameter,
                                    CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}