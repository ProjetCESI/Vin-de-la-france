using System.Windows.Controls;
using System.Linq;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Media;
using VinWpf.DataSet;
using Microsoft.EntityFrameworkCore;

namespace VinWpf.Views
{
    public partial class CommandeClient : Page
    {
        public ObservableCollection<ArticlesClass> ArticlesClass { get; set; }
        public ObservableCollection<ClientsClass> ClientsClass { get; set; }
        public ObservableCollection<LigneCommandeClientsClass> LigneCommandeClientsClass { get; set; }

        public CommandeClient()
        {
            InitializeComponent();
            ArticlesClass = new ObservableCollection<ArticlesClass>();
            LigneCommandeClientsClass = new ObservableCollection<LigneCommandeClientsClass>();
            ClientsClass = new ObservableCollection<ClientsClass>();
            DataContext = this;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LigneCommandeClientsClass.Clear();
            LoadListeArticles();
            PanierDataGrid.Items.Refresh();
            UpdateTotalPrix();
            LoadClients();
        }

        private void LoadClients()
        {
            using (var context = new PhishingContext())
            {
                List<ClientsClass> clients = context.ClientsClass.ToList();
                ClientsClass.Clear();
                foreach (var client in clients)
                {
                    ClientsClass.Add(client);
                }
                ClientComboBox.ItemsSource = ClientsClass;
            }
        }

        private void LoadListeArticles()
        {
            using (var context = new PhishingContext())
            {
                List<ArticlesClass> listeArticles = context.ArticlesClass
                    .Include(a => a.FamillesClass)
                    .ToList();
                ArticlesClass.Clear();
                foreach (var article in listeArticles)
                {
                    ArticlesClass.Add(article);
                }
            }
        }

        private void AddPanier(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var article = button?.DataContext as ArticlesClass;

            if (article != null)
            {
                var row = ArticlesDataGrid.ItemContainerGenerator.ContainerFromItem(article) as DataGridRow;
                var comboBox = FindVisualChild<ComboBox>(row);
                int quantite = int.Parse((comboBox?.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "1");

                LigneCommandeClientsClass.Add(new LigneCommandeClientsClass
                {
                    ArticlesClass = article,
                    Quantite = quantite,
                    PrixUnitaire = article.UnitPrice
                });

                PanierDataGrid.Items.Refresh();
                UpdateTotalPrix();

                MessageTextBlock.Text = "Article ajouté au panier.";
                MessageTextBlock.Foreground = Brushes.Green;
            }
            else
            {
                MessageTextBlock.Text = "Erreur lors de l'ajout de l'article.";
                MessageTextBlock.Foreground = Brushes.Red;
            }
        }


        private void UpdateTotalPrix()
        {
            int totalPrix = LigneCommandeClientsClass.Sum(ligne => ligne.PrixTotal);

            TotalPrixTextBlock.Text = $"Prix total: {totalPrix} €";
        }


        private static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T)
                    return (T)child;

                var result = FindVisualChild<T>(child);
                if (result != null)
                    return result;
            }
            return null;
        }

        private void SaveCommande(object sender, RoutedEventArgs e)
        {
            if (LigneCommandeClientsClass.Count == 0)
            {
                MessageTextBlock.Text = "Votre panier est vide. Ajoutez des articles avant d'enregistrer.";
                MessageTextBlock.Foreground = Brushes.Red;
                return;
            }

            var selectedClient = ClientComboBox.SelectedItem as ClientsClass;
            if (selectedClient == null)
            {
                MessageTextBlock.Text = "Veuillez sélectionner un client avant d'enregistrer la commande.";
                MessageTextBlock.Foreground = Brushes.Red;
                return;
            }

            using (var context = new PhishingContext())
            {
                var nouvelleCommande = new CommandeClientsClass
                {
                    Date = DateTime.Now,
                    Statut = "Enregistrée",
                    ClientsClassId = selectedClient.Id
                };

                context.CommandeClientsClass.Add(nouvelleCommande);
                context.SaveChanges(); 

                foreach (var ligne in LigneCommandeClientsClass)
                {
                    var ligneCommande = new LigneCommandeClientsClass
                    {
                        ArticlesClassId = ligne.ArticlesClass.Id,
                        Quantite = ligne.Quantite,
                        PrixUnitaire = ligne.PrixUnitaire,
                        CommandeClientsClassId = nouvelleCommande.Id
                    };

                    context.LigneCommandeClientsClass.Add(ligneCommande);
                }

                context.SaveChanges();

                MessageTextBlock.Text = "Commande enregistrée avec succès !";
                MessageTextBlock.Foreground = Brushes.Green;

                LigneCommandeClientsClass.Clear();
                PanierDataGrid.Items.Refresh();
                UpdateTotalPrix();
            }
        }
    }
}
