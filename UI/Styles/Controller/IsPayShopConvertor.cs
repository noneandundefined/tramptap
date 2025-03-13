using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace tramptap.UI.Styles.Controller
{
    public class IsPayShopConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is long balance && parameter is string costString && int.TryParse(costString, out int cost))
            {
                return balance >= cost ? new SolidColorBrush(Color.FromRgb(0, 124, 255)) : new SolidColorBrush(Colors.Gray);
            }

            return new SolidColorBrush(Colors.Gray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
