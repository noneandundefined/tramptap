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

        private ImageSource _currentImage;
        private Brush _currentBackground;

        private readonly ImageSource[] _images = {
            new BitmapImage(new Uri("pack://application:,,,/Public/images/delivery-passive.png", UriKind.Absolute)),
            new BitmapImage(new Uri("pack://application:,,,/Public/images/curier-passive.png", UriKind.Absolute)),
            new BitmapImage(new Uri("pack://application:,,,/Public/images/mac-passive.png", UriKind.Absolute)),
            new BitmapImage(new Uri("pack://application:,,,/Public/images/prog-passive.png", UriKind.Absolute)),
            new BitmapImage(new Uri("pack://application:,,,/Public/images/social-passive.png", UriKind.Absolute)),
            new BitmapImage(new Uri("pack://application:,,,/Public/images/space-passive.png", UriKind.Absolute)),
            new BitmapImage(new Uri("pack://application:,,,/Public/images/world-passive.png", UriKind.Absolute)),
        };

        private readonly Brush[] _backgrounds = {
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffc800")),
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff8600")),
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff0000")),
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#c800ff")),
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4500ff")),
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00c1ff")),
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00d319")),
        };

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

        public ImageSource CurrentImage
        {
            get { return _currentImage; }
            set
            {
                _currentImage = value;
                OnPropertyChanged(nameof(CurrentImage));
            }
        }

        public Brush CurrentBackground
        {
            get { return _currentBackground; }
            set
            {
                _currentBackground = value;
                OnPropertyChanged(nameof(CurrentBackground));
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

            (short pass, decimal pricePass) = ShopRepository.GetPriceForPassive().Value;
            if (pass >= 9820)
            {
                PassiveCountPay.Content = $"МАКС.";
            }
            else
            {
                PassiveCountPay.Content = $"+{pass} | -{pricePass} AUDI";
            }

            try
            {
                int key = ShopRepository.GetNextKeyPassive();
                CurrentImage = _images[key];
                CurrentBackground = _backgrounds[key];
            }
            catch (Exception ex)
            {
                CurrentImage = _images[6];
                CurrentBackground = _backgrounds[6];
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

        private void btnPayPassive_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                (short pass, decimal price) = ShopRepository.GetPriceForPassive().Value;

                if (price > ClickRepository.ReadClick())
                {
                    return;
                }

                ClickRepository.WritePassiveClick(pass);
                CurrentImage = this._images[ClickRepository.PassiveClick() % this._images.Length];
                CurrentBackground = this._backgrounds[ClickRepository.PassiveClick() % this._backgrounds.Length];

                ClickRepository.PayPassive(pass, price);
                ShopInitial();
            }
            catch (Exception ex) { }
        }
    }
}
