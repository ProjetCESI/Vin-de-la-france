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
                        var result = MessageBox.Show("Êtes-vous sûr de vouloir changer le statut de cette commande ?",
                                                     "Confirmation",
                                                     MessageBoxButton.YesNo,
                                                     MessageBoxImage.Question);
                        if (result == MessageBoxResult.No)
                        {
                            return;
                        }

                        if (commande.Statut == "En attente")
                        {
                            commande.Statut = "Terminé";

                            var lignesCommande = context.LigneCommandeFournisseursClass
                                .Where(lc => lc.CommandeFournisseursClassId == commande.Id)
                                .ToList();

                            foreach (var ligne in lignesCommande)
                            {
                                var article = context.ArticlesClass.FirstOrDefault(a => a.Id == ligne.ArticlesClassId);
                                if (article != null)
                                {
                                    article.QuantityStock += ligne.Quantite;
                                    context.ArticlesClass.Update(article);
                                }
                            }
                        }
                        else if (commande.Statut == "Terminé")
                        {
                            commande.Statut = "En attente";

                            var lignesCommande = context.LigneCommandeFournisseursClass
                                .Where(lc => lc.CommandeFournisseursClassId == commande.Id)
                                .ToList();

                            foreach (var ligne in lignesCommande)
                            {
                                var article = context.ArticlesClass.FirstOrDefault(a => a.Id == ligne.ArticlesClassId);
                                if (article != null)
                                {
                                    article.QuantityStock -= ligne.Quantite;
                                    context.ArticlesClass.Update(article);
                                }
                            }
                        }

                        context.CommandeFournisseursClass.Update(commande);
                        context.SaveChanges();
                    }

                    LoadCommandes();
                }
            }
        }
    }
}
