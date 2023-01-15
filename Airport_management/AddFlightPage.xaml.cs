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
using Npgsql;

namespace Airport_management
{
    public partial class AddFlightPage : Page
    {
        string messagebox = "Flight added";
        string error = "Fill the data";
        int language;
        public AddFlightPage(int x)
        {
            InitializeComponent();
            TB_origin.Focus();
            KeyDown += Window_KeyDown;
            language = x;
            DP_date.SelectedDate = DateTime.Today;
            Translate();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BT_add_Click(null, null);
            }
            else if (e.Key == Key.Escape)
            {
                BT_back_Click(null, null);
            }
        }

        private void Translate()
        {
            if (language == 0)
            {
                LB_main.Content = "Dodaj lot";
                messagebox = "Dodano lot";
                error = "Uzupełnij dane";
                BT_add.Content = "Dodaj";
                BT_back.Content = "Cofnij";
                LB_origin.Content = "Skąd";
                LB_destination.Content = "Dokąd";
                LB_date.Content = "Data";
            }
            else
            {
                LB_main.Content = "Add a new flight";
                messagebox = "Flight added";
                error = "Fill the data";
                BT_add.Content = "Add";
                BT_back.Content = "Back";
                LB_origin.Content = "Origin";
                LB_destination.Content = "Destination";
                LB_date.Content = "Date";
            }
        }

        private void BT_add_Click(object sender, RoutedEventArgs e)
        {
            if (TB_origin.Text != String.Empty && TB_destination.Text != String.Empty && DP_date.Text != String.Empty)
            {
                var cs = "Host=localhost;Username=postgres;Password=Lemonade999;Database=Airport_database";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = con;

                string origin = TB_origin.Text;
                string destination = TB_destination.Text;
                string date = DP_date.Text;
                TB_origin.Clear();
                TB_destination.Clear();
                string sql = string.Format("INSERT INTO flight(origin,destination,date) VALUES('{0}','{1}','{2}')", origin, destination, date);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                MessageBox.Show(messagebox);
            }
            else
            {
                MessageBox.Show(error);
            }
        }
        private void BT_back_Click(object sender, RoutedEventArgs e)
        {
            FlightsPage flightsPage = new FlightsPage(language);
            ((MainWindow)Application.Current.MainWindow).Content = flightsPage;
        }
    }
}
