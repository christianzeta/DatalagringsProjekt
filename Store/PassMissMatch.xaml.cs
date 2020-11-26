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
    /// Interaction logic for PassMissMatch.xaml
    /// </summary>
    public partial class PassMissMatch : Window
    {
        public PassMissMatch()
        {
            InitializeComponent();
        }

        public void PasswordMissMatch_Click(object sender, RoutedEventArgs e)
        {
            var next_window = new CreateAccount();
            next_window.Show();
            this.Close();

        }

    }
}
