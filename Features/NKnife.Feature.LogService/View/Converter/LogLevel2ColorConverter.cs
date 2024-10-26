using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using NLog;

namespace NKnife.Feature.LogService.View.Converter
{
    public class LogLevel2ColorConverter : IValueConverter
    {
        public object? Convert(object? value,
                               Type targetType,
                               object? parameter,
                               CultureInfo culture)
        {
            if (value is LogLevel level
                && parameter is string p)
            {
                var ordinal = level.Ordinal;

                switch (p)
                {
                    case "Foreground":
                        switch (ordinal)
                        {
                            case 5:
                            case 4:
                                return Brushes.White;
                            case 1:
                                return Brushes.DimGray;
                            case 0:
                                return Brushes.DarkGray;
                            case 3:
                            case 2:
                            default:
                                return SystemColors.ControlTextBrush;
                        }
                    case "Background":
                        switch (ordinal)
                        {
                            case 5:
                                return Brushes.DarkRed;
                            case 4:
                                return Brushes.PaleVioletRed;
                            case 3:
                                return Brushes.LightGoldenrodYellow;
                            case 2:
                            case 1:
                            case 0:
                            default:
                                return SystemColors.ControlLightLightBrush;
                        }
                }
            }

            return Brushes.Gray;
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