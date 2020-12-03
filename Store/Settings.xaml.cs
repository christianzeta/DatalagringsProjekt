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
using System.Data;
using DatabaseConnection;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace Store
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
            var name = State.User.FirstName + " " + State.User.LastName;
            var UsernameShow = "Your logged in as: " + name;
            ProfileUsername.Text = UsernameShow;

                
        }

        public void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            
            var oldPassword = OldPasswordField.Text;

            if (oldPassword == State.User.Password)
            {
                if (NewPasswordField.Text == ConfirmNewPasswordField.Text )
                {
                    State.User.Password = NewPasswordField.Text;

                    var new_window = new ProfileWindow();
                    new_window.Show();
                    this.Close();
                    API.SaveChanges();

                }

            } else
            {
                var new_window = new PasswordMissmatchWindow();
                new_window.Show();
                this.Close();

            }
                        
        }

        public void Profile_Click(object sender, RoutedEventArgs e)
        {
            var next = new ProfileWindow();
            next.Show();
            this.Close();
        }
    }
}
