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
        public ObservableCollection<CommandeFournisseurViewModel> Commandes { get; set; }

        public ListeCommandeFournisseurs()
        {
            InitializeComponent();
            Commandes = new ObservableCollection<CommandeFournisseurViewModel>();
            this.DataContext = this;
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCommandes();
        }

        private void LoadCommandes()
        {
            using (var context = new VinContext())
            {
                var commandes = context.CommandeFournisseursClass.ToList();
                Commandes.Clear();

                foreach (var commande in commandes)
                {
                    var lignesCommande = context.LigneCommandeFournisseursClass
                        .Where(lc => lc.CommandeFournisseursClassId == commande.Id)
                        .Select(lc => new CommandeFournisseurViewModel
                        {
                            Id = commande.Id,
                            Date = commande.Date,
                            Statut = commande.Statut,
                            NomArticle = lc.ArticlesClass.Name,  
                            Quantite = lc.Quantite,
                            Famille = lc.ArticlesClass.FamillesClass.Name,
                            Fournisseur = lc.ArticlesClass.FournisseursClass.Name
                        })
                        .ToList();

                    foreach (var ligne in lignesCommande)
                    {
                        Commandes.Add(ligne);
                    }
                }
            }
        }


        private void UpdateStatus_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                var commandeViewModel = button.DataContext as CommandeFournisseurViewModel;
                if (commandeViewModel != null)
                {
                    using (var context = new VinContext())
                    {
                        var result = MessageBox.Show("Êtes-vous sûr de vouloir changer le statut de cette commande ?",
                                                     "Confirmation",
                                                     MessageBoxButton.YesNo,
                                                     MessageBoxImage.Question);
                        if (result == MessageBoxResult.No)
                        {
                            return;
                        }

                        var commande = context.CommandeFournisseursClass.FirstOrDefault(c => c.Id == commandeViewModel.Id);

                        if (commande != null)
                        {
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
                    }

                    LoadCommandes();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                var commandeViewModel = button.DataContext as CommandeFournisseurViewModel;
                if (commandeViewModel != null)
                {
                    var result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette commande ?",
                                                 "Confirmation",
                                                 MessageBoxButton.YesNo,
                                                 MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        using (var context = new VinContext())
                        {
                            var commande = context.CommandeFournisseursClass.FirstOrDefault(c => c.Id == commandeViewModel.Id);
                            if (commande != null)
                            {
                                var lignesCommande = context.LigneCommandeFournisseursClass
                                    .Where(lc => lc.CommandeFournisseursClassId == commande.Id).ToList();

                                context.LigneCommandeFournisseursClass.RemoveRange(lignesCommande);

                                context.CommandeFournisseursClass.Remove(commande);

                                context.SaveChanges();
                            }
                        }

                        Commandes.Remove(commandeViewModel);
                    }
                }
            }
        }

        public class CommandeFournisseurViewModel
        {
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public string Statut { get; set; }
            public string NomArticle { get; set; }
            public int Quantite { get; set; }
            public string Famille { get; set; }
            public string Fournisseur { get; set; }
        }
    }
}
