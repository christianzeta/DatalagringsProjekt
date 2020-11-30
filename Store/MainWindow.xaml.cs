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
            State.Movies = API.GetMovieSlice(0, 30);
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
    }
}
