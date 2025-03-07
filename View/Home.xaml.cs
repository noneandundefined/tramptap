using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using tramptap.Internal.Interfaces;
using tramptap.Internal.Repository;

namespace tramptap.View
{
    /// <summary>
    /// Логика взаимодействия для Home.xaml
    /// </summary>
    public partial class Home : UserControl, INotifyPropertyChanged
    {
        private readonly ClickInterface _click;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int Counter
        {
            get { return this._click.ReadClick(); }
            set
            {
                this._click.WriteClick();
                OnPropertyChanged(nameof(Counter));
            }
        }



        public Home()
        {
            InitializeComponent();

            DataContext = this;
            this._click = new ClickRepository();
        }

        private void BtnClickTap_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Counter++;

            Point clickPosition = e.GetPosition(ClickEffectGrid);

            TextBlock floatingText = new TextBlock
            {
                Text = "+ 1",
                FontSize = 30,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#004b9a")),
                Style = (Style)Application.Current.Resources["Font"],
                FontWeight = FontWeights.Bold
            };

            floatingText.Margin = new Thickness(clickPosition.X, clickPosition.Y, 0, 0);
            ClickEffectGrid.Children.Add(floatingText);

            floatingText.IsHitTestVisible = false;

            ThicknessAnimation moveUp = new ThicknessAnimation
            {
                From = floatingText.Margin,
                To = new Thickness(floatingText.Margin.Left, floatingText.Margin.Top - 300, floatingText.Margin.Right, floatingText.Margin.Bottom),
                Duration = TimeSpan.FromSeconds(3),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            DoubleAnimation fadeOut = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.5),
                BeginTime = TimeSpan.FromSeconds(0.3)
            };

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(moveUp);
            storyboard.Children.Add(fadeOut);

            // Устанавливаем цель для анимаций
            Storyboard.SetTarget(moveUp, floatingText);
            Storyboard.SetTarget(fadeOut, floatingText);
            Storyboard.SetTargetProperty(moveUp, new PropertyPath(TextBlock.MarginProperty));
            Storyboard.SetTargetProperty(fadeOut, new PropertyPath(UIElement.OpacityProperty));

            // Обработчик завершения анимации
            fadeOut.Completed += (s, args) =>
            {
                // Удаляем элемент после завершения анимации
                ClickEffectGrid.Children.Remove(floatingText);
            };

            // Запускаем анимацию
            storyboard.Begin();
        }
    }
}
