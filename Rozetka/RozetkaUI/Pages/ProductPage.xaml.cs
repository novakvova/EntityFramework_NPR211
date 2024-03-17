using BAL.DTO.Models;
using BAL.Interfaces;
using BAL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
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
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page, INotifyPropertyChanged
    {
        private Page _prevPage;
        public ProductPage(Page prevPage, ProductEntityDTO product, CategoryEntityDTO category)
        {
            InitializeComponent();
            Product = product;
            _category = category;
            SelectedImage = Product.Images.FirstOrDefault();
            DataContext = this;
            _prevPage= prevPage;

            if (Product.Sales_Products.Count > 0)
            {
                SalePrice = decimal.Round(Product.Price - (Product.Sales_Products.First().Sale.DecreasePercent * Product.Price / 100), 2, MidpointRounding.AwayFromZero);
            }


            var mainWindow = (MainWindow)App.Current.MainWindow;

            mainWindow.OnLoginedUserChanged += () =>
            {
                if (mainWindow.IsLogined)
                {
                    if (mainWindow.LoginedUser.Baskets.Where(x => x.ProductId == product.Id).FirstOrDefault() != null)
                    {
                        inBasketBtn.Visibility = Visibility.Visible;
                        basketBtn.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    inBasketBtn.Visibility = Visibility.Collapsed;
                    basketBtn.Visibility = Visibility.Visible;
                }
            };

            if (mainWindow.IsLogined)
            {
                if (mainWindow.LoginedUser.Baskets.Where(x=>x.ProductId == product.Id).FirstOrDefault() != null)
                {
                    inBasketBtn.Visibility = Visibility.Visible;
                    basketBtn.Visibility = Visibility.Collapsed;
                }
            }
        }
        private CategoryEntityDTO _category;
        public ProductEntityDTO Product { get; }
        public decimal SalePrice { get; set; }

        private ProductImageEntityDTO _selectedImage;

        public ProductImageEntityDTO SelectedImage
        {
            get { return _selectedImage; }
            set { _selectedImage = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ChangeSelectedImage(object sender, RoutedEventArgs e)
        {
            var content = (ProductImageEntityDTO)((sender as Button).TemplatedParent as ContentPresenter).Content;
            SelectedImage = content;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (App.Current.MainWindow as MainWindow).pageFrame.Navigate(_prevPage);
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.PreviousSize.Width > e.NewSize.Width)
            {
                if (rightBlock.ActualWidth > 280 && ActualWidth <= 980)
                {
                    var newSize = rightBlock.ActualWidth - (e.PreviousSize.Width - e.NewSize.Width);
                    if (newSize <= 300)
                    {
                        rightBlock.Width = ActualWidth - 350;
                    }
                    else
                    {
                        rightBlock.Width = newSize;
                    }
                }
            }
            else
            {
                if (rightBlock.ActualWidth < 600)
                {
                    var newSize = rightBlock.ActualWidth + (e.NewSize.Width - e.PreviousSize.Width);
                    if (newSize > 600)
                    {
                        rightBlock.Width = 600;
                    }
                    else
                    {
                        rightBlock.Width = newSize;   
                    }

                }
            }

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (ActualWidth < 980)
            {
                rightBlock.Width = ActualWidth - 350;
            }
            else
            {
                rightBlock.Width = 600;
            }

        }

        private async void BasketClick(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            if (mainWindow.LoginedUser == null)
            {
                mainWindow.modalFrame.Navigate(new LoginPage());
                return;
            }

            (sender as Button).IsEnabled = false;

            var user = mainWindow.LoginedUser;
            var basketItem = new BasketEntityDTO()
            {
                Count = 1,
                ProductId = Product.Id,
                UserId = user.Id
            };
            IUserService userService = new UserService();
            await userService.AddProductToBasket(basketItem);

            basketItem.User = user;
            basketItem.Product = Product;

            user.Baskets.Add(basketItem);

            inBasketBtn.Visibility = Visibility.Visible;
            basketBtn.Visibility = Visibility.Collapsed;

            (sender as Button).IsEnabled = true;
        }

        private void MoveToBasket(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.pageFrame.Navigate(new BasketPage(mainWindow.LoginedUser));
        }
    }
}
