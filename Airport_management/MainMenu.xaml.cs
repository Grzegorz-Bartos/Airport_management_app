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
    public partial class MainMenu : Page
    {
        int language { get; set; }
        string cs { get; set; }
        public MainMenu(int x, string y)
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
                RBT_EN.IsChecked = false;
                RBT_PL.IsChecked = true;
                BT_Login.Content = "Zaloguj";
                BT_Register.Content = "Zarejestruj";
                BT_Exit.Content = "Wyjdź";
            }
            else
            {
                BT_Login.Content = "Log In";
                BT_Register.Content = "Register";
                BT_Exit.Content = "Exit";
                RBT_PL.IsChecked = false;
                RBT_EN.IsChecked = true;
            }
        }
        private void RBT_Click(object sender, RoutedEventArgs e)
        {
            if (sender == RBT_PL)
            {
                RBT_EN.IsChecked = false;
                language = 0;
            }
            else
            {                RBT_PL.IsChecked = false;
                language = 1;
            }
            Translate();
        }

        private void BT_register_Click(object sender, RoutedEventArgs e)
        {
            RegisterPage registerPage = new RegisterPage(language,cs);
            ((MainWindow)Application.Current.MainWindow).Content = registerPage;
        }

        private void BT_login_Click(object sender, RoutedEventArgs e)
        {
            LogInPage loginPage = new LogInPage(language,cs);
            ((MainWindow)Application.Current.MainWindow).Content = loginPage;
        }

        private void BT_Exit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
