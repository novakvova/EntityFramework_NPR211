using BAL.DTO.Models;
using BAL.Interfaces;
using BAL.Services;
using BAL.Utilities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using Xceed.Wpf.Toolkit;
using static System.Net.Mime.MediaTypeNames;

namespace RozetkaUI.Pages
{
    /// <summary>
    /// Interaction logic for AddCategoryPage.xaml
    /// </summary>
    public partial class AddCategoryPage : Page
    {
        private CategoryEntityDTO _category;
        private Page _prevPage;
        public AddCategoryPage(Page prevPage, CategoryEntityDTO category = null)
        {
            InitializeComponent();
            _category = category;
            _prevPage = prevPage;

            if (_category != null)
            {
                submit.Content = "Відредагувати";

                categoryNameTextBox.Text = _category.Name;

                if (!String.IsNullOrEmpty(_category.Image))
                {
                    photosDockPanel.Children.Insert(photosDockPanel.Children.Count - 1, CreatePhoto(_category.Image));
                    photosDockPanel.Children[1].Visibility = Visibility.Hidden;
                }
            }
        }

        private Border CreatePhoto(string filePath)
        {
            /*
             XAML CODE
             <Border Style="{StaticResource PhotoCard}"
                            Drop="Image_Drop"
                            MouseMove="Image_MouseMove"
                            Name="image1">
                        <Border.Background>
                            <ImageBrush Stretch="UniformToFill" ImageSource="https://storage.ws.pho.to/s2/ba5069b25867b2305fe566efdffa8813bdee34c5_m.jpeg"/>
                        </Border.Background>
                        <Border CornerRadius="7">
                            <Border.Style>
                                <Style>
                                    <Setter Property="Border.Opacity" Value="1"/>
                                    <Setter Property="Border.Visibility" Value="Collapsed"></Setter>
                                    <Style.Triggers>
                                        <DataTrigger Binding="{Binding ElementName=image1, Path=IsMouseOver}" Value="true">
                                            <Setter Property="Border.Visibility" Value="Visible"></Setter>
                                            <Setter Property="Border.Background">
                                                <Setter.Value>
                                                    <SolidColorBrush Color="Black" Opacity="0.6"/>
                                                </Setter.Value>
                                            </Setter>
                                        </DataTrigger>
                                    </Style.Triggers>
                                </Style>
                            </Border.Style>
                            <Grid HorizontalAlignment="Stretch"
                                  VerticalAlignment="Stretch">
                                <Button Style="{StaticResource CardButton}"
                                    VerticalAlignment="Bottom" 
                                    Margin="31,0,67,10">
                                    <Path Data="{StaticResource EditImage}"
                                      Stretch="Uniform" 
                                      Margin="-1 0 0 -2"
                                      Fill="{StaticResource PrimaryBackgroundColor}" 
                                      Width="17" Height="17"
                                      HorizontalAlignment="Center">
                                        <Path.LayoutTransform>
                                            <RotateTransform CenterX="0" CenterY="0" Angle="180" />
                                        </Path.LayoutTransform>
                                    </Path>
                                </Button>
                                <Button Style="{StaticResource CardButton}"
                                    VerticalAlignment="Bottom"
                                    Margin="67,0,31,10">
                                    <Path Data="{StaticResource Delete}"
                                      Stretch="Uniform" 
                                      Fill="{StaticResource PrimaryBackgroundColor}" 
                                      Width="17" Height="17"
                                      HorizontalAlignment="Center">
                                        <Path.LayoutTransform>
                                            <RotateTransform CenterX="0" CenterY="0" Angle="180" />
                                        </Path.LayoutTransform>
                                    </Path>
                                </Button>
                            </Grid>
                        </Border>
                    </Border>
             */

            var main = new Border();
            main.Style = this.Resources["PhotoCard"] as Style;
            main.Name = $"image1";
            var image = new ImageBrush();
            image.ImageSource = new BitmapImage(new Uri(filePath));
            image.Stretch = Stretch.UniformToFill;
            main.Background = image;

            var child = new Border();
            child.CornerRadius = new CornerRadius(7);
            var childStyle = new Style();

            /*
             <Border.Style>
                 <Style>
                   <Setter Property="Border.Opacity" Value="1"/>
                   <Setter Property="Border.Visibility" Value="Collapsed"></Setter>
                   <Style.Triggers>
                        <DataTrigger Binding="{Binding ElementName=image1, Path=IsMouseOver}" Value="true">
                              <Setter Property="Border.Visibility" Value="Visible"></Setter>
                              <Setter Property="Border.Background">
                                  <Setter.Value>
                                       SolidColorBrush Color="Black" Opacity="0.6"/>
                                  </Setter.Value>
                              </Setter>
                          </DataTrigger>
                      </Style.Triggers>
                  </Style>
             </Border.Style>               
             */

            childStyle.Setters.Add(new Setter(Border.OpacityProperty, 1.0));
            childStyle.Setters.Add(new Setter(Border.VisibilityProperty, Visibility.Collapsed));
            var dataTrigger = new DataTrigger()
            {
                Binding = new Binding()
                {
                    Path = new PropertyPath("IsMouseOver"),
                    Source = main
                },
                Value = Boolean.TrueString
            };
            dataTrigger.Setters.Add(new Setter(Border.VisibilityProperty, Visibility.Visible));
            dataTrigger.Setters.Add(new Setter(Border.BackgroundProperty, new SolidColorBrush() { Color = Color.FromRgb(0, 0, 0), Opacity = 0.6 }));
            childStyle.Triggers.Add(dataTrigger);

            child.Style = childStyle;

            var grid = new Grid();
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;
            grid.VerticalAlignment = VerticalAlignment.Stretch;

            var edit = new Button()
            {
                Style = this.Resources["CardButton"] as Style,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(31, 0, 67, 10),
                Content = new System.Windows.Shapes.Path()
                {
                    Data = this.FindResource("EditImage") as PathGeometry,
                    Stretch = Stretch.Uniform,
                    Margin = new Thickness(-1, 0, 0, -2),
                    Fill = this.FindResource("PrimaryBackgroundColor") as SolidColorBrush,
                    Width = 17,
                    Height = 17,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    LayoutTransform = new RotateTransform()
                    {
                        CenterX = 0,
                        CenterY = 0,
                        Angle = 180,
                    }
                },
                Name = $"{main.Name}Edit"
            };
            edit.Click += ChangePhoto;
            grid.Children.Add(edit);
            var delete = new Button()
            {
                Style = this.Resources["CardButton"] as Style,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(67, 0, 31, 10),
                Content = new System.Windows.Shapes.Path()
                {
                    Data = this.FindResource("Delete") as PathGeometry,
                    Stretch = Stretch.Uniform,
                    Fill = this.FindResource("PrimaryBackgroundColor") as SolidColorBrush,
                    Width = 17,
                    Height = 17,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    LayoutTransform = new RotateTransform()
                    {
                        CenterX = 0,
                        CenterY = 0,
                        Angle = 180,
                    }
                },
                Name = $"{main.Name}Delete"
            };
            delete.Click += DeletePhoto;
            grid.Children.Add(delete);
            child.Child = grid;

            child.Child = grid;

            main.Child = child;

            return main;
        }

