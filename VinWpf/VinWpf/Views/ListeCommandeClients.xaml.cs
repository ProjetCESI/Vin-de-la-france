using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VinWpf.DataSet;
using Microsoft.EntityFrameworkCore;

namespace VinWpf.Views
{
    public partial class ListeCommandeClients : Page
    {
        public List<CommandeClientsClass> Commandes { get; set; }

        public ListeCommandeClients()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCommandes();
        }

        private void LoadCommandes()
        {
            using (var context = new PhishingContext())
            {
                Commandes = context.CommandeClientsClass
                    .Include(c => c.ClientsClass)
                    .ToList();
            }

            CommandesDataGrid.ItemsSource = Commandes;
        }

        private void UpdateStatus_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var commande = button?.Tag as CommandeClientsClass;

            if (commande != null)
            {
                var statuts = new List<string> { "Enregistrée", "En cours", "Livrée", "Annulée" };

                var currentStatutIndex = statuts.IndexOf(commande.Statut);

                var nextStatutIndex = (currentStatutIndex + 1) % statuts.Count;

                commande.Statut = statuts[nextStatutIndex];

                CommandesDataGrid.Items.Refresh();
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new PhishingContext())
                {
                    var commandesInDb = context.CommandeClientsClass
                        .Include(c => c.ClientsClass)
                        .ToList();

                    foreach (var commande in Commandes)
                    {
                        var commandeInDb = commandesInDb.FirstOrDefault(c => c.Id == commande.Id);

                        if (commandeInDb != null)
                        {
                            if (commandeInDb.Statut != commande.Statut)
                            {
                                if (commandeInDb.Statut == "Livrée" && commande.Statut != "Livrée")
                                {
                                    var lignesCommandes = context.LigneCommandeClientsClass
                                        .Where(l => l.CommandeClientsClassId == commandeInDb.Id)
                                        .ToList();

                                    foreach (var ligne in lignesCommandes)
                                    {
                                        var article = context.ArticlesClass.FirstOrDefault(a => a.Id == ligne.ArticlesClassId);
                                        if (article != null)
                                        {
                                            article.QuantityStock += ligne.Quantite; 
                                        }
                                    }
                                }

                                if (commande.Statut == "Livrée")
                                {
                                    var lignesCommandes = context.LigneCommandeClientsClass
                                        .Where(l => l.CommandeClientsClassId == commandeInDb.Id)
                                        .ToList();

                                    foreach (var ligne in lignesCommandes)
                                    {
                                        var article = context.ArticlesClass.FirstOrDefault(a => a.Id == ligne.ArticlesClassId);
                                        if (article != null)
                                        {
                                            article.QuantityStock -= ligne.Quantite;
                                        }
                                    }
                                }

                                commandeInDb.Statut = commande.Statut;
                            }
                        }
                    }

                    context.SaveChanges();
                    MessageTextBlock.Text = "Les modifications ont été sauvegardées avec succès.";
                    MessageTextBlock.Foreground = Brushes.Green;
                }
            }
            catch (Exception ex)
            {
                MessageTextBlock.Text = "Erreur lors de la sauvegarde : " + ex.Message;
                MessageTextBlock.Foreground = Brushes.Red;
            }
        }
    }
}
