using BAL.DTO.Models;
using BAL.Interfaces;
using BAL.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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

namespace RozetkaUI.Pages
{
    /// <summary>
    /// Interaction logic for AdminPanelPage.xaml
    /// </summary>
    public partial class AdminPanelPage : Page, INotifyPropertyChanged
    {
        private IUserService _userService;
        private SaleService _saleService;
        public AdminPanelPage()
        {
            InitializeComponent();
            _userService = new UserService();
            _saleService = new SaleService();
            DataContext = this;

            if ((App.Current.MainWindow as MainWindow).LoginedUser.UserRoles.FirstOrDefault(x => x.Role.Name == "SuperAdmin") != null)
            {
                adminPanel.Visibility = Visibility.Collapsed;
                superAdminPanel.Visibility = Visibility.Visible;

                userPanel.Visibility = Visibility.Collapsed;
                userSuperPanel.Visibility = Visibility.Visible;
            }
            else
            {
                adminPanel.Visibility = Visibility.Visible;
                superAdminPanel.Visibility = Visibility.Collapsed;

                userPanel.Visibility = Visibility.Visible;
                userSuperPanel.Visibility = Visibility.Collapsed;
            }
        }

        private string _prevSort;
        private IOrderService _orderService;
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var allusers = _userService.GetAllUsers().ToList();
            Sales = _saleService.GetAllSales().ToList();

            var mainWindow = App.Current.MainWindow as MainWindow;

            Users = allusers.Where(x => x.UserRoles.First().Role.Name == "User").ToList();
            Admins = allusers.Where(x => x.UserRoles.First().Role.Name == "Admin" && x.UserRoles.Count == 1 && x.UserRoles.First().UserId != mainWindow.LoginedUser.Id).ToList();
            

            if (Orders.Count == 0)
            {
                _orderService = new OrderService();
                var list = await _orderService.GetOrdersBy(x => true, 0, 5);
                Orders = list.ToList();
                SortBy<int>(x => x.Id, true);
                _prevSort = "number";
            }
            else
            {
                CollectionViewSource.GetDefaultView(Orders).Refresh();
            }
        }

        public bool IsSuperAdmin { get; set; }

        private List<UserEntityDTO> _users;

        public List<UserEntityDTO> Users
        {
            get
            {
                if (_users != null)
                    CollectionViewSource.GetDefaultView(_users).Refresh();
                return _users ?? (_users = new List<UserEntityDTO>());
            }
            set
            {
                if (_users == value)
                    return;
                _users = value;
                OnPropertyChanged();
            }
        }

        private List<SaleEntityDTO> _sales;

        public List<SaleEntityDTO> Sales
        {
            get
            {
                if (_sales != null)
                    CollectionViewSource.GetDefaultView(_sales).Refresh();
                return _sales ?? (_sales = new List<SaleEntityDTO>());
            }
            set
            {
                if (_sales == value)
                    return;
                _sales = value;
                OnPropertyChanged();
            }
        }

        private List<OrderEntityDTO> _orders;

        public List<OrderEntityDTO> Orders
        {
            get
            {
                if (_orders != null)
                {
                    CollectionViewSource.GetDefaultView(_orders).Refresh();
                    GeneralSum.Text = _orders.Select(x=>x.OrderItems.Select(x=>x.PriceBuy * x.Count).Sum()).Sum().ToString("C");
                    GeneralProducts.Text = _orders.Select(x=>x.OrderItems.Count).Sum().ToString();
                }
                return _orders ?? (_orders = new List<OrderEntityDTO>());
            }
            set
            {
                if (_orders == value)
                    return;
                _orders = value;
                OnPropertyChanged();
            }
        }

        private List<UserEntityDTO> _admins;

        public List<UserEntityDTO> Admins
        {
            get
            {
                if (_admins != null)
                    CollectionViewSource.GetDefaultView(_admins).Refresh();
                return _admins ?? (_admins = new List<UserEntityDTO>());
            }
            set
            {
                if (_admins == value)
                    return;
                _admins = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void MakeAdmin(object sender, RoutedEventArgs e)
        {
            var user = (UserEntityDTO)(sender as Button).DataContext;
            if (MessageBox.Show($"Зробити {user.FirstName} {user.SecondName} адміном?", "Увага!",MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                await _userService.EditUserRole(user.UserRoles.First(), "Admin");
                Users.Remove(user);
                Admins.Add(user);
                CollectionViewSource.GetDefaultView(Admins).Refresh();
            }
        }

        private async void DeleteAdmin(object sender, RoutedEventArgs e)
        {
            var user = (UserEntityDTO)(sender as Button).DataContext;
            if (MessageBox.Show($"Позбавити {user.FirstName} {user.SecondName} адмінських прав?", "Увага!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                await _userService.EditUserRole(user.UserRoles.First(), "User");
                Admins.Remove(user);
                Users.Add(user);
                CollectionViewSource.GetDefaultView(Users).Refresh();
            }
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
        private void SortByUser(object sender, RoutedEventArgs e)
        {
            if (_prevSort != "user")
            {
                _prevSort = "user";
                SortBy(x => x.User.FirstName + " " + x.User.SecondName, true);
            }
            else
            {
                _prevSort = string.Empty;
                SortBy(x => x.User.FirstName + " " + x.User.SecondName);
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
            mainWindow.pageFrame.Navigate(new OrderInfoPage(this, content, true));
        }

        private async void LoadMore(object sender, RoutedEventArgs e)
        {
            (sender as Button).IsEnabled = false;
            var list = await _orderService.GetOrdersBy(x => true, Orders.Count, 5);
            Task.WaitAll(Task.FromResult(list));
            Orders.AddRange(list);
            CollectionViewSource.GetDefaultView(Orders).Refresh();
            (sender as Button).IsEnabled = true;
        }

        private void AddSale(object sender, RoutedEventArgs e)
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            mainWindow.pageFrame.Navigate(new AddSalePage(this));
        }
        private void EditSale(object sender, RoutedEventArgs e)
        {
            var sale = (SaleEntityDTO)(sender as Button).DataContext;
            var mainWindow = App.Current.MainWindow as MainWindow;
            mainWindow.pageFrame.Navigate(new AddSalePage(this, sale));
        }

        private async void DeleteSale(object sender, RoutedEventArgs e)
        {
            var sale = (SaleEntityDTO)(sender as Button).DataContext;
            if (MessageBox.Show($"Видалити акцію з назвою {sale.SaleName}?", "Увага!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                await _saleService.DeleteSale(sale);
                Sales.Remove(sale);
            }
        }

        private void EditSaleProducts(object sender, RoutedEventArgs e)
        {
            var sale = (SaleEntityDTO)(sender as Button).DataContext;
            var mainWindow = App.Current.MainWindow as MainWindow;
            mainWindow.pageFrame.Navigate(new SaleProductsPage(sale));
        }
    }

    public class UserNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = value as UserEntityDTO;

            return val.FirstName + " " + val.SecondName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
