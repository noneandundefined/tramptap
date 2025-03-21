﻿using System;
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
using System.Windows.Threading;
using tramptap.Internal.Interfaces;
using tramptap.Internal.Repository;

namespace tramptap.View
{
    /// <summary>
    /// Логика взаимодействия для Home.xaml
    /// </summary>
    public partial class Home : UserControl, INotifyPropertyChanged
    {
        private bool _isPaused = false;
        private string _imageSource = "pack://application:,,,/Public/images/tramp_scin_2.png";

        public string ImageSource
        {
            get => _imageSource;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public long Counter
        {
            get => ClickRepository.ReadClick();
            set
            {
                ClickRepository.WriteClick();
                OnPropertyChanged(nameof(Counter));
                OnPropertyChanged(nameof(CounterDisplay));
                OnPropertyChanged(nameof(Energy));

                if (Counter >= 100)
                {
                    _imageSource = "pack://application:,,,/Public/images/tramp_scin_4.png";
                    OnPropertyChanged(nameof(ImageSource));
                }

                if (Counter >= 500)
                {
                    _imageSource = "pack://application:,,,/Public/images/tramp_scin_3.png";
                    OnPropertyChanged(nameof(ImageSource));
                }

                if (Counter >= 1000)
                {
                    _imageSource = "pack://application:,,,/Public/images/tramp_scin_5.png";
                    OnPropertyChanged(nameof(ImageSource));
                }
            }
        }

        public long Energy
        {
            get => ClickRepository.ReadEnergyCount();
            set
            {
                ClickRepository.WriteEnergy();
                OnPropertyChanged(nameof(Energy));
            }
        }

        public long Energy_Limit
        {
            get => ClickRepository.ReadEnergyCountLimit();
            set
            {
                OnPropertyChanged();
            }
        }

        public string CounterDisplay => $"{this.Counter} AUDI";

        public Home()
        {
            InitializeComponent();

            AppTimer.Instance.Tick += AppTimer_Tick;
            AppTimer.Instance.StartEnergy();

            DataContext = this;

            if (Counter >= 100)
            {
                _imageSource = "pack://application:,,,/Public/images/tramp_scin_4.png";
                OnPropertyChanged(nameof(ImageSource));
            }

            if (Counter >= 500)
            {
                _imageSource = "pack://application:,,,/Public/images/tramp_scin_3.png";
                OnPropertyChanged(nameof(ImageSource));
            }

            if (Counter >= 1000)
            {
                _imageSource = "pack://application:,,,/Public/images/tramp_scin_5.png";
                OnPropertyChanged(nameof(ImageSource));
            }
        }

        private void AppTimer_Tick(object sender, EventArgs e)
        {
            // Логика обновления состояния
            if (!this._isPaused && ClickRepository.ReadEnergyCount() < ClickRepository.ReadEnergyCountLimit())
            {
                Energy++;
            }
            else if (ClickRepository.ReadEnergyCount() >= ClickRepository.ReadEnergyCountLimit())
            {
                this._isPaused = true;
            }
            else if (this._isPaused && ClickRepository.ReadEnergyCount() < ClickRepository.ReadEnergyCountLimit())
            {
                this._isPaused = false;
            }

            if (ClickRepository.ReadPassive() > 0)
            {
                ClickRepository.WriteClickPass(ClickRepository.PassiveClick());
                OnPropertyChanged(nameof(Counter));
                OnPropertyChanged(nameof(CounterDisplay));
            }
        }

        private void BtnClickTap_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Counter++;

            if (ClickRepository.ReadEnergyCount() - ClickRepository.ClickForTap() < 0) return;

            AppTimer.Instance.StopEnergy();
            Task.Delay(1000).ContinueWith(t =>
            {
                AppTimer.Instance.StartEnergy();
            }, TaskScheduler.FromCurrentSynchronizationContext());

            Point clickPosition = e.GetPosition(ClickEffectGrid);

            TextBlock floatingText = new TextBlock
            {
                Text = $"+ {ClickRepository.ClickForTap()}",
                FontSize = 50,
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
                To = new Thickness(floatingText.Margin.Left, floatingText.Margin.Top - 430, floatingText.Margin.Right, floatingText.Margin.Bottom),
                Duration = TimeSpan.FromSeconds(3),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            DoubleAnimation fadeOut = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.65),
                BeginTime = TimeSpan.FromSeconds(0.65)
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
