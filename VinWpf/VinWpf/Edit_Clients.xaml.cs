using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace VinWpf
{
    public partial class Edit_Clients : Page
    {
        private string connectionString = "Server=LAPTOP-FG0RP6E7\\MSSQLSERVER02;Database=Vin_project;Trusted_Connection=True;";
        private int clientId;

        public Edit_Clients(int clientId)
        {
            InitializeComponent();
            this.clientId = clientId;
            LoadClientDetails();
        }

        private void LoadClientDetails()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string selectQuery = "SELECT * FROM Client WHERE Id = @ClientId";
                    SqlCommand cmd = new SqlCommand(selectQuery, conn);
                    cmd.Parameters.AddWithValue("@ClientId", clientId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtName.Text = reader["Name"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                        txtPhone.Text = reader["Phone"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors du chargement des détails du client : {ex.Message}");
                }
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string updateQuery = "UPDATE Client SET Name = @Name, Email = @Email, Phone = @Phone WHERE Id = @ClientId";
                    SqlCommand cmd = new SqlCommand(updateQuery, conn);
                    cmd.Parameters.AddWithValue("@ClientId", clientId);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Client mis à jour avec succès.");
                    ((MainWindow)Application.Current.MainWindow).Content = new Liste_Clients();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la mise à jour du client : {ex.Message}");
                }
            }
        }
        private void Button_Click_Clients(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).Content = new Liste_Clients();
        }
    }
}
