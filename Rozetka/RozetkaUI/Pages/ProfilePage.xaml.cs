using BAL.DTO.Models;
using BAL.Interfaces;
using BAL.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page, INotifyPropertyChanged
    {
        public ProfilePage(UserEntityDTO user)
        {
            InitializeComponent();
            User = user;
            _userService = new UserService();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private UserEntityDTO _user;
        public UserEntityDTO User
        {
            get { return _user; }
            set { _user = value; OnPropertyChanged(); }
        }
        private IUserService _userService;
        private async void saveUserInformation_Click(object sender, RoutedEventArgs e)
        {
            editUserInformation.IsChecked = true;

            User.FirstName = firstNameTextBox.Text;
            User.SecondName = secondNameTextBox.Text;
            OnPropertyChanged(nameof(User));

            await _userService.EditUserInformation(User);
            (App.Current.MainWindow as MainWindow).LoginedUser = User;

            firstNameTextBox.Text = string.Empty;
            secondNameTextBox.Text = string.Empty;
        }

        private void ClearUserFields(object sender, RoutedEventArgs e)
        {
            editUserInformation.IsChecked = true;
            firstNameTextBox.Text = string.Empty;
            secondNameTextBox.Text = string.Empty;
        }

        private async void saveContacts_Click(object sender, RoutedEventArgs e)
        {
            emailTextBox.BorderBrush = this.FindResource("PrimaryBackgroundColor") as SolidColorBrush;
            phoneTextBox.BorderBrush = this.FindResource("PrimaryBackgroundColor") as SolidColorBrush;

            if (await _userService.FindUserByEmailOrPhone(emailTextBox.Text) != null)
            {
                emailTextBox.BorderBrush = Brushes.Red;
                emailTextBox.Focus();
                return;
            }
            else if(await _userService.FindUserByEmailOrPhone(phoneTextBox.Text) != null)
            {
                phoneTextBox.BorderBrush = Brushes.Red;
                phoneTextBox.Focus();
                return;
            }

            editContacts.IsChecked = true;

            User.Email = emailTextBox.Text;
            User.Phone = phoneTextBox.Text;
            OnPropertyChanged(nameof(User));

            await _userService.EditUserInformation(User);
            (App.Current.MainWindow as MainWindow).LoginedUser = User;

            phoneTextBox.Text = string.Empty;
            emailTextBox.Text = string.Empty;
        }

        private void ClearContactsFields(object sender, RoutedEventArgs e)
        {
            editContacts.IsChecked = true;
            phoneTextBox.Text = string.Empty;
            emailTextBox.Text = string.Empty;
        }

        private void ChangePassword(object sender, RoutedEventArgs e)
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            mainWindow.modalFrame.Navigate(new ChangePasswordPage(User));
        }

        private void DeleteAccount(object sender, RoutedEventArgs e)
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            mainWindow.modalFrame.Navigate(new DeleteAccountPage(User));
        }

        public void LogoutAccount(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.navBar.LogoutClick(sender, e);
        }
    }
    public class BooleanConverter<T> : IValueConverter
    {
        public BooleanConverter(T trueValue, T falseValue)
        {
            True = trueValue;
            False = falseValue;
        }

        public T True { get; set; }
        public T False { get; set; }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool && ((bool)value) || value is decimal && ((decimal)value) == decimal.Zero ? True : False;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is T && EqualityComparer<T>.Default.Equals((T)value, True);
        }
    }
    public class TrueFalseConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Length == 2?(values[0] as string).Length != 0 && (values[1] as string).Length != 0: (values[0] as string).Length != 0 && (values[1] as string).Length != 0 && !(values[1] as string).Contains('_');
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public sealed class BooleanToVisibilityConverter : BooleanConverter<Visibility>
    {
        public BooleanToVisibilityConverter() :
            base(Visibility.Collapsed, Visibility.Visible)
        { }
    }
}
