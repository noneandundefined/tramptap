using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using tramptap.Internal.Services;
using tramptap.Models;

namespace tramptap.ViewModel
{
    public class PanelVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Название компонентов в панеле
        /// </summary>
        public ObservableCollection<PanelItems> PanelItems { get; set; }

        /// <summary>
        /// Команды
        /// </summary>
        private ICommand _panelItemClickCommand;
        public ICommand PanelItemClickCommand
        {
            get
            {
                return _panelItemClickCommand ?? (_panelItemClickCommand = new RelayCommand(path =>
                {
                    UIActions _action = new UIActions(Application.Current.MainWindow as MainWindow);
                    _action.SetFrame(path as string);
                }));
            }
        }

        public PanelVM()
        {
            PanelItems = new ObservableCollection<PanelItems>
            {
                new PanelItems {Image = "pack://application:,,,/Public/icons/store.png", PathView = "pack://application:,,,/View/Shop.xaml", Title = "Магазин"},
                new PanelItems {Image = "pack://application:,,,/Public/icons/layout-grid.png", PathView = "pack://application:,,,/View/Home.xaml", Title = "Главная"},
                new PanelItems {Image = "pack://application:,,,/Public/icons/calendar-check.png", PathView = "pack://application:,,,/View/Task.xaml", Title = "Выборы"},
            };
        }
    }
}
