using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using tramptap.Internal.Repository;

namespace tramptap.View
{
    /// <summary>
    /// Логика взаимодействия для Elections.xaml
    /// </summary>
    public partial class Elections : UserControl
    {
        public Elections()
        {
            InitializeComponent();
        }

        private async void Start_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Random random = new Random();
            int min = 100;
            int max = 114_000_000;
            int randomNumber = random.Next(min, max);

            if (ClickRepository.ReadClick() > randomNumber)
            {
                WinLoseImage.Source = new BitmapImage(new Uri("pack://application:,,,/Public/images/tramp_scin_5.png", UriKind.Absolute));
                WinLoseLabel.Content = "Победу одержал Трамп!";

                ClickRepository.WriteClickPass((long)randomNumber);
            }
            else
            {
                WinLoseImage.Source = new BitmapImage(new Uri("pack://application:,,,/Public/images/kamlla_h.png", UriKind.Absolute));
                WinLoseLabel.Content = "Камалла победила!";

                ClickRepository.ZeroClick();
            }

            await Task.Delay(1500).ContinueWith(t =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    WinLoseImage.Source = null;
                    WinLoseLabel.Content = "";
                });
            });
        }
    }
}
