using System;
using System.Collections.Generic;
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
using DatabaseConnection;

namespace Store
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var name = State.User.FirstName;
            var SignInText = "Du är inloggad som: " + name;
            SignedInAs.Text = SignInText;

            var categories = new List<String> { "Comedy", "Action", "Thriller", "Drama", "Documentary", "Horror", "Sport", "Biography"};
            foreach (var category in categories)
            {
                var menuItem = new MenuItem() { };
                menuItem.Header = category;
                menuItem.Name = category;
                menuItem.Click += Category_Click;
                menuItem.Background = new SolidColorBrush(Colors.Black);
                menuItem.Foreground = new SolidColorBrush(Colors.White);
                this.CategoryMenu.Items.Add(menuItem);
            }
            
            switch (State.Sorting)
            {
                case "Rating":
                    State.Movies = API.SortByRating(0, 30);
                    break;
                case "´Title":
                    State.Movies = API.GetMovieSlice(0, 30);
                    break;
                case "Comedy":
                    State.Movies = API.SortByCategory(0, 30, State.Sorting);
                    break;
                case "Action":
                    State.Movies = API.SortByCategory(0, 30, State.Sorting);
                    break;
                case "Thriller":
                    State.Movies = API.SortByCategory(0, 30, State.Sorting);
                    break;
                case "Drama":
                    State.Movies = API.SortByCategory(0, 30, State.Sorting);
                    break;
                case "Documentary":
                    State.Movies = API.SortByCategory(0, 30, State.Sorting);
                    break;
                case "Horror":
                    State.Movies = API.SortByCategory(0, 30, State.Sorting);
                    break;
                case "Sport":
                    State.Movies = API.SortByCategory(0, 30, State.Sorting);
                    break;
                case "Biography":
                    State.Movies = API.SortByCategory(0, 30, State.Sorting);
                    break;
                default:
                    State.Movies = API.GetMovieSlice(0, 30);
                    break;
            }
           
          for (int y = 0; y <= MovieGrid.RowDefinitions.Count; y++)
            {
                for (int x = 0; x < MovieGrid.ColumnDefinitions.Count; x++)
                {
                    int i = y * MovieGrid.ColumnDefinitions.Count + x;
                    if (i < State.Movies.Count)
                    {
                        var movie = State.Movies[i];

                        try
                        {
                            var imagestack = new StackPanel() { };
                            imagestack.Height = 250;
                            var image = new Image() { };
                            var textblock = new TextBlock() { };
                            textblock.Text = movie.Title;
                            textblock.FontSize = 12;
                            textblock.TextAlignment = TextAlignment.Center;
                            image.Cursor = Cursors.Hand;
                            image.HorizontalAlignment = HorizontalAlignment.Center;
                            image.VerticalAlignment = VerticalAlignment.Center;
                            image.Source = new BitmapImage(new Uri(movie.ImageURL));
                            image.Height = 200;
                            image.Margin = new Thickness(4, 4, 4, 4);
                            imagestack.Children.Add(image);
                            imagestack.Children.Add(textblock);
                            imagestack.MouseUp += Imagestack_MouseUp;
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
        }
        private void Category_Click(object sender, RoutedEventArgs e)
        {
            var MenuItem = sender as MenuItem;
            var name = MenuItem.Name;
            State.Sorting = name;
            var next_window = new MainWindow();
            next_window.Show();
            this.Close();

        }
  
        private void Imagestack_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var x = Grid.GetColumn(sender as UIElement);
            var y = Grid.GetRow(sender as UIElement);

            int i = y * MovieGrid.ColumnDefinitions.Count + x;
            State.Pick = State.Movies[i];

            if(API.RegisterSale(State.User, State.Pick))
                MessageBox.Show("All is well and you can download your movie now.", "Sale Succeeded!", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("An error happened while buying the movie, please try again at a later time.", "Sale Failed!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if (State.User != null)
            {
                var next_window = new ProfileWindow();
                next_window.Show();
                this.Close();
            }
            
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var next_window = new LoginWindow();
            next_window.Show();
            this.Close();
            
            
        }

        private void SortByComedy_Click(object sender, RoutedEventArgs e)
        {
            State.Sorting = "Comedy";
            var next_window = new MainWindow();
            next_window.Show();
            this.Close();
        }

        private void SortByRating_Click(object sender, RoutedEventArgs e)
        {
            State.Sorting = "Rating";
            var next_window = new MainWindow();
            next_window.Show();
            this.Close();
        }
        private void SortByTitle_Click(object sender, RoutedEventArgs e)
        {
            State.Sorting = "Title";
            var next_window = new MainWindow();
            next_window.Show();
            this.Close();
        }
    }
}
