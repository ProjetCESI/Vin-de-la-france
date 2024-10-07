using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using VinWpf.DataSet;

namespace VinWpf.Views
{
    public partial class ListeCommandeFournisseurs : Page
    {
        public ObservableCollection<CommandeFournisseursClass> Commandes { get; set; }

        public ListeCommandeFournisseurs()
        {
            InitializeComponent();
            Commandes = new ObservableCollection<CommandeFournisseursClass>();
            this.DataContext = this;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCommandes();
        }

        private void LoadCommandes()
        {
            using (var context = new PhishingContext())
            {
                var commandes = context.CommandeFournisseursClass.ToList();
                Commandes.Clear();
                foreach (var commande in commandes)
                {
                    Commandes.Add(commande);
                }
            }
        }

        private void UpdateStatus_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                var commande = button.DataContext as CommandeFournisseursClass;
                if (commande != null)
                {
                    using (var context = new PhishingContext())
                    {
                        // Boîte de dialogue de confirmation
                        var result = MessageBox.Show("Êtes-vous sûr de vouloir changer le statut de cette commande ?",
                                                     "Confirmation",
                                                     MessageBoxButton.YesNo,
                                                     MessageBoxImage.Question);
                        if (result == MessageBoxResult.No)
                        {
                            return; // L'utilisateur a annulé l'opération
                        }

                        // Si le statut est "En attente", on le passe à "Terminé" et on ajoute au stock
                        if (commande.Statut == "En attente")
                        {
                            commande.Statut = "Terminé";

                            // Récupérer les lignes de commande associées à la commande
                            var lignesCommande = context.LigneCommandeFournisseursClass
                                .Where(lc => lc.CommandeFournisseursClassId == commande.Id)
                                .ToList();

                            foreach (var ligne in lignesCommande)
                            {
                                // Récupérer l'article associé
                                var article = context.ArticlesClass.FirstOrDefault(a => a.Id == ligne.ArticlesClassId);
                                if (article != null)
                                {
                                    // Ajouter la quantité au stock
                                    article.QuantityStock += ligne.Quantite;
                                    context.ArticlesClass.Update(article);
                                }
                            }
                        }
                        // Si le statut est "Terminé", on le repasse à "En attente" et on retire du stock
                        else if (commande.Statut == "Terminé")
                        {
                            commande.Statut = "En attente";

                            // Récupérer les lignes de commande associées à la commande
                            var lignesCommande = context.LigneCommandeFournisseursClass
                                .Where(lc => lc.CommandeFournisseursClassId == commande.Id)
                                .ToList();

                            foreach (var ligne in lignesCommande)
                            {
                                // Récupérer l'article associé
                                var article = context.ArticlesClass.FirstOrDefault(a => a.Id == ligne.ArticlesClassId);
                                if (article != null)
                                {
                                    // Retirer la quantité du stock
                                    article.QuantityStock -= ligne.Quantite;
                                    context.ArticlesClass.Update(article);
                                }
                            }
                        }

                        // Sauvegarder les changements dans la base de données
                        context.CommandeFournisseursClass.Update(commande);
                        context.SaveChanges();
                    }

                    // Recharger les commandes pour mettre à jour l'affichage
                    LoadCommandes();
                }
            }
        }
    }
}
