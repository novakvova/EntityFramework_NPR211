using BAL.DTO.Models;
using BAL.Interfaces;
using BAL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.Metadata;
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
    /// Interaction logic for SaleProductsPage.xaml
    /// </summary>
    public partial class SaleProductsPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private UserEntityDTO _loginedUser;

        public UserEntityDTO LoginedUser
        {
            get => _loginedUser;
            set
            {
                _loginedUser = value;
                OnPropertyChanged();
            }
        }
        public SaleProductsPage(SaleEntityDTO sale)
        {
            InitializeComponent();
            DataContext = this;
            Sale = sale;
            var mainWindow = (App.Current.MainWindow as MainWindow);
            mainWindow.OnLoginedUserChanged += () =>
            {
                LoginedUser = mainWindow.LoginedUser;
            };
            LoginedUser = mainWindow.LoginedUser;
        }
        public SaleEntityDTO Sale { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var content = (Sales_ProductEntityDTO)((sender as Button).TemplatedParent as ContentPresenter).Content;
            (App.Current.MainWindow as MainWindow).pageFrame.Navigate(new ProductPage(this, content.Product, content.Product.Category));
        }

        private void AddProduct(object sender, RoutedEventArgs e)
        {
            (App.Current.MainWindow as MainWindow).modalFrame.Navigate(new AddProductSalePage(Sale));

            //(App.Current.MainWindow as MainWindow).pageFrame.Navigate(new AddProductPage(this, Category));
        }

        private async void DeleteProduct(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            var content = (Sales_ProductEntityDTO)((sender as Button).TemplatedParent as ContentPresenter).Content;
            if (MessageBox.Show($"Видалити {content.Product.Name} з списку акцій?", "Увага", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                SaleService saleService = new SaleService();
                await saleService.DeleteSalesProduct(content);
                Sale.Sales_Products.Remove(content);
                CollectionViewSource.GetDefaultView(Sale.Sales_Products).Refresh();
            }
        }
    }
}
