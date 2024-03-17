using BAL.DTO.Models;
using BAL.Interfaces;
using BAL.Services;
using DAL.Interfaces;
using RozetkaUI.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace RozetkaUI.Components
{
    /// <summary>
    /// Interaction logic for NavigationBar.xaml
    /// </summary>
    public partial class NavigationBar : UserControl
    {
        public IList<CategoryEntityDTO> Categories { get; set; }

        public NavigationBar()
        {
            InitializeComponent();
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var size = e.NewSize;
            if (size.Width < 1280)
            {
                logoText.Visibility = Visibility.Collapsed;
            }
            else
            {
                logoText.Visibility = Visibility.Visible;
            }

            if (size.Width < 1024)
            {
                categoriesButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                categoriesButton.Visibility = Visibility.Visible;
            }
        }

        private void Back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                closeMenu.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            if (!(App.Current.MainWindow as MainWindow).IsLogined)
            {
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.modalFrame.Navigate(new LoginPage());
            }
        }

        private void RegistrationClick(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.modalFrame.Navigate(new RegistrationPage());
        }

        public void LogoutClick(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;

            mainWindow.LoginedUser = null;

            if (mainWindow.pageFrame.Content.GetType() == typeof(BasketPage) || 
                mainWindow.pageFrame.Content.GetType() == typeof(AdminPanelPage) ||
                mainWindow.pageFrame.Content.GetType() == typeof(AddCategoryPage) ||
                mainWindow.pageFrame.Content.GetType() == typeof(AddProductPage) ||
                mainWindow.pageFrame.Content.GetType() == typeof(ProfilePage) ||
                mainWindow.pageFrame.Content.GetType() == typeof(OrdersPage) ||
                mainWindow.pageFrame.Content.GetType() == typeof(OrderInfoPage) ||
                mainWindow.pageFrame.Content.GetType() == typeof(AddSalePage))
            {
                mainWindow.pageFrame.Navigate(new Main_Page());
            }
        }

        private void navBar_Loaded(object sender, RoutedEventArgs e)
        {
            ICategoryService categoryService = new CategoryService();
            Categories = categoryService.GetCategories().ToList();

            categoriesItemControl.ItemsSource = Categories;
        }

        private void catalogButton_Click(object sender, RoutedEventArgs e)
        {
            closeMenu.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            categoriesButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private void moveToCategory(object sender, RoutedEventArgs e)
        {
            var content = (CategoryEntityDTO)((sender as Button).TemplatedParent as ContentPresenter).Content;
            closeCategories.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            (App.Current.MainWindow as MainWindow).pageFrame.Navigate(new ProductListPage(content));
        }

        private void CategoryMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                closeCategories.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private async void DeleteCategory(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            var content = (CategoryEntityDTO)((sender as Button).TemplatedParent as ContentPresenter).Content;
            if (MessageBox.Show($"Видалити {content.Name}?", "Увага", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                ICategoryService categoryService = new CategoryService();
                await categoryService.DeleteCategory(content);
                Categories.Remove(content);
                CollectionViewSource.GetDefaultView(Categories).Refresh();
            }
        }

        private void EditCategory(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            var content = (CategoryEntityDTO)((sender as Button).TemplatedParent as ContentPresenter).Content;
            (App.Current.MainWindow as MainWindow).pageFrame.Navigate(new AddCategoryPage((Page)(App.Current.MainWindow as MainWindow).pageFrame.Content, content));
            closeCategories.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private void AddCategory(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            (App.Current.MainWindow as MainWindow).pageFrame.Navigate(new AddCategoryPage((Page)(App.Current.MainWindow as MainWindow).pageFrame.Content));
            closeCategories.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private void moveToAllCategories(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            if (mainWindow.pageFrame.Content.GetType() != typeof(AllCategoriesPage))
                mainWindow.pageFrame.Navigate(new AllCategoriesPage(Categories.ToList()));
            closeCategories.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private void HomeMenuClick(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            if (mainWindow.pageFrame.Content.GetType() != typeof(Main_Page))
                mainWindow.pageFrame.Navigate(new Main_Page());
            closeMenu.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private void HomeClick(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            if (mainWindow.pageFrame.Content.GetType() != typeof(Main_Page))
                mainWindow.pageFrame.Navigate(new Main_Page());
        }

        private void QuestionClick(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://help.rozetka.com.ua",
                UseShellExecute = true
            });
        }

        private void ChatClick(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://t.me/Rozetka_helpBot?start=src=hc",
                UseShellExecute = true
            });
        }

        private void AboutUs(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://rozetka.com.ua/ua/pages/about/",
                UseShellExecute = true
            });
        }

        private void TermsClick(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://rozetka.com.ua/ua/pages/legal_terms/",
                UseShellExecute = true
            });
        }

        private void CareersClick(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://rozetka.com.ua/ua/careers/",
                UseShellExecute = true
            });
        }

        private void ContactsClick(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://rozetka.com.ua/ua/contacts/",
                UseShellExecute = true
            });
        }

        private void DownloadFromGooglePlay(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://play.google.com/store/apps/details/?id=ua.com.rozetka.shop",
                UseShellExecute = true
            });
        }

        private void DownloadFromAppStore(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://apps.apple.com/app/apple-store/id740469631",
                UseShellExecute = true
            });
        }

        private void FacebookClick(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.facebook.com/rozetka.ua",
                UseShellExecute = true
            });
        }

        private void TwitterClick(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://twitter.com/rozetka_ua",
                UseShellExecute = true
            });
        }

        private void YoutubeClick(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.youtube.com/channel/UCr7r1-z79TYfqS2IPeRR47A",
                UseShellExecute = true
            });
        }

        private void InstagramClick(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.instagram.com/rozetkaua/",
                UseShellExecute = true
            });
        }

        private void ViberClick(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://invite.viber.com/?g2=AQB9mwM%2F5f%2FxJUlMxP4V9flr2%2BvXTC1MpxdGFZ0P6d%2Fs6Ws%2FFe%2FQtLiZwA4E28sj&lang=ru",
                UseShellExecute = true
            });
        }

        private void TelegramClick(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://t.me/rrozetka",
                UseShellExecute = true
            });
        }

        private void BasketClick(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            if (!(App.Current.MainWindow as MainWindow).IsLogined)
            {
                mainWindow.modalFrame.Navigate(new LoginPage());
            }
            else
            {
                if (mainWindow.pageFrame.Content.GetType() != typeof(BasketPage))
                    mainWindow.pageFrame.Navigate(new BasketPage(mainWindow.LoginedUser));
            }

        }

        private void BasketMenuClick(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            if (!(App.Current.MainWindow as MainWindow).IsLogined)
            {
                mainWindow.modalFrame.Navigate(new LoginPage());
            }
            else
            {
                if (mainWindow.pageFrame.Content.GetType() != typeof(BasketPage))
                    mainWindow.pageFrame.Navigate(new BasketPage(mainWindow.LoginedUser));
                closeMenu.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }

        private void AdminPanelClick(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;

            if (mainWindow.pageFrame.Content.GetType() != typeof(AdminPanelPage))
                mainWindow.pageFrame.Navigate(new AdminPanelPage());

            closeMenu.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private void ToProfilePageClick(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;

            if (mainWindow.pageFrame.Content.GetType() != typeof(ProfilePage))
                mainWindow.pageFrame.Navigate(new ProfilePage(mainWindow.LoginedUser));

            closeMenu.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private void OrdersClick(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            if (mainWindow.pageFrame.Content.GetType() != typeof(OrdersPage))
                mainWindow.pageFrame.Navigate(new OrdersPage(mainWindow.LoginedUser));
        }

        private void OrdersMenuClick(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            if (mainWindow.pageFrame.Content.GetType() != typeof(OrdersPage))
                mainWindow.pageFrame.Navigate(new OrdersPage(mainWindow.LoginedUser));
            closeMenu.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
    }
    public class IfAdminConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is UserEntityDTO)
            {
                return (value as UserEntityDTO).UserRoles.FirstOrDefault(x => x.Role.Name == "Admin") == null ? Visibility.Collapsed : Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
