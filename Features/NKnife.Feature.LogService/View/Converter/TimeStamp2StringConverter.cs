using System.Globalization;
using System.Windows.Data;

namespace NKnife.Feature.LogService.View.Converter
{
    public class TimeStamp2StringConverter : IValueConverter
    {
        public object? Convert(object? value,
                               Type targetType,
                               object? parameter,
                               CultureInfo culture)
        {
            if (value is DateTime dt)
            {
                var ms = dt.ToString(@"ffffff").Insert(3, ".");
                return dt.ToString($"HH:mm:ss [{ms}]");
            }

            return string.Empty;
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