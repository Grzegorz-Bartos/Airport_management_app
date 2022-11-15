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
    public partial class AddPilotPage : Page
    {
        string messagebox = "Pilot added";
        public AddPilotPage()
        {
            InitializeComponent();
        }

        private void BT_add_Click(object sender, RoutedEventArgs e)
        {
            var cs = "Host=localhost;Username=postgres;Password=Lemonade999;Database=Airport_database";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;

            string name = TB_name.Text;
            string surname = TB_surname.Text;
            string age = TB_age.Text;
            TB_name.Clear();
            TB_surname.Clear();
            TB_age.Clear();
            string sql = string.Format("INSERT INTO pilot(name,surname,age) VALUES('{0}',SHA512('{1}'),{2})", name, surname,age);
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            MessageBox.Show(messagebox);
        }

        private void BT_back_Click(object sender, RoutedEventArgs e)
        {
            PilotsPage pilotsPage = new PilotsPage();
            ((MainWindow)Application.Current.MainWindow).Content = pilotsPage;
        }
    }
}
