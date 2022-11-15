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
    public partial class AddUserPage : Page
    {
        string messagebox = "User added";
        public AddUserPage()
        {
            InitializeComponent();
        }

        private void BT_back_Click(object sender, RoutedEventArgs e)
        {
            UsersPage usersPage = new UsersPage();
            ((MainWindow)Application.Current.MainWindow).Content = usersPage;
        }

        private void BT_add_Click(object sender, RoutedEventArgs e)
        {
            var cs = "Host=localhost;Username=postgres;Password=Lemonade999;Database=Airport_database";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;

            string username = TB_username.Text;
            string password = TB_password.Text;
            TB_username.Clear();
            TB_password.Clear();
            string sql = string.Format("INSERT INTO users(username,password) VALUES('{0}',SHA512('{1}'))", username, password);
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            MessageBox.Show(messagebox);
        }
    }
}
