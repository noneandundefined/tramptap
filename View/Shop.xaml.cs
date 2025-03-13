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
            DataContext = this;
            ShopInitial();
        }

        private void ShopInitial()
        {
            (short tap, decimal priceTap) = ShopRepository.GetPriceForTap().Value;
            if (tap == 100)
            {
                TapCountPay.Content = $"МАКС.";
            }
            else
            {
                TapCountPay.Content = $"+{tap} | -{priceTap} AUDI";
            }

            (short energy, decimal priceEnergy) = ShopRepository.GetPriceForEnergy().Value;
            if (energy == 30000)
            {
                EnergyCountPay.Content = $"МАКС.";
            }
            else
            {
                EnergyCountPay.Content = $"+{energy} | -{priceEnergy} AUDI";
            }
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

        private void btnPayEnergy_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                (short energy, decimal price) = ShopRepository.GetPriceForEnergy().Value;

                if (price > ClickRepository.ReadClick())
                {
                    return;
                }

                ClickRepository.PayEnergyLimit(energy, price);
                ShopInitial();
            }
            catch (Exception ex) { }
        }
    }
}
