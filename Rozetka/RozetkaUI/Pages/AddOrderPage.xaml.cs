using BAL.DTO.Models;
using BAL.Interfaces;
using BAL.Services;
using DAL.Data.Entities;
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
    /// Interaction logic for AddOrderPage.xaml
    /// </summary>
    public partial class AddOrderPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public AddOrderPage(UserEntityDTO user)
        {
            InitializeComponent();
            User = user;
            OrderCount = user.Orders.Count;
            Summury = user.Baskets.Select(x=> x.Product.Sales_Products.Count == 0? x.Product.Price * x.Count: decimal.Round(x.Product.Price - (x.Product.Sales_Products.First().Sale.DecreasePercent * x.Product.Price / 100), 2, MidpointRounding.AwayFromZero)).Sum();
        }

        private int _orderCount;

        public int OrderCount
        {
            get { return _orderCount; }
            set { _orderCount = value; OnPropertyChanged(); }
        }
        private decimal _summury;

        public decimal Summury
        {
            get { return _summury; }
            set { _summury = value; OnPropertyChanged(); }
        }

        private UserEntityDTO _user;

        public UserEntityDTO User
        {
            get { return _user; }
            set { _user = value; OnPropertyChanged(); }
        }


        private void MoveToBasket(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.pageFrame.Navigate(new BasketPage(User));
        }

        private async void ApplyOrder(object sender, RoutedEventArgs e)
        {
            (sender as ToggleButton).IsEnabled = false;
            var mainWindow = (MainWindow)Application.Current.MainWindow;

            IOrderService orderService = new OrderService();

            var statuses = await orderService.GetOrderStatuses();

            var order = new OrderEntityDTO()
            {
                DateCreated= DateTime.Now,
                OrderStatus = statuses.Where(x=>x.Name == "В обробці").FirstOrDefault(),
                OrderStatusId = statuses.Where(x=>x.Name == "В обробці").FirstOrDefault().Id,
                User = User,
                UserId = User.Id,
            };

            await orderService.CreateOrder(order);

            var itemOrders = new List<OrderItemEntityDTO>();
            foreach (var item in User.Baskets)
            {
                itemOrders.Add(new OrderItemEntityDTO()
                {
                    Count= item.Count,
                    DateCreated= DateTime.Now,
                    Order = order,
                    OrderId = order.Id,
                    PriceBuy = item.Product.Sales_Products.Count == 0?item.Product.Price: decimal.Round(item.Product.Price - (item.Product.Sales_Products.First().Sale.DecreasePercent * item.Product.Price / 100), 2, MidpointRounding.AwayFromZero),
                    Product = item.Product,
                    ProductId= item.ProductId
                });
            }

            await orderService.CreateOrderItemRange(itemOrders);

            order.OrderItems = itemOrders;


            IUserService userService = new UserService();
            foreach (var basketEntity in User.Baskets)
            {
                await userService.DeleteProductBasket(basketEntity);
            }
            User.Baskets.Clear();

            User.Orders.Add(order);

            mainWindow.LoginedUser = User;

            (sender as ToggleButton).IsEnabled = true;

            mainWindow.pageFrame.Navigate(new OrdersPage(User));
        }
    }
    public class CountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value + 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
