using BAL.Interfaces;
using BAL.Services;
using BAL.Utilities;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                CloseModal();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            CloseModal();
        }

        private void CloseModal()
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.modalFrame.Navigate(null);
        }

        private async void login_Click(object sender, RoutedEventArgs e)
        {
            var login = loginTB.Text;
            var password = passwordHidden.Password;

            loginTB.BorderBrush = this.FindResource("PrimaryBackgroundColor") as SolidColorBrush;
            passwordHidden.BorderBrush = this.FindResource("PrimaryBackgroundColor") as SolidColorBrush;

            try
            {
                IUserService userService = new UserService();
                var user = await userService.Login(new BAL.DTO.Models.UserEntityDTO()
                {
                    Email = login,
                    Password = PasswordHasher.Hash(password)
                });

                (App.Current.MainWindow as MainWindow).LoginedUser = user;

                CloseModal();
            }
            catch (Exception ex)
            {
                switch (ex.Message)
                {
                    case "login error":
                        loginTB.BorderBrush = new SolidColorBrush(Colors.Red);
                        loginTB.Focus();
                        break;
                    case "password error":
                        passwordHidden.BorderBrush = new SolidColorBrush(Colors.Red);
                        passwordHidden.Focus();
                        break;
                }
            }
        }

        private void ShowPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e) => ShowPasswordFunction();
        private void ShowPassword_PreviewMouseUp(object sender, MouseButtonEventArgs e) => HidePasswordFunction();
        private void ShowPassword_MouseLeave(object sender, MouseEventArgs e) => HidePasswordFunction();
        private void ShowPasswordFunction()
        {
            showPasswordIcon.Data = this.FindResource("Eye") as PathGeometry;
            passwordUnmask.Visibility = Visibility.Visible;
            passwordHidden.Foreground = this.FindResource("SecundaryBackgroundColor") as SolidColorBrush;
            passwordUnmask.Text = passwordHidden.Password;
        }
        private void HidePasswordFunction()
        {
            showPasswordIcon.Data = this.FindResource("HiddenEye") as PathGeometry;
            passwordUnmask.Visibility = Visibility.Hidden;
            passwordHidden.Foreground = this.FindResource("SecundaryTextColor") as SolidColorBrush;
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.modalFrame.Navigate(new RegistrationPage());
        }
    }
}
