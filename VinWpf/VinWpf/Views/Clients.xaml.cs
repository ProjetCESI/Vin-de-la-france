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
                bool emailExists = context.ClientsClass.Any(u => u.Email == TextBoxClientEmail.Text);
                bool phoneExists = context.ClientsClass.Any(u => u.Phone == TextBoxClientPhone.Text);
                if (addressExists == true)
                {
                    AddClientMessage.Foreground = new SolidColorBrush(Colors.Red);
                    AddClientMessage.Text = "Cette adresse est déjà utilisée.";
                    return;
                }

                else if (phoneExists == true)
                {
                    AddClientMessage.Foreground = new SolidColorBrush(Colors.Red);
                    AddClientMessage.Text = "Ce numéro de téléphone est déjà utilisé.";
                    return;
                }

                else if (emailExists == true)
                {
                    AddClientMessage.Foreground = new SolidColorBrush(Colors.Red);
                    AddClientMessage.Text = "Cet email est déjà utilisé.";
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
                TextBoxClientName.Text = "";
                TextBoxClientAddress.Text = "";
                TextBoxClientPhone.Text = "";
                TextBoxClientEmail.Text = "";
            }
        }

        private ClientsClass editClient;

        private void EditClient_Click(object sender, RoutedEventArgs e)
        {
            editClient = ((Button)sender).DataContext as ClientsClass;
            TextBoxClientName.Text = editClient.Name;
            TextBoxClientAddress.Text = editClient.Address;
            TextBoxClientPhone.Text = editClient.Phone;
            TextBoxClientEmail.Text = editClient.Email;

            AddClientMessage.Text = "";
            AddEditClientText.Text = "Modifier le client";
            UpdateClientButton.Visibility = Visibility.Visible;
            AddClientButton.Visibility = Visibility.Collapsed;
            CancelUpdateClientButton.Visibility = Visibility.Visible;
        }
        private void UpdateClient_Click(object sender, RoutedEventArgs e)
        {
            using (PhishingContext context = new PhishingContext())
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

                bool addressExists = context.ClientsClass.Any(u => u.Address == TextBoxClientAddress.Text && u.Id != editClient.Id);
                bool emailExists = context.ClientsClass.Any(u => u.Email == TextBoxClientEmail.Text && u.Id != editClient.Id);
                bool phoneExists = context.ClientsClass.Any(u => u.Phone == TextBoxClientPhone.Text && u.Id != editClient.Id);

                if (addressExists)
                {
                    AddClientMessage.Foreground = new SolidColorBrush(Colors.Red);
                    AddClientMessage.Text = "Cette adresse est déjà utilisée.";
                    return;
                }

                if (phoneExists)
                {
                    AddClientMessage.Foreground = new SolidColorBrush(Colors.Red);
                    AddClientMessage.Text = "Ce numéro de téléphone est déjà utilisé.";
                    return;
                }

                if (emailExists)
                {
                    AddClientMessage.Foreground = new SolidColorBrush(Colors.Red);
                    AddClientMessage.Text = "Cet email est déjà utilisé.";
                    return;
                }

                var ClientUpdate = context.ClientsClass.FirstOrDefault(u => u.Id == editClient.Id);
                ClientUpdate.Name = TextBoxClientName.Text;
                ClientUpdate.Address = TextBoxClientAddress.Text;
                ClientUpdate.Phone = TextBoxClientPhone.Text;
                ClientUpdate.Email = TextBoxClientEmail.Text;

                context.SaveChanges();
                LoadClients();

                AddClientMessage.Text = "Le client a été modifié avec succès.";
                AddClientMessage.Foreground = new SolidColorBrush(Colors.Green);
                TextBoxClientName.Text = "";
                TextBoxClientAddress.Text = "";
                TextBoxClientPhone.Text = "";
                TextBoxClientEmail.Text = "";
                AddEditClientText.Text = "Ajouter un client";
                UpdateClientButton.Visibility = Visibility.Collapsed;
                AddClientButton.Visibility = Visibility.Visible;
                CancelUpdateClientButton.Visibility = Visibility.Collapsed;
            }
        }

        private void CancelUpdateClient_Click(object sender, RoutedEventArgs e)
        {
            TextBoxClientName.Text = "";
            TextBoxClientAddress.Text = "";
            TextBoxClientPhone.Text = "";
            TextBoxClientEmail.Text = "";
            AddClientMessage.Text = "";
            AddEditClientText.Text = "Ajouter un client";
            UpdateClientButton.Visibility = Visibility.Collapsed;
            AddClientButton.Visibility = Visibility.Visible;
        }
    }
}
