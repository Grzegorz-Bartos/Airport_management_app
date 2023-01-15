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
    public partial class RegisterPage : Page
    {
        public int language;
        string messagebox = "Registation successful";
        string error = "Passwords differ";
        public RegisterPage(int x)
        {
            InitializeComponent();
            TB_username.Focus();
            language = x;
            Translate();
        }
        private void Translate()
        {
            if (language == 0)
            {
                BT_back.Content = "Cofnij";
                BT_register.Content = "Zarejestruj";
                LB_username.Content = "Nazwa użytkownika";
                LB_password.Content = "Hasło";
                LB_confirm.Content = "Potwierdź hasło";
                LB_createacc.Content = "Utwórz nowe konto";
                messagebox = "Rejestracja przebiegła pomyślnie";
                error = "Hasła nie są jednakowe";
            }
            else
            {
                BT_back.Content = "Back";
                BT_register.Content = "Register";
                LB_username.Content = "Username";
                LB_password.Content = "Password";
                LB_confirm.Content = "Confirm password";
                LB_createacc.Content = "Create a new account";
                messagebox = "Registation successful";
                error = "Passwords differ";
            }
        }
        private void BT_back_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu(language);
            ((MainWindow)Application.Current.MainWindow).Content = mainMenu;
        }
        private void BT_register_Click(object sender, RoutedEventArgs e)
        {
            var cs = "Host=localhost;Username=postgres;Password=Lemonade999;Database=Airport_database";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;

            if (PB_password.Password.ToString() == PB_confirm.Password.ToString())
            {
                string username = TB_username.Text;
                string password = PB_password.Password.ToString();

                TB_username.Clear();
                PB_password.Clear();
                PB_confirm.Clear();

                string sql = string.Format("INSERT INTO users(username,password) VALUES('{0}',SHA512('{1}'))", username, password);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                MessageBox.Show(messagebox);
                MainMenu mainMenu = new MainMenu(language);
                ((MainWindow)Application.Current.MainWindow).Content = mainMenu;
            }
            else
            {
                MessageBox.Show(error);
            }
        }
        private void RBT_hideshow_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}