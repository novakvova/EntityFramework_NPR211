using BAL.DTO.Models;
using BAL.Interfaces;
using BAL.Services;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
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
    /// Interaction logic for BasketPage.xaml
    /// </summary>
    public partial class BasketPage : Page
    {
        public UserEntityDTO User { get; }
        public BasketPage(UserEntityDTO user)
        {
            InitializeComponent();
            DataContext = this;
            User = user;
            ChangeSumBalance();
        }

        private async void Plus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var content = (BasketEntityDTO)((sender as Button).TemplatedParent as ContentPresenter).Content;

                await ChangeItemContent(sender, content, (short)(content.Count + 1));
            }
            catch 
            {

            }
        }

        private void ChangeSumBalance()
        {
            decimal sum = 0;
            foreach (var basket in User.Baskets)
            {
                if (basket.Product.Sales_Products.Count == 0)
                {
                    sum += basket.Count * basket.Product.Price;
                }
                else
                {
                    sum += basket.Count * decimal.Round(basket.Product.Price - (basket.Product.Sales_Products.First().Sale.DecreasePercent * basket.Product.Price / 100), 2, MidpointRounding.AwayFromZero);
                }
            }

            sumBalance.Text = sum.ToString();
        }

        private async Task ChangeItemContent(object sender ,BasketEntityDTO basketEntity, short count)
        {
            basketEntity.Count = count;

            var parent = (sender as Button).Parent;
            var textBlock = (TextBlock)(parent as StackPanel).Children[3];
            textBlock.Text = (basketEntity.Count * basketEntity.Product.Price).ToString();

            ChangeSumBalance();

            IUserService userService = new UserService();
            await userService.EditProductBasket(basketEntity);

            CollectionViewSource.GetDefaultView(User.Baskets).Refresh();
        }

        private async void Minus_Click(object sender, RoutedEventArgs e)
        {
            var content = (BasketEntityDTO)((sender as Button).TemplatedParent as ContentPresenter).Content;
            if (content.Count - 1 > 0)
            {
                await ChangeItemContent(sender, content, (short)(content.Count - 1));
            }
        }

        private async void DeleteAll(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Видалити всі товари з кошика?","Увага!",MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                for (int i = User.Baskets.Count - 1; i >= 0; i--)
                {
                    await DeleteBasket(User.Baskets.ElementAt(i));
                    User.Baskets.Remove(User.Baskets.ElementAt(i));
                }
                CollectionViewSource.GetDefaultView(User.Baskets).Refresh();

                productsEmpty.Visibility = Visibility.Visible;
                productsList.Visibility = Visibility.Collapsed;
            }

        }

        private async void DeleteClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var content = (BasketEntityDTO)((sender as Button).TemplatedParent as ContentPresenter).Content;

                await DeleteBasket(content);
                User.Baskets.Remove(content);
                ChangeSumBalance();

                if (User.Baskets.Count == 0)
                {
                    productsEmpty.Visibility = Visibility.Visible;
                    productsList.Visibility = Visibility.Collapsed;
                }

                CollectionViewSource.GetDefaultView(User.Baskets).Refresh();
            }
            catch 
            {

            }

        }

        private async Task DeleteBasket(BasketEntityDTO basketEntity)
        {
            IUserService userService = new UserService();
            await userService.DeleteProductBasket(basketEntity);
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var content = (BasketEntityDTO)((sender as TextBlock).TemplatedParent as ContentPresenter).Content;

            var mainWindow = (MainWindow)App.Current.MainWindow;
            mainWindow.pageFrame.Navigate(new ProductPage(this, content.Product, content.Product.Category));
        }

        private void SubmitOrder(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)App.Current.MainWindow;
            mainWindow.pageFrame.Navigate(new AddOrderPage(User));
        }
    }
    public class BasketPriceConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var product = values[2] as ProductEntityDTO;
            if (product.Sales_Products.Count == 0)
            {
                return ((decimal)values[0] * (short)values[1]).ToString("C");
            }
            else
            {
                return decimal.Round(product.Price - (product.Sales_Products.First().Sale.DecreasePercent * product.Price / 100), 2, MidpointRounding.AwayFromZero).ToString("C");
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
