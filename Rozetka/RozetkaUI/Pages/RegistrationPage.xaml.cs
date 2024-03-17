using BAL.DTO.Models;
using BAL.Interfaces;
using BAL.Services;
using BAL.Utilities;
using DAL.Data.Entities;
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
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
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

        private void login_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.modalFrame.Navigate(new LoginPage());
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

        private async void Register_Click(object sender, RoutedEventArgs e)
        {
            var firstName = firstNameTB.Text;
            var secondName = secondNameTB.Text;
            var phone = phoneTB.Text;
            var email = emailTB.Text;
            var password = passwordHidden.Password;

            if (!Validate(firstName, secondName, email, password))
                return;

            (sender as Button).IsEnabled = false;

            try
            {
                IUserService userService = new UserService();
                var user = new BAL.DTO.Models.UserEntityDTO()
                {
                    FirstName = firstName,
                    SecondName = secondName,
                    Phone = phone,
                    Email = email,
                    Password = PasswordHasher.Hash(password),
                    DateCreated = DateTime.Now
                };
                await userService.Registrate(user);

                (App.Current.MainWindow as MainWindow).LoginedUser = user;

                CloseModal();
            }
            catch (Exception ex)
            {
                switch (ex.Message)
                {
                    case "email error":
                        emailTB.BorderBrush = new SolidColorBrush(Colors.Red);
                        emailTB.Focus();
                        break;
                    case "phone error":
                        phoneTB.BorderBrush = new SolidColorBrush(Colors.Red);
                        phoneTB.Focus();
                        break;
                }
            }
            (sender as Button).IsEnabled = true;
        }

        private bool Validate(string firstName, string secondName, string email, string password)
        {
            bool isValid = true;

            firstNameTB.BorderBrush = this.FindResource("PrimaryBackgroundColor") as SolidColorBrush;
            secondNameTB.BorderBrush = this.FindResource("PrimaryBackgroundColor") as SolidColorBrush;
            phoneTB.BorderBrush = this.FindResource("PrimaryBackgroundColor") as SolidColorBrush;
            emailTB.BorderBrush = this.FindResource("PrimaryBackgroundColor") as SolidColorBrush;
            passwordHidden.BorderBrush = this.FindResource("PrimaryBackgroundColor") as SolidColorBrush;

            if (firstName.Length == 0)
            {
                firstNameTB.BorderBrush = new SolidColorBrush(Colors.Red);
                isValid = false;
            }

            if (secondName.Length == 0)
            {
                secondNameTB.BorderBrush = new SolidColorBrush(Colors.Red);
                isValid = false;
            }

            if (phoneTB.Text.Contains('_'))
            {
                phoneTB.BorderBrush = new SolidColorBrush(Colors.Red);
                isValid = false;
            }

            if (!Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                emailTB.BorderBrush = new SolidColorBrush(Colors.Red);
                isValid = false;
            }

            if (password.Length == 0 || !Regex.IsMatch(password, @"^[a-zA-Z0-9_]+$"))
            {
                passwordHidden.BorderBrush = new SolidColorBrush(Colors.Red);
                isValid = false;
            }

            return isValid;
        }
    }
}
