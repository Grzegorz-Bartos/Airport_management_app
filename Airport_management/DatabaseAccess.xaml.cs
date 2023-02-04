using Npgsql;
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
    public partial class DatabaseAccess : Page
    {
        int language { get; set; }
        string cs { get; set; } 
        public DatabaseAccess(int x)
        {
            InitializeComponent();
            PB_database.Focus();
            KeyDown += Window_KeyDown;
            language = x;
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Confirm(null, null);
            }
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            cs = string.Format("Host=localhost;Username=postgres;Password={0};Database=Airport_database", PB_database.Password);
            using var con = new NpgsqlConnection(cs);
            
            try
            {
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    MainMenu mainMenu = new MainMenu(language,cs);
                    ((MainWindow)Application.Current.MainWindow).Content = mainMenu;
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Connection failed");
                PB_database.Clear();
            }
        }
    }
}
