using JustSeat.Model;
using JustSeat.Parameters;
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
    public class ChairWithinSetMultiValueConverter : MarkupExtension, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((values.Count() > 1) == false)
                return null;

            var table = values[0] as Table;
            var chair = values[1] as Chair;

            if (chair == null || table == null)
                return null;

            return new ChairRemovalParameter
            {
                Table = table,
                ChairToRemove = chair
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
