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
    public partial class MainWindow : Window
    {
        int language { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            this.language = 1;
            DatabaseAccess dbaccess = new DatabaseAccess(language);
            ((MainWindow)Application.Current.MainWindow).Content = dbaccess;

            /*var cs = "Host=localhost;Username=postgres;Password=Lemonade999;Database=Airport_database";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "DROP TABLE IF EXISTS verification";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "DROP TABLE IF EXISTS users";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"CREATE TABLE verification(id SERIAL PRIMARY KEY,username VARCHAR(255),password VARCHAR(255))";
            cmd.ExecuteNonQuery(); 
            cmd.CommandText = @"CREATE TABLE users(id SERIAL PRIMARY KEY,username VARCHAR(255),password VARCHAR(255))";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"CREATE TABLE pilot(id SERIAL PRIMARY KEY,name VARCHAR(255),surname VARCHAR(255),age INT)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"CREATE TABLE flight(id SERIAL PRIMARY KEY,origin VARCHAR(255),destination VARCHAR(255),date TIME)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"CREATE TABLE plane(id SERIAL PRIMARY KEY,name VARCHAR(255),model VARCHAR(255),status VARCHAR(255))";
            cmd.ExecuteNonQuery();*/
        }
    }
}