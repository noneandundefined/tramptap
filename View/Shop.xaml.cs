using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class Shop : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public long Balance
        {
            get => ClickRepository.ReadClick();
            set
            {
                OnPropertyChanged(nameof(Balance));
            }
        }

        public decimal ClickUp
        {
            get
            {
                (short _, decimal price) = ShopRepository.GetPriceForTap().Value;
                return price;
            }
            set
            {
                OnPropertyChanged(nameof(Balance));
            }
        }

        public Shop()
        {
            InitializeComponent();

            ShopInitial();
            DataContext = this;
        }

        private void ShopInitial()
        {
            try
            {
                (short key, decimal value) = ShopRepository.GetPriceForTap().Value;
                TapCountPay.Content = $"+{key} | -{value} AUDI";
            }
            catch (Exception ex) { }
        }

        private void btnPayTap_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                (short tap, decimal price) = ShopRepository.GetPriceForTap().Value;

                if (price > ClickRepository.ReadClick())
                {
                    return;
                }

                ClickRepository.PayClick(tap, price);
                ShopInitial();
            } 
            catch (Exception ex) { }
        }
    }
}
