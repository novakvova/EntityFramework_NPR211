using BAL.DTO.Models;
using BAL.Interfaces;
using BAL.Services;
using DAL.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for OrderInfoPage.xaml
    /// </summary>
    public partial class OrderInfoPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private Page _prevPage;
        public OrderInfoPage(Page prevPage, OrderEntityDTO orderEntity, bool isAdmin = false)
        {
            InitializeComponent();
            _prevPage = prevPage;
            Order = orderEntity;
            IsAdmin = isAdmin;
        }

        private OrderEntityDTO _order;

        public OrderEntityDTO Order
        {
            get { return _order; }
            set { _order = value; OnPropertyChanged(); }
        }

        private List<OrderStatusEntityDTO> _statuses;

        public List<OrderStatusEntityDTO> Statuses
        {
            get { return _statuses; }
            set { _statuses = value; OnPropertyChanged(); }
        }

        private bool _isAdmin;

        public bool IsAdmin
        {
            get { return _isAdmin; }
            set { _isAdmin = value; OnPropertyChanged(); }
        }

        private void ProductClick(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = (MainWindow)App.Current.MainWindow;
            var content = (OrderItemEntityDTO)((sender as TextBlock).TemplatedParent as ContentPresenter).Content;
            mainWindow.pageFrame.Navigate(new ProductPage(this, content.Product, content.Product.Category));
        }

        private void ReturnBack(object sender, RoutedEventArgs e)
        {
            if (_prevPage is OrdersPage)
            {
                var order = (_prevPage as OrdersPage).Orders.FirstOrDefault(x=>x.Id == _order.Id);
                order = Order;
                CollectionViewSource.GetDefaultView((_prevPage as OrdersPage).Orders).Refresh();
            }
            else
            {
                var order = (_prevPage as AdminPanelPage).Orders.FirstOrDefault(x => x.Id == _order.Id);
                order = Order;
                CollectionViewSource.GetDefaultView((_prevPage as AdminPanelPage).Orders).Refresh();
            }
            (App.Current.MainWindow as MainWindow).pageFrame.Navigate(_prevPage);
        }

        private async void CancelOrder(object sender, RoutedEventArgs e)
        {
            (sender as ToggleButton).IsEnabled = false;

            IOrderService orderService = new OrderService();
            Order.OrderStatus = Statuses.FirstOrDefault(x => x.Name == OrderStatuses.Canceled);
            Order.OrderStatusId = Order.OrderStatus.Id;
            await orderService.EditOrder(Order);
            OnPropertyChanged(nameof(Order));

            (sender as ToggleButton).IsEnabled = true;
        }

        private async void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
            (sender as ComboBox).IsEnabled = false;

            try
            {
                IOrderService orderService = new OrderService();
                await orderService.EditOrder(Order);
                Order.OrderStatus = Statuses.FirstOrDefault(x => x.Id == Order.OrderStatusId);
            }
            catch 
            {

            }

            (sender as ComboBox).IsEnabled = true;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            IOrderService orderService = new OrderService();
            var list = await orderService.GetOrderStatuses();
            Statuses = list.ToList();
        }
    }
    public class PriceStringFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (decimal)value;
            return val.ToString("C");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class IndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (int)value;

            return val - 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (int)value;

            return val + 1;
        }
    }
}
