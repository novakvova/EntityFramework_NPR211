using AutoMapper;
using BAL.DTO.Models;
using BAL.Interfaces;
using BAL.Services;
using DAL.Data;
using RozetkaUI.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
using Xceed.Wpf.Toolkit;

namespace RozetkaUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            DatabaseSeeder.Seed();
            DataContext = this;
            IsLogined = false;
            pageFrame.Content = new Main_Page();
        }

        private UserEntityDTO _loginedUser;

        public UserEntityDTO LoginedUser
        {
            get => _loginedUser; 
            set
            {
                if (value != null)
                {
                    this.navBar.tipProfile.Visibility = Visibility.Hidden;
                    IsLogined = true;
                    Settings.Default.Login = value.Email;
                    Settings.Default.Password = value.Password;
                }
                else
                {
                    this.navBar.tipProfile.Visibility = Visibility.Visible;
                    IsLogined = false;
                    Settings.Default.Login = string.Empty;
                    Settings.Default.Password = string.Empty;
                }

                CollectionViewSource.GetDefaultView(this.navBar.Categories).Refresh();
                _loginedUser = value;
                OnLoginedUserChanged?.Invoke();
                OnPropertyChanged(); 
            }
        }
        public event Action OnLoginedUserChanged;

        private bool _isLogined;

        public bool IsLogined
        {
            get { return _isLogined; }
            set { _isLogined = value; OnPropertyChanged(); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (WindowState == System.Windows.WindowState.Maximized)
            {
                Settings.Default.Top = RestoreBounds.Top;
                Settings.Default.Left = RestoreBounds.Left;
                Settings.Default.Height = RestoreBounds.Height;
                Settings.Default.Width = RestoreBounds.Width;
                Settings.Default.Maximized = true;
            }
            else
            {
                Settings.Default.Top = this.Top;
                Settings.Default.Left = this.Left;
                Settings.Default.Height = this.Height;
                Settings.Default.Width = this.Width;
                Settings.Default.Maximized = false;
            }

            Settings.Default.Save();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Top = Settings.Default.Top;
            this.Left = Settings.Default.Left;
            this.Height = Settings.Default.Height;
            this.Width = Settings.Default.Width;
            if (Settings.Default.Maximized)
            {
                WindowState = System.Windows.WindowState.Maximized;
            }

            if (!String.IsNullOrEmpty(Settings.Default.Login) && !String.IsNullOrEmpty(Settings.Default.Password))
            {
                IUserService userService = new UserService();
                try
                {
                    var user = new UserEntityDTO()
                    {
                        Email = Settings.Default.Login,
                        Password = Settings.Default.Password
                    };
                    LoginedUser = await userService.Login(user);
                }
                catch
                {

                    
                }
            } 
        }
    }
}
