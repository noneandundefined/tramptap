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
    public class ActivePanelToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string activePanel = value as string;
            string panelTitle = parameter as string;

            return activePanel == panelTitle ? new SolidColorBrush(Color.FromRgb(153, 153, 153)) : new SolidColorBrush(Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
