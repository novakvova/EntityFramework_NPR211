using BAL.DTO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RozetkaUI.Pages
{
    /// <summary>
    /// Interaction logic for OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public OrdersPage(UserEntityDTO user)
        {
            InitializeComponent();
            User = user;
            Orders = user.Orders.ToList();
            SortBy<int>(x => x.Id, true);
            _prevSort = "number";
        }
        private UserEntityDTO _user;

        public UserEntityDTO User
        {
            get { return _user; }
            set { _user = value; OnPropertyChanged(); }
        }

        private List<OrderEntityDTO> _orders;

        public List<OrderEntityDTO> Orders
        {
            get { return _orders; }
            set { _orders = value; OnPropertyChanged(); }
        }
        private string _prevSort;

        private void ToMainPageClick(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)App.Current.MainWindow;
            mainWindow.pageFrame.Navigate(new Main_Page());
        }

        private void SortByNumber(object sender, RoutedEventArgs e)
        {
            if (_prevSort != "number")
            {
                _prevSort = "number";
                SortBy<int>(x => x.Id, true);
            }
            else
            {
                _prevSort = string.Empty;
                SortBy<int>(x => x.Id);
            }
        }

        private void SortByPrice(object sender, RoutedEventArgs e)
        {
            if (_prevSort != "price")
            {
                _prevSort = "price";
                SortBy(x => x.OrderItems.Select(x => x.PriceBuy * x.Count).Sum());
            }
            else
            {
                _prevSort = string.Empty;
                SortBy(x => x.OrderItems.Select(x => x.PriceBuy * x.Count).Sum(), true);
            }
        }

        private void SortByCountProducts(object sender, RoutedEventArgs e)
        {
            if (_prevSort != "countProducts")
            {
                _prevSort = "countProducts";
                SortBy(x => x.OrderItems.Count);
            }
            else
            {
                _prevSort = string.Empty;
                SortBy(x => x.OrderItems.Count, true);
            }
        }

        private void SortByStatus(object sender, RoutedEventArgs e)
        {
            if (_prevSort != "status")
            {
                _prevSort = "status";
                SortBy(x => x.OrderStatus.Name, true);
            }
            else
            {
                _prevSort = string.Empty;
                SortBy(x => x.OrderStatus.Name);
            }
        }

        private void SortBy<T>(Func<OrderEntityDTO, T> predicate, bool isDescending = false)
        {
            Orders = isDescending ? Orders.OrderBy(predicate).ToList() : Orders.OrderByDescending(predicate).ToList();
        }

        private void OrderInfoClick(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)App.Current.MainWindow;
            var content = (OrderEntityDTO)((sender as Button).TemplatedParent as ContentPresenter).Content;
            mainWindow.pageFrame.Navigate(new OrderInfoPage(this, content));
        }
    }

    public class SumPriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = value as List<OrderItemEntityDTO>;

            return val.Select(x => x.PriceBuy * x.Count).Sum().ToString("C");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
