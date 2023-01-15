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
using System.Security.Cryptography;

namespace Airport_management
{
    public partial class LogInPage : Page
    {
        public int language;
        string messagebox = "Logged in successfuly";
        string loginerror = "Wrong login";
        string passerror = "Wrong password";
        string username;
        string password;
        string verify;
        public LogInPage(int x)
        {
            InitializeComponent();
            TB_username.Focus();
            KeyDown += Window_KeyDown;
            language = x;
            Translate();
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BT_login_Click(null, null);
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
                BT_back.Content = "Cofnij";
                BT_login.Content = "Zaloguj";
                LB_username.Content = "nazwa użytkownika";
                LB_password.Content = "hasło";
                LB_loginto.Content = "Zaloguj się";
                messagebox = "Zalogowano";
                loginerror = "Błędny login";
                passerror = "Błędne hasło";
            }
            else
            {
                BT_back.Content = "Back";
                BT_login.Content = "Log In";
                LB_username.Content = "username";
                LB_password.Content = "password";
                LB_loginto.Content = "Log In to an existing account";
                messagebox = "Logged in successfuly";
                loginerror = "Wrong login";
                passerror = "Wrong password";
            }
        }
        private void BT_back_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu(language);
            ((MainWindow)Application.Current.MainWindow).Content = mainMenu;
        }

        private void verification(string username, string password)
        {
            var cs = "Host=localhost;Username=postgres;Password=Lemonade999;Database=Airport_database";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            string sql = string.Format("INSERT INTO verification(username,password) VALUES('{0}',SHA512('{1}'))", username, password);
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            sql = string.Format("SELECT password FROM verification WHERE username LIKE '{0}'", username);
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();

            using NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                verify = reader.GetString(0);
                Console.WriteLine("username: " + username + " " + "password hash: " + verify);
            } reader.Close();
        }

        private void BT_login_Click(object sender, RoutedEventArgs e)
        {
            //storing the credentials
            username = TB_username.Text;
            password = PB_password.Password;

            string login = string.Empty;
            string pass = string.Empty;

            TB_username.Clear();
            PB_password.Clear();

            var cs = "Host=localhost;Username=postgres;Password=Lemonade999;Database=Airport_database";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            string sql = string.Format("SELECT username, password FROM users WHERE username LIKE '{0}'", username);
            using var cmd = new NpgsqlCommand(sql, con);

            using NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                login = reader.GetString(0);
                pass = reader.GetString(1);

                Console.WriteLine("username: " + login + " " + "password hash: " + pass);
                /*Console.WriteLine("{0} {1}", reader.GetInt32(0), reader.GetString(1));*/
            } reader.Close();

            verification(username,password);

            if (login == username)
            {
                if (pass == verify)
                {
                    MainApp mainApp = new MainApp(language);
                    ((MainWindow)Application.Current.MainWindow).Content = mainApp;
                    MessageBox.Show(messagebox);
                }
                else
                {
                    MessageBox.Show(passerror);
                }
            }
            else
            {
                MessageBox.Show(loginerror);
            }
        }
    }
}