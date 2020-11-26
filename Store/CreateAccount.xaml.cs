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
    /// Interaction logic for CreateAccount.xaml
    /// </summary>

    public partial class CreateAccount : Window
    {
        public CreateAccount()
        {
            InitializeComponent();
        }

        public void AccountCreate_Click(object sender, RoutedEventArgs e)
        {
            string username = CreateAccountUsernameField.Text;
            string password = CreateAccountPasswordField.Text;
            string passwordMatch = ConfirmPasswordField.Text;
            if (password == passwordMatch)
            {

                API.AddUser(username, password);
                var next_window = new LoginWindow();
                next_window.Show();
                this.Close();

            }
            else
            {
                var new_window = new PassMissMatch();
                new_window.Show();
                this.Close();
            }

        }
        

    }

    }

