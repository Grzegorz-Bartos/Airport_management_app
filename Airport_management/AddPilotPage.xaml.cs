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
        string error = "Fill the data";
        int language { get; set; }
        string cs { get; set; }
        public AddPilotPage(int x, string y)
        {
            InitializeComponent();
            TB_name.Focus();
            KeyDown += Window_KeyDown;
            this.language = x;
            this.cs = y;
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
                LB_main.Content = "Dodaj pilota";
                messagebox = "Dodano pilota";
                error = "Uzupełnij dane";
                BT_add.Content = "Dodaj";
                BT_back.Content = "Cofnij";
                LB_name.Content = "Imię";
                LB_surname.Content = "Nazwisko";
                LB_age.Content = "Wiek";
            }
            else
            {
                LB_main.Content = "Add a new pilot";
                messagebox = "Pilot added";
                error = "Fill the data";
                BT_add.Content = "Add";
                BT_back.Content = "Back";
                LB_name.Content = "Name";
                LB_surname.Content = "Surname";
                LB_age.Content = "Age";
            }
        }

        private void BT_add_Click(object sender, RoutedEventArgs e)
        {

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
            string sql = string.Format("INSERT INTO pilot(name,surname,age) VALUES('{0}','{1}',{2})", name, surname,age);
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            MessageBox.Show(messagebox);
        }

        private void BT_back_Click(object sender, RoutedEventArgs e)
        {
            PilotsPage pilotsPage = new PilotsPage(language, cs);
            ((MainWindow)Application.Current.MainWindow).Content = pilotsPage;
        }
    }
}
