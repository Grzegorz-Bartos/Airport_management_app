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
        int language { get; set; }
        string cs { get; set; }
        public MainApp(int x, string y)
        {
            InitializeComponent();
            this.language = x;
            this.cs = y;
            Translate();
        }

        private void Translate()
        {
            if (language == 0)
            {
                LB_manage.Content = "Zarządzaj bazą danych";
                BT_users.Content = "Użytkownicy";
                BT_Flights.Content = "Loty";
                BT_pilots.Content = "Piloci";
                BT_planes.Content = "Samoloty";
                BT_back.Content = "Wyloguj";
            }
            else
            {
                LB_manage.Content = "Manage the database";
                BT_users.Content = "Users";
                BT_Flights.Content = "Flights";
                BT_pilots.Content = "Pilots";
                BT_planes.Content = "Planes";
                BT_back.Content = "Log out";
            }
        }

        private void BT_planes_Click(object sender, RoutedEventArgs e)
        {
            PlanesPage planesPage = new PlanesPage(language, cs);
            ((MainWindow)Application.Current.MainWindow).Content = planesPage;
        }

        private void BT_back_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu(language, cs);
            ((MainWindow)Application.Current.MainWindow).Content = mainMenu;
        }

        private void BT_users_Click(object sender, RoutedEventArgs e)
        {
            UsersPage usersPage = new UsersPage(language, cs);
            ((MainWindow)Application.Current.MainWindow).Content = usersPage;
        }

        private void BT_pilots_Click(object sender, RoutedEventArgs e)
        {
            PilotsPage pilotsPage = new PilotsPage(language, cs);
            ((MainWindow)Application.Current.MainWindow).Content = pilotsPage;
        }

        private void BT_Flights_Click(object sender, RoutedEventArgs e)
        {
            FlightsPage flightsPage = new FlightsPage(language, cs);
            ((MainWindow)Application.Current.MainWindow).Content = flightsPage;
        }
    }
}
