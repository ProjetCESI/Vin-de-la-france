using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using VinWpf.DataSet;

namespace VinWpf.Views
{
    public partial class CommandeFournisseurs : Page
    {
        private int articleId;

        public CommandeFournisseurs(int articleId)
        {
            InitializeComponent();
            this.articleId = articleId;
        }

        private void LoadPage(object sender, RoutedEventArgs e)
        {
            LoadArticleDetails(articleId);
        }

        private void LoadArticleDetails(int articleId)
        {
            using (var context = new VinContext())
            {
                var article = context.ArticlesClass.FirstOrDefault(a => a.Id == articleId);

                if (article != null)
                {
                    NameArticle.Text = $"Article: {article.Name}";
                    QuantiteArticle.Text = $"Quantité: {article.QuantityStock}";
                    StockMinimumArticle.Text = $"Stock minimum: {article.MinimumThreshold}";
                    ReferenceArticle.Text = $"Référence: {article.Reference}";
                }
                else
                {
                    NameArticle.Text = "Article non trouvé.";
                }
            }
        }

        private void Commander_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(QuantiteCommandeTextBox.Text, out int quantiteCommande) && quantiteCommande > 0)
            {
                using (var context = new VinContext())
                {
                    var article = context.ArticlesClass.FirstOrDefault(a => a.Id == articleId);

                    if (article != null)
                    {
                        var newCommande = new CommandeFournisseursClass
                        {
                            Date = DateTime.Now,
                            Statut = "En attente",
                            FournisseursClassId = article.FournisseursClassId
                        };

                        context.CommandeFournisseursClass.Add(newCommande);
                        context.SaveChanges();

                        var newLigneCommande = new LigneCommandeFournisseursClass
                        {
                            ArticlesClassId = article.Id,
                            CommandeFournisseursClassId = newCommande.Id,
                            Quantite = quantiteCommande,
                            PrixUnitaire = article.UnitPrice
                        };

                        context.LigneCommandeFournisseursClass.Add(newLigneCommande);
                        context.SaveChanges();

                        MessageBox.Show("Commande passée avec succès !");
                    }
                    else
                    {
                        MessageBox.Show("L'article n'a pas été trouvé.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez entrer une quantité valide.");
            }
        }
    }
}
