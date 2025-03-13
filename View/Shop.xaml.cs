using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using tramptap.Internal.Repository;

namespace tramptap.View
{
    /// <summary>
    /// Логика взаимодействия для Shop.xaml
    /// </summary>
    public partial class Shop : UserControl
    {
        public Shop()
        {
            InitializeComponent();

            ShopInitial();
        }

        private void ShopInitial()
        {
            (short key, decimal value) = ShopRepository.GetPriceForTap().Value;

            TapCountPay.Content = $"+{key} | -{value} AUDI";
        }

        private void btnPayTap_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
