using BAL.DTO.Models;
using BAL.Services;
using System;
using System.Collections;
using System.Collections.Generic;
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
    /// Interaction logic for Main_Page.xaml
    /// </summary>
    public partial class Main_Page : Page
    {
        public Main_Page()
        {
            InitializeComponent();
            DataContext = this;
            _categoryService = new CategoryService();
            _saleService = new SaleService();

            Sales = _saleService.GetAllSales();
            var categories = _categoryService.GetCategories().ToList();

            Category1 = categories[0];
            Category2 = categories[1];
            Category3 = categories[2];
        }
        private SaleService _saleService;
        private CategoryService _categoryService;
        public IEnumerable<SaleEntityDTO> Sales { get; set; }
        public CategoryEntityDTO Category1 { get; }
        public CategoryEntityDTO Category2 { get; }
        public CategoryEntityDTO Category3 { get; }

        private void MoreNotebooks_Click(object sender, RoutedEventArgs e)
        {
            (App.Current.MainWindow as MainWindow).pageFrame.Navigate(new ProductListPage(Category1));
        }

        private void MoreComputers_Click(object sender, RoutedEventArgs e)
        {
            (App.Current.MainWindow as MainWindow).pageFrame.Navigate(new ProductListPage(Category2));
        }

        private void MoreMonitors_Click(object sender, RoutedEventArgs e)
        {
            (App.Current.MainWindow as MainWindow).pageFrame.Navigate(new ProductListPage(Category3));
        }

        private ProductEntityDTO GetProduct1(string Name)
        {
            foreach (ProductEntityDTO pr in Category1.Products)
            {
                if (pr.Name == Name)
                {
                    return pr;
                }
            }
            return null;
        }
        private ProductEntityDTO GetProduct2(string Name)
        {
            foreach (ProductEntityDTO pr in Category2.Products)
            {
                if (pr.Name == Name)
                {
                    return pr;
                }
            }
            return null;
        }
        private ProductEntityDTO GetProduct3(string Name)
        {
            foreach (ProductEntityDTO pr in Category3.Products)
            {
                if (pr.Name == Name)
                {
                    return pr;
                }
            }
            return null;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string product = (((sender as Button).Parent as StackPanel).Children[1] as TextBlock).Text;
            (App.Current.MainWindow as MainWindow).pageFrame.Navigate(new ProductPage(this ,GetProduct1(product), Category1));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string product = (((sender as Button).Parent as StackPanel).Children[1] as TextBlock).Text;
            (App.Current.MainWindow as MainWindow).pageFrame.Navigate(new ProductPage(this, GetProduct2(product), Category2));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string product = (((sender as Button).Parent as StackPanel).Children[1] as TextBlock).Text;
            (App.Current.MainWindow as MainWindow).pageFrame.Navigate(new ProductPage(this,GetProduct3(product), Category3));
        }

        private void SaleBoard_Click(object sender, RoutedEventArgs e)
        {
            var content = (SaleEntityDTO)((sender as Button).TemplatedParent as ContentPresenter).Content;
            (App.Current.MainWindow as MainWindow).pageFrame.Navigate(new SaleProductsPage(content));
        }
    }
}
