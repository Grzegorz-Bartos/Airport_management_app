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

namespace Airport_management
{
    public partial class MainApp : Page
    {
        public MainApp()
        {
            InitializeComponent();
        }

        private void BT_planes_Click(object sender, RoutedEventArgs e)
        {
            UsersPage usersPage = new UsersPage();
            ((MainWindow)Application.Current.MainWindow).Content = usersPage;
        }

        private void BT_back_Click(object sender, RoutedEventArgs e)
        {
            UsersPage usersPage = new UsersPage();
            ((MainWindow)Application.Current.MainWindow).Content = usersPage;
        }

        private void BT_users_Click(object sender, RoutedEventArgs e)
        {
            UsersPage usersPage = new UsersPage();
            ((MainWindow)Application.Current.MainWindow).Content = usersPage;
        }

        private void BT_pilots_Click(object sender, RoutedEventArgs e)
        {
            PilotsPage pilotsPage = new PilotsPage();
            ((MainWindow)Application.Current.MainWindow).Content = pilotsPage;
        }

        private void BT_Flights_Click(object sender, RoutedEventArgs e)
        {
            UsersPage usersPage = new UsersPage();
            ((MainWindow)Application.Current.MainWindow).Content = usersPage;
        }
    }
}
