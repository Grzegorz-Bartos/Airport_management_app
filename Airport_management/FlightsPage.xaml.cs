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
using System.Data.SqlClient;
using System.Data;

namespace Airport_management
{
    public partial class FlightsPage : Page
    {
        string error;
        int language { get; set; }
        string cs { get; set; }
        public FlightsPage(int x, string y)
        {
            InitializeComponent();
            TB_find.Focus();
            KeyDown += Window_KeyDown;
            this.language = x;
            this.cs = y;
            Translate();
            BT_all_Click(null, null);
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

        private void BT_find_Click(object sender, RoutedEventArgs e)
        {
            using var con = new NpgsqlConnection(cs);
            con.Open();

            string searchTerm = TB_find.Text;

            using (NpgsqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM flight WHERE origin LIKE @searchTerm";

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
                cmd.CommandText = "SELECT * FROM flight";

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
                    string origin = (string)row.Row.ItemArray[1];
                    string destination = (string)row.Row.ItemArray[2];
                    string date = (string)row.Row.ItemArray[3];

                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE flight SET origin = @origin, destination = @destination, date = @date WHERE id = @id";
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@origin", origin);
                        cmd.Parameters.AddWithValue("@destination", destination);
                        cmd.Parameters.AddWithValue("@date", date);
                        cmd.ExecuteNonQuery();
                    }
                }

                //refresh the datagrid to show updated data
                using (NpgsqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM flight";

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
                    cmd.CommandText = "DELETE FROM flight WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }

                using (NpgsqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM flight";

                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);

                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    DG_result.ItemsSource = dataTable.DefaultView;
                }
            }

            private void BT_add_Click(object sender, RoutedEventArgs e)
            {
                AddFlightPage addFlightPage = new AddFlightPage(language, cs);
                ((MainWindow)Application.Current.MainWindow).Content = addFlightPage;
            }

            private void BT_back_Click(object sender, RoutedEventArgs e)
            {
                MainApp mainApp = new MainApp(language, cs);
                ((MainWindow)Application.Current.MainWindow).Content = mainApp;
            }

            private void DG_result_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
            {

            }
        }
    } 