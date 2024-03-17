using BAL.DTO.Models;
using BAL.Interfaces;
using BAL.Services;
using BAL.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePasswordPage : Page
    {
        private UserEntityDTO _user;
        public ChangePasswordPage(UserEntityDTO user)
        {
            InitializeComponent();
            _user = user;
            passwordHidden.PasswordChanged += (s,e) =>
            {
                CheckPassword();
            };

            passwordHidden1.PasswordChanged += (s,e) =>
            {
                CheckPassword();
            };

            passwordHidden2.PasswordChanged += (s,e) =>
            {
                CheckPassword();
            };

        }

        private void ShowPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e) => ShowPasswordFunction(showPasswordIcon, passwordUnmask, passwordHidden);
        private void ShowPassword_PreviewMouseUp(object sender, MouseButtonEventArgs e) => HidePasswordFunction(showPasswordIcon, passwordUnmask, passwordHidden);
        private void ShowPassword_MouseLeave(object sender, MouseEventArgs e) => HidePasswordFunction(showPasswordIcon, passwordUnmask, passwordHidden);
        private void ShowPassword1_PreviewMouseDown(object sender, MouseButtonEventArgs e) => ShowPasswordFunction(showPasswordIcon1, passwordUnmask1, passwordHidden1);
        private void ShowPassword1_PreviewMouseUp(object sender, MouseButtonEventArgs e) => HidePasswordFunction(showPasswordIcon1, passwordUnmask1, passwordHidden1);
        private void ShowPassword1_MouseLeave(object sender, MouseEventArgs e) => HidePasswordFunction(showPasswordIcon1, passwordUnmask1, passwordHidden1);
        private void ShowPassword2_PreviewMouseDown(object sender, MouseButtonEventArgs e) => ShowPasswordFunction(showPasswordIcon2, passwordUnmask2, passwordHidden2);
        private void ShowPassword2_PreviewMouseUp(object sender, MouseButtonEventArgs e) => HidePasswordFunction(showPasswordIcon2, passwordUnmask2, passwordHidden2);
        private void ShowPassword2_MouseLeave(object sender, MouseEventArgs e) => HidePasswordFunction(showPasswordIcon2, passwordUnmask2, passwordHidden2);
        private void ShowPasswordFunction(Path path, TextBlock textBlock, PasswordBox passwordBox)
        {
            path.Data = this.FindResource("Eye") as PathGeometry;
            textBlock.Visibility = Visibility.Visible;
            passwordBox.Foreground = this.FindResource("SecundaryBackgroundColor") as SolidColorBrush;
            textBlock.Text = passwordBox.Password;
        }
        private void HidePasswordFunction(Path path, TextBlock textBlock, PasswordBox passwordBox)
        {
            path.Data = this.FindResource("HiddenEye") as PathGeometry;
            textBlock.Visibility = Visibility.Hidden;
            passwordBox.Foreground = this.FindResource("SecundaryTextColor") as SolidColorBrush;
        }

        private async void saveClick(object sender, RoutedEventArgs e)
        {
            IUserService userService = new UserService();

            _user.Password = PasswordHasher.Hash(passwordHidden2.Password);

            await userService.EditUserInformation(_user);

            (App.Current.MainWindow as MainWindow).LoginedUser = _user;

            CloseModal();
        }

        private void CheckPassword()
        {
            if (String.IsNullOrWhiteSpace(passwordHidden.Password) ||
                String.IsNullOrWhiteSpace(passwordHidden1.Password) ||
                String.IsNullOrWhiteSpace(passwordHidden2.Password) ||
                !Regex.IsMatch(passwordHidden1.Password, @"^[a-zA-Z0-9_]+$") ||
                passwordHidden1.Password != passwordHidden2.Password ||
                passwordHidden2.Password == passwordHidden.Password ||
                PasswordHasher.Hash(passwordHidden.Password) != _user.Password)
            {
                savePassword.IsEnabled = false;
                return;
            }
            savePassword.IsEnabled = true;
        }

        private void CloseModal()
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.modalFrame.Navigate(null);
        }

        private void CloseModal(object sender, RoutedEventArgs e)
        {
            CloseModal();
        }

    }
}
