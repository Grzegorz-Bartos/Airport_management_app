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
using Npgsql;
using System.Data;

namespace Airport_management
{
    public partial class PlanesPage : Page
    {
        string error;
        int language { get; set; }
        string cs { get; set; }
        public PlanesPage(int x, string y)
        {
            InitializeComponent();
            TB_find.Focus();
            KeyDown += Window_KeyDown;
            this.language = x;
            this.cs= y;
            BT_all_Click(null, null);
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
                BT_all.Content = "Wszystkie";
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
            AddPlanePage addPlanePage = new AddPlanePage(language, cs);
            ((MainWindow)Application.Current.MainWindow).Content = addPlanePage;
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
                cmd.CommandText = "SELECT * FROM plane WHERE name LIKE @searchTerm";

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
                cmd.CommandText = "SELECT * FROM plane";

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
                int id = (int)row.Row.ItemArray[0];
                string name = (string)row.Row.ItemArray[1];
                string model = (string)row.Row.ItemArray[2];
                string status = (string)row.Row.ItemArray[3];

                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "UPDATE plane SET name = @name, model = @model, status = @status WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@model", model);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.ExecuteNonQuery();
                }
            }

            using (NpgsqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM plane";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                DG_result.ItemsSource = dataTable.DefaultView;
            }
        }

        private void BT_delete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = DG_result.SelectedItem as DataRowView;
            if (row == null)
            {
                MessageBox.Show(error);
                return;
            }

            int id = (int)row["id"];
            using var con = new NpgsqlConnection(cs);
            con.Open();

            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM plane WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

            using (NpgsqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM plane";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                DG_result.ItemsSource = dataTable.DefaultView;
            }
        }

    }
}
