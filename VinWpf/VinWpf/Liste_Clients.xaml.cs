using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace VinWpf
{
    public partial class Liste_Clients : Page
    {
        private string connectionStringMaster = "Server=LAPTOP-FG0RP6E7\\MSSQLSERVER02;Database=master;Trusted_Connection=True;";
        private string connectionString = "Server=LAPTOP-FG0RP6E7\\MSSQLSERVER02;Database=Vin_project;Trusted_Connection=True;";

        public Liste_Clients()
        {
            InitializeComponent();
            EnsureDatabaseAndTableExist();
            LoadClients();
        }

        private void EnsureDatabaseAndTableExist()
        {
            using (SqlConnection conn = new SqlConnection(connectionStringMaster))
            {
                try
                {
                    conn.Open();
                    string createDbQuery = "IF DB_ID('Vin_project') IS NULL CREATE DATABASE Vin_project;";
                    SqlCommand cmd = new SqlCommand(createDbQuery, conn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la création de la base de données : {ex.Message}");
                    return;
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string createTableQuery = @"
                        IF OBJECT_ID('Client', 'U') IS NULL 
                        CREATE TABLE Client (
                            Id INT PRIMARY KEY IDENTITY(1,1),
                            Name NVARCHAR(100),
                            Email NVARCHAR(100),
                            Phone NVARCHAR(15)
                        );";
                    SqlCommand cmd = new SqlCommand(createTableQuery, conn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la création de la table : {ex.Message}");
                }
            }
        }

        private void LoadClients()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string selectQuery = "SELECT * FROM Client";
                    SqlDataAdapter da = new SqlDataAdapter(selectQuery, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridClients.ItemsSource = dt.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors du chargement des clients : {ex.Message}");
                }
            }
        }

        private void Button_Click_Add_Clients(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).Content = new Add_Clients();
        }

        private void Button_Click_Delete_Client(object sender, RoutedEventArgs e)
        {
            if (sender is Button deleteButton && deleteButton.CommandParameter is int clientId)
            {
                var result = MessageBox.Show("Voulez vous vraiment supprimer ce client ?", "Confirmation de suppression", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();
                            string deleteQuery = "DELETE FROM Client WHERE Id = @ClientId";
                            SqlCommand cmd = new SqlCommand(deleteQuery, conn);
                            cmd.Parameters.AddWithValue("@ClientId", clientId);
                            cmd.ExecuteNonQuery();
                            LoadClients();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Erreur lors de la suppression du client : {ex.Message}");
                        }
                    }
                }
            }
        }
    }
}
