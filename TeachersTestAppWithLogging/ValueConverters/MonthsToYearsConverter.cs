using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachersTestAppWithLogging.ValueConverters
{
    public class MonthsToYearsConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if(value is null) return null;
            return ((int)value / 12).ToString();
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is null) return 0;
            int month;
            try
            {
                month = (int)(System.Convert.ToDouble((string)value) * 12);
            }
            catch
            {
                return 0;
            }
            return month;
        }
    }
}
