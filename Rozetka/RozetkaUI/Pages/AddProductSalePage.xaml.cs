using BAL.DTO.Models;
using BAL.Interfaces;
using BAL.Services;
using DAL.Data.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for AddProductSalePage.xaml
    /// </summary>
    public partial class AddProductSalePage : Page
    {
        private SaleEntityDTO _sale;
        public AddProductSalePage(SaleEntityDTO sale)
        {
            InitializeComponent();
            DataContext = this;
            _sale = sale;

            IProductService productService = new ProductService();

            var products = productService.GetAllProducts().ToList();
            products.AddRange(sale.Sales_Products.Select(x => x.Product).AsEnumerable());
            Products = products.GroupBy(x => x.Id).Select(x => x.Count() > 1?null:x.First()).Where(x=>x!=null).ToList();
        }


        public List<ProductEntityDTO> Products { get; set; }

        private void CloseModal()
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.modalFrame.Navigate(null);
        }

        private void CloseModal(object sender, RoutedEventArgs e)
        {
            CloseModal();
        }

        private async void saveClick(object sender, RoutedEventArgs e)
        {
            (sender as ToggleButton).IsEnabled = false;
            var mainPage = App.Current.MainWindow as MainWindow;
            var tempList = _combo.SelectedItems;
            var list = new List<Sales_ProductEntityDTO>();
            SaleService saleService = new SaleService();
            foreach (ProductEntityDTO item in tempList)
            {
                var salesProduct = new Sales_ProductEntityDTO()
                {
                    ProductId = item.Id,
                    Product = item,
                    SaleId = _sale.Id,
                    Sale = _sale
                };
                await saleService.AddSalesProduct(salesProduct);
                list.Add(salesProduct);
            }
            ((mainPage.pageFrame.Content as SaleProductsPage).Sale.Sales_Products as List<Sales_ProductEntityDTO>).AddRange(list);
            CollectionViewSource.GetDefaultView((mainPage.pageFrame.Content as SaleProductsPage).Sale.Sales_Products).Refresh();
            (sender as ToggleButton).IsEnabled = true;
            CloseModal();
        }
    }
}
