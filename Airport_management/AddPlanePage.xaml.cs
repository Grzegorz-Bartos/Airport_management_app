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
    public partial class AddPlanePage : Page
    {
        string messagebox = "Plane added";
        string error = "Fill the data";
        int language;
        public AddPlanePage(int x)
        {
            InitializeComponent();
            TB_name.Focus();
            KeyDown += Window_KeyDown;
            language = x;
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
                LB_main.Content = "Dodaj samolot";
                messagebox = "Dodano samolot";
                error = "Uzupełnij dane";
                BT_add.Content = "Dodaj";
                BT_back.Content = "Cofnij";
                LB_name.Content = "Nazwa";
                LB_model.Content = "Model";
                LB_status.Content = "Status";
            }
            else
            {
                LB_main.Content = "Add a new plane";
                messagebox = "Plane added";
                error = "Fill the data";
                BT_add.Content = "Add";
                BT_back.Content = "Back";
                LB_name.Content = "Name";
                LB_model.Content = "Model";
                LB_status.Content = "Status";
            }
        }

        private void BT_add_Click(object sender, RoutedEventArgs e)
        {
            var cs = "Host=localhost;Username=postgres;Password=Lemonade999;Database=Airport_database";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;

            string name = TB_name.Text;
            string model = TB_model.Text;
            string status = TB_status.Text;

            TB_name.Clear();
            TB_model.Clear();
            TB_status.Clear();

            string sql = string.Format("INSERT INTO plane(name,model,status) VALUES('{0}','{1}','{2}')", name, model, status);
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            MessageBox.Show(messagebox);
        }

        private void BT_back_Click(object sender, RoutedEventArgs e)
        {
            PlanesPage planesPage = new PlanesPage(language);
            ((MainWindow)Application.Current.MainWindow).Content = planesPage;
        }
    }
}
