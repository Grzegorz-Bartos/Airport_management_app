﻿using System;
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
using System.Data;
using Npgsql;
using System.Data.SqlClient;

namespace Airport_management
{
    public partial class UsersPage : Page
    {
        int language { get; set; }
        string cs { get; set; }
        string error;
        public UsersPage(int x, string y)
        {
            InitializeComponent();
            TB_find.Focus();
            KeyDown += Window_KeyDown;
            this.language = x;
            this.cs = y;
            Translate();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BT_find_Click(null, null);
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
                error = "Nie wybrano elementu";
                LB_find.Content = "Znajdź";
                LB_result.Content = "Wyniki wyszukiwania";
                BT_find.Content = "Szukaj";
                BT_add.Content = "Dodaj";
                BT_all.Content = "Wszyscy";
                BT_delete.Content = "Usuń";
                BT_update.Content = "Zapisz";
                BT_back.Content = "Cofnij";
            }
            else
            {
                error = "No item selected";
                LB_find.Content = "Search";
                LB_result.Content = "Results";
                BT_find.Content = "Find";
                BT_add.Content = "Add";
                BT_all.Content = "Show all";
                BT_delete.Content = "Delete";
                BT_update.Content = "Save";
                BT_back.Content = "Back";
            }
        }

        private void BT_add_Click(object sender, RoutedEventArgs e)
        {
            AddUserPage addUserPage = new AddUserPage(language, cs);
            ((MainWindow)Application.Current.MainWindow).Content = addUserPage;
        }
        private void BT_back_Click(object sender, RoutedEventArgs e)
        {
            MainApp mainApp = new MainApp(language, cs);
            ((MainWindow)Application.Current.MainWindow).Content = mainApp;
        }

        private void BT_find_Click(object sender, RoutedEventArgs e)
        {
            using var con = new NpgsqlConnection(cs);
            con.Open();

            string searchTerm = TB_find.Text;

            using (NpgsqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "SELECT id,username FROM users WHERE username LIKE @searchTerm";

                cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                DG_result.Foreground = new SolidColorBrush(Colors.Black);
                DG_result.ItemsSource = dataTable.DefaultView;
            }
            TB_find.Clear();
        }

        private void BT_all_Click(object sender, RoutedEventArgs e)
        {
            using var con = new NpgsqlConnection(cs);
            con.Open();

            using (NpgsqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "SELECT id,username FROM users";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                DG_result.ItemsSource = dataTable.DefaultView;
            }
        }

        private void BT_update_Click(object sender, RoutedEventArgs e)
        {
            using var con = new NpgsqlConnection(cs);
            con.Open();

            foreach (DataRowView row in DG_result.ItemsSource)
            {
                    //get values from rows
                    int id = (int)row.Row.ItemArray[0];
                    string username = (string)row.Row.ItemArray[1];

                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE users SET username = @username WHERE id = @id";
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.ExecuteNonQuery();
                    }
            }

            //refresh the datagrid to show updated data
            using (NpgsqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "SELECT id,username FROM users";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                DG_result.ItemsSource = dataTable.DefaultView;
            }
        }

        private void BT_delete_Click(object sender, RoutedEventArgs e)
        {
            // get selected item of datagrid
            DataRowView row = DG_result.SelectedItem as DataRowView;
            if (row == null)
            {
                //error when nothing selected
                MessageBox.Show(error);
                return;
            }

            //ID of selected item
            int id = (int)row["id"];

            using var con = new NpgsqlConnection(cs);
            con.Open();

            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM users WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            
            using (NpgsqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "SELECT id,username FROM users";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                DG_result.ItemsSource = dataTable.DefaultView;
            }
        }

        private void DG_result_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }
    }
}