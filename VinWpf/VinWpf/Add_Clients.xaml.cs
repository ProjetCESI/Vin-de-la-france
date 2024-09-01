using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Xml.Linq;

namespace VinWpf
{
    /// <summary>
    /// Logique d'interaction pour Add_Clients.xaml
    /// </summary>
    public partial class Add_Clients : Page
    {
        private string connectionString = "Server=LAPTOP-FG0RP6E7\\MSSQLSERVER02;Database=Vin_project;Trusted_Connection=True;";
        public Add_Clients()
        {
            InitializeComponent();
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Veuillez remplir tous les champs.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string insertQuery = "INSERT INTO Client (Name, Email, Phone) VALUES (@Name, @Email, @Phone)";
                    SqlCommand cmd = new SqlCommand(insertQuery, conn);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Client ajouté avec succès.");

                    txtName.Text = string.Empty;
                    txtEmail.Text = string.Empty;
                    txtPhone.Text = string.Empty;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de l'ajout du client : {ex.Message}");
                }
            }
        }

        private void Button_Click_Articles(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).Content = new Add_Clients();
        }
    }
}
