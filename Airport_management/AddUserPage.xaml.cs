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
        string error = "Fill the data";
        int language { get; set; }
        string cs { get; set; }
        public AddUserPage(int x, string y)
        {
            InitializeComponent();
            TB_username.Focus();
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
            if(language == 0)
            {
                LB_main.Content = "Dodaj użytkownika";
                messagebox = "Dodano użytkownika";
                error = "Uzupełnij dane";
                BT_add.Content = "Dodaj";
                BT_back.Content = "Cofnij";
                LB_username.Content = "Nazwa użytkownika";
                LB_password.Content = "Hasło";
            }
            else
            {
                LB_main.Content = "Add a new user";
                messagebox = "User added";
                error = "Fill the data";
                BT_add.Content = "Add";
                BT_back.Content = "Back";
                LB_username.Content = "Username";
                LB_password.Content = "Password";
            }
        }

        private void BT_back_Click(object sender, RoutedEventArgs e)
        {
            UsersPage usersPage = new UsersPage(language, cs);
            ((MainWindow)Application.Current.MainWindow).Content = usersPage;
        }

        private void BT_add_Click(object sender, RoutedEventArgs e)
        {
            if (TB_username.Text != String.Empty && PB_password.Password != String.Empty)
            {

                using var con = new NpgsqlConnection(cs);
                con.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = con;

                string username = TB_username.Text;
                string password = PB_password.Password;
                TB_username.Clear();
                PB_password.Clear();
                string sql = string.Format("INSERT INTO users(username,password) VALUES('{0}',SHA512('{1}'))", username, password);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                MessageBox.Show(messagebox);
            }
            else
            {
                MessageBox.Show(error);
            }
            
        }
    }
}
