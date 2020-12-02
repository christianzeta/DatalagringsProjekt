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
    /// Interaction logic for PasswordMissmatchWindow.xaml
    /// </summary>
    public partial class PasswordMissmatchWindow : Window
    {
        public PasswordMissmatchWindow()
        {
            InitializeComponent();

            
        }
        public void GoBack_Click(object sender, RoutedEventArgs e)
        {
            var new_window = new Settings();
            new_window.Show();
            this.Close();

        }
    }
}