        private void DeletePhoto(object sender, RoutedEventArgs e)
        {
            var mainName = (sender as Button).Name.Substring(0, (sender as Button).Name.IndexOf("Delete"));
            foreach (Border photo in photosDockPanel.Children)
            {
                if (photo.Name == mainName)
                {
                    photosDockPanel.Children[1].Visibility = Visibility.Visible;
                    photosDockPanel.Children.Remove(photo);
                    break;
                }
            }
        }
        private void ChangePhoto(object sender, RoutedEventArgs e)
        {
            var mainName = (sender as Button).Name.Substring(0, (sender as Button).Name.IndexOf("Edit"));
            int index = 0;
            Border realPhoto = null;
            foreach (Border photo in photosDockPanel.Children)
            {
                if (photo.Name == mainName)
                {
                    index = photosDockPanel.Children.IndexOf(photo);
                    realPhoto = photo;
                    break;
                }
            }

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() != false)
            {
                photosDockPanel.Children.Remove(realPhoto);
                photosDockPanel.Children.Insert(index, CreatePhoto(fileDialog.FileName));
                photosDockPanel.Children[1].Visibility = Visibility.Hidden;
            }
        }

        private void Button_Drop(object sender, DragEventArgs e)
        {
            if (e.Effects != DragDropEffects.None)
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                photosDockPanel.Children.Insert(photosDockPanel.Children.Count - 1, CreatePhoto(files[0]));
            }
        }
        public readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPEG", ".JPE", ".BMP", ".GIF", ".PNG" };
        private void Button_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
                return;
            }



            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Length > 1)
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
                return;
            }

            if (ImageExtensions.Contains(System.IO.Path.GetExtension(files[0]).ToUpperInvariant()))
            {
                e.Effects = DragDropEffects.Move;
            }
            else
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
                return;
            }
        }

        private void LoadPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() != false)
            {
                photosDockPanel.Children.Insert(photosDockPanel.Children.Count - 1, CreatePhoto(fileDialog.FileName));
                photosDockPanel.Children[1].Visibility = Visibility.Hidden;
            }
        }

        private void ReturnBackClick(object sender, RoutedEventArgs e)
        {
            if (_prevPage == null || _prevPage.GetType() != typeof(AllCategoriesPage))
            {
                (App.Current.MainWindow as MainWindow).navBar.categoriesButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                (App.Current.MainWindow as MainWindow).pageFrame.Navigate(_prevPage);
            }
            else
            {
                (App.Current.MainWindow as MainWindow).pageFrame.Navigate(_prevPage);
            }
        }

        private async void add_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).IsEnabled = false;
            returnBack.IsEnabled = false;

            var name = categoryNameTextBox.Text;
            string photo = null;
            try
            {
                photo = ((photosDockPanel.Children[0] as Border).Background as ImageBrush).ImageSource.ToString();
                photo = photo.Substring(8);
            }
            catch 
            {

            }

            if (name.Length == 0)
            {
                categoryNameTextBox.BorderBrush = Brushes.Red;
                categoryNameTextBox.Focus();
                return;
            }

            ICategoryService categoryService = new CategoryService();
            if (_category == null)
            {
                if (photo != null)
                    photo = PhotoSaver.UploadImage(File.ReadAllBytes(photo));

                var category = new CategoryEntityDTO()
                {
                    Name = name,
                    Image = photo,
                    DateCreated= DateTime.Now
                };
                await categoryService.CreateCategory(category);
                category.Products = new List<ProductEntityDTO>();

                (App.Current.MainWindow as MainWindow).navBar.Categories.Add(category);
                CollectionViewSource.GetDefaultView((App.Current.MainWindow as MainWindow).navBar.Categories).Refresh();

                if (_prevPage != null)
                {
                    if (_prevPage.GetType() == typeof(AllCategoriesPage))
                    {
                        (_prevPage as AllCategoriesPage).Categories.Add(category);
                        CollectionViewSource.GetDefaultView((_prevPage as AllCategoriesPage).Categories).Refresh();
                    }
                }

                categoryNameTextBox.Text = "";

                photosDockPanel.Children.RemoveRange(0, photosDockPanel.Children.Count - 1);
                photosDockPanel.Children[0].Visibility = Visibility.Visible;

                var timer = new System.Timers.Timer();
                (sender as Button).Content = "Успішно добавлено";
                timer.Interval = 5000;
                timer.Elapsed += (s, e) =>
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        (sender as Button).Content = "Добавити категорію";
                        timer.Stop();
                    });
                };
                timer.Start();
            }
            else
            {
                if (photo != null)
                {
                    if (!photo.Contains(@"https://") && !photo.Contains(@"solido.tk") && !photo.Contains(@"rozetka.com"))
                        photo = PhotoSaver.UploadImage(File.ReadAllBytes(photo));
                    else
                        photo = "https://" + photo;
                }

                _category.Image = photo;
                _category.Name = name;

                await categoryService.EditCategory(_category);

                var category = (App.Current.MainWindow as MainWindow).navBar.Categories.First(c => c.Id == _category.Id);
                category.Name = _category.Name;
                category.Image = _category.Image;
                CollectionViewSource.GetDefaultView((App.Current.MainWindow as MainWindow).navBar.Categories).Refresh();

                if (_prevPage != null)
                {
                    if (_prevPage.GetType() == typeof(AllCategoriesPage))
                    {
                        var cat = (_prevPage as AllCategoriesPage).Categories.First(c => c.Id == _category.Id);
                        cat.Name = _category.Name;
                        cat.Image = _category.Image;
                        CollectionViewSource.GetDefaultView((_prevPage as AllCategoriesPage).Categories).Refresh();
                    }
                }

                var timer = new System.Timers.Timer();
                (sender as Button).Content = "Успішно відредаговано";
                timer.Interval = 5000;
                timer.Elapsed += (s, e) =>
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        (sender as Button).Content = "Відредагувати";
                        timer.Stop();
                    });
                };
                timer.Start();
            }

            (sender as Button).IsEnabled = true;
            returnBack.IsEnabled = true;
        }
    }
}
