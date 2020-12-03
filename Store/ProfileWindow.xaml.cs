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
using System.Windows.Shapes;

namespace Store
{
    /// <summary>
    /// Interaction logic for ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {
        
        public ProfileWindow()
        {

            InitializeComponent();
            var name = State.User.FirstName + " " + State.User.LastName;
            var greeting = "Profilpage: " + name + ", Number of rented Movies: ";
            ProfileName.Text = greeting.ToUpper();
            ProfileName.FontSize = 10;
            var currentMovies = DatabaseConnection.API.GetCurrentMovieList(State.User.Id);
            var previousMovies = DatabaseConnection.API.GetPreviousMovieList(State.User.Id);
            var returnDates = DatabaseConnection.API.GetReturnDates(State.User.Id);
            ProfileName.Text += currentMovies.Count;
            for (int y = 0; y <= MovieGrid.RowDefinitions.Count; y++)
            {
                for (int x = 0; x < MovieGrid.ColumnDefinitions.Count; x++)
                {
                    int i = y * MovieGrid.ColumnDefinitions.Count + x;
                    if (i < currentMovies.Count)
                    {
                        var movie = currentMovies[i];
                        var returnDate = returnDates[i];
                        try
                        {
                            var imagestack = new StackPanel() { };
                            imagestack.Height = 250;
                            var image = new Image() { };
                            var titleBlock = new TextBlock() { };
                            var returnBlock = new TextBlock() { };
                            titleBlock.Text = movie.Title;
                            titleBlock.FontSize = 12;
                            titleBlock.TextAlignment = TextAlignment.Center;
                            titleBlock.Foreground = new SolidColorBrush(Colors.White);
                            returnBlock.Text = "Ends: " + returnDate.ToString();
                            returnBlock.FontSize = 12;
                            returnBlock.TextAlignment = TextAlignment.Center;
                            returnBlock.Foreground = new SolidColorBrush(Colors.White);
                            image.Cursor = Cursors.Hand;
                            image.HorizontalAlignment = HorizontalAlignment.Center;
                            image.VerticalAlignment = VerticalAlignment.Center;
                            image.Source = new BitmapImage(new Uri(movie.ImageURL));
                            image.Height = 200;
                            image.Margin = new Thickness(4, 4, 4, 4);
                            imagestack.Children.Add(image);
                            imagestack.Children.Add(titleBlock);
                            imagestack.Children.Add(returnBlock);
                            //imagestack.MouseUp += Imagestack_MouseUp;
                            MovieGrid.Children.Add(imagestack);
                            Grid.SetRow(imagestack, y);
                            Grid.SetColumn(imagestack, x);
                        }
                        catch (Exception e) when
                            (e is ArgumentNullException ||
                             e is System.IO.FileNotFoundException ||
                             e is UriFormatException)
                        {
                            continue;
                        }
                    }
                }
            }
            AccountName.Text = "Name: " + name;
            AccountMobile.Text = "Mobile: " + State.User.Mobile;
        }


        private void MoviesButton_Click(object sender, RoutedEventArgs e)
        {
            if (State.User != null)
            {
                var next_window = new MainWindow();
                next_window.Show();
                this.Close();
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            var next_window = new Settings();
            next_window.Show();
            this.Close();

        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var next_window = new LoginWindow();
            next_window.Show();
            this.Close();

        }

    }
}
