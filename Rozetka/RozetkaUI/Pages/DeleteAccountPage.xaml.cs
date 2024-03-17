using BAL.DTO.Models;
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
    /// Interaction logic for DeleteAccountPage.xaml
    /// </summary>
    public partial class DeleteAccountPage : Page
    {
        private UserEntityDTO _user; 
        public DeleteAccountPage(UserEntityDTO user)
        {
            InitializeComponent();
            _user = user;
        }

        private async void saveClick(object sender, RoutedEventArgs e)
        {
            IUserService userService = new UserService();

            _user.IsDelete = true;

            await userService.EditUserInformation(_user);

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            (mainWindow.pageFrame.Content as ProfilePage).LogoutAccount(sender, e);

            CloseModal();
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
