using Npgsql.Replication.PgOutput.Messages;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VinWpf.DataSet;

namespace VinWpf.Views
{
    public partial class Clients : Page
    {
        public ObservableCollection<ClientsClass> ClientsList { get; set; }

        public Clients()
        {
            InitializeComponent();
            ClientsList = new ObservableCollection<ClientsClass>();
            this.DataContext = this;
            LoadClients();
        }

        private void LoadClients()
        {
            using (var context = new PhishingContext())
            {
                var clients = context.ClientsClass.ToList();
                ClientsList.Clear();
                foreach (var client in clients)
                {
                    ClientsList.Add(client);
                }
            }
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxClientName.Text))
            {
                AddClientMessage.Foreground = new SolidColorBrush(Colors.Red);
                AddClientMessage.Text = "Veuillez entrer le nom du client.";
                DataGridClientMessage.Text = "";
                return;
            }

            if (string.IsNullOrEmpty(TextBoxClientAddress.Text))
            {
                AddClientMessage.Foreground = new SolidColorBrush(Colors.Red);
                AddClientMessage.Text = "Veuillez entrer l'adresse du client.";
                DataGridClientMessage.Text = "";
                return;
            }

            if (string.IsNullOrEmpty(TextBoxClientPhone.Text))
            {
                AddClientMessage.Foreground = new SolidColorBrush(Colors.Red);
                AddClientMessage.Text = "Veuillez entrer le numéro de téléphone du client.";
                DataGridClientMessage.Text = "";
                return;
            }

            if (string.IsNullOrEmpty(TextBoxClientEmail.Text))
            {
                AddClientMessage.Foreground = new SolidColorBrush(Colors.Red);
                AddClientMessage.Text = "Veuillez entrer l'adresse email du client.";
                DataGridClientMessage.Text = "";
                return;
            }

            using (PhishingContext context = new PhishingContext())
            {
                bool addressExists = context.ClientsClass.Any(u => u.Address == TextBoxClientAddress.Text);
                if (addressExists == true)
                {
                    AddClientMessage.Foreground = new SolidColorBrush(Colors.Red);
                    AddClientMessage.Text = "Cette adresse est déjà utilisée.";
                    return;
                }

                context.ClientsClass.Add(new ClientsClass
                {
                    Name = TextBoxClientName.Text,
                    Address = TextBoxClientAddress.Text,
                    Phone = TextBoxClientPhone.Text,
                    Email = TextBoxClientEmail.Text
                });
                context.SaveChanges();
                LoadClients();
                AddClientMessage.Text = "Le client a été avec succès.";
                AddClientMessage.Foreground = new SolidColorBrush(Colors.Green);
            }
        }
    }
}
