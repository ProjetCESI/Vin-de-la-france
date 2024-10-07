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
            using (var context = new PhishingContext())
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
    }
}
