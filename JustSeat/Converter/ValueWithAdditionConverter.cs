using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace JustSeat.Converter
{
    public class ValueWithAdditionConverter : MarkupExtension, IValueConverter
    {
        public double AdditionValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double result = 0;
            if (value != null && value is double)
                result = (double)value + AdditionValue;

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double result = 0;
            if (value != null && value is double)
                result = (double)value - AdditionValue;

            return result;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
