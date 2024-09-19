using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VinWpf.DataSet;

namespace VinWpf.Views
{
    public partial class Articles : Page
    {
        public ObservableCollection<ArticleClass> ArticlesList { get; set; }

        private ArticleClass editArticle;
        public ObservableCollection<FamilleClass> FamilleClass { get; set; }
        public Articles()
        {
            InitializeComponent();
            ArticlesList = new ObservableCollection<ArticleClass>();
            FamilleClass = new ObservableCollection<FamilleClass>();
            this.DataContext = this;
        }

        private void ArticleLoadedPage(object sender, RoutedEventArgs e)
        {
            LoadArticles();
            LoadComboBoxArticlesFamilleID();
        }

        private void LoadArticles()
        {
            using (var context = new PhishingContext())
            {
                var articles = context.ArticleClass
                                      .Include(a => a.FamilleClass)
                                      .ToList();
                ArticlesList.Clear();
                foreach (var article in articles)
                {
                    ArticlesList.Add(article);
                }
            }
        }


        private void LoadComboBoxArticlesFamilleID()
        {
            using (PhishingContext context = new PhishingContext())
            {
                List<FamilleClass> familleList = context.FamilleClass.ToList();
                FamilleClass.Clear();

                FamilleClass allFamille = new FamilleClass
                {
                    Id = -999,
                    Name = "Toutes les sociétés"
                };
                FamilleClass.Add(allFamille);

                foreach (FamilleClass society in familleList)
                {
                    FamilleClass.Add(society);
                }

                ComboBoxArticlesFamilleID.ItemsSource = FamilleClass;
                ComboBoxArticlesFamilleID.DisplayMemberPath = "Name";
                ComboBoxArticlesFamilleID.SelectedValuePath = "Id";


                ComboBoxArticlesFamilleID.SelectedValue = allFamille.Id;
            }
        }

        private void AddArticles_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateArticleInput())
            {
                using (var context = new PhishingContext())
                {
                    if (context.ArticleClass.Any(c => c.Name == TextBoxArticlesName.Text))
                    {
                        ShowMessage(AddArticlesMessage, "Cet article existe déjà.", Colors.Red);
                        return;
                    }

                    var newArticle = new ArticleClass
                    {
                        Name = TextBoxArticlesName.Text,
                        UnitPrice = int.Parse(TextBoxArticlesUnitPrice.Text),
                        QuantityStock = int.Parse(TextBoxArticlesQuantityStock.Text),
                        MinimumThreshold = int.Parse(TextBoxArticlesMinimumThreshold.Text),
                        FamilleClassId = (int)ComboBoxArticlesFamilleID.SelectedValue
                    };

                    context.ArticleClass.Add(newArticle);
                    context.SaveChanges();
                    LoadArticles();
                    ClearInputFields();
                    ShowMessage(AddArticlesMessage, "L'article a été ajouté avec succès.", Colors.Green);
                }
            }
        }

        private void EditArticle_Click(object sender, RoutedEventArgs e)
        {
            editArticle = ((Button)sender).DataContext as ArticleClass;
            FillInputFieldsForEditing(editArticle);
            ShowEditMode();
        }

        private void UpdateArticle_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateArticleInput())
            {
                using (var context = new PhishingContext())
                {
                    var articleToUpdate = context.ArticleClass.FirstOrDefault(a => a.Id == editArticle.Id);

                    if (articleToUpdate != null)
                    {
                        articleToUpdate.Name = TextBoxArticlesName.Text;
                        articleToUpdate.UnitPrice = int.Parse(TextBoxArticlesUnitPrice.Text);
                        articleToUpdate.QuantityStock = int.Parse(TextBoxArticlesQuantityStock.Text);
                        articleToUpdate.MinimumThreshold = int.Parse(TextBoxArticlesMinimumThreshold.Text);
                        articleToUpdate.FamilleClassId = (int)ComboBoxArticlesFamilleID.SelectedValue;

                        context.SaveChanges();
                        LoadArticles();
                        ClearInputFields();
                        ShowMessage(AddArticlesMessage, "L'article a été modifié avec succès.", Colors.Green);
                        ShowAddMode();
                    }
                }
            }
        }

        private void DeleteArticle_Click(object sender, RoutedEventArgs e)
        {
            var articleId = (int)((Button)sender).Tag;

            if (MessageBox.Show("Voulez-vous vraiment supprimer cet article ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                using (var context = new PhishingContext())
                {
                    var articleToDelete = context.ArticleClass.FirstOrDefault(a => a.Id == articleId);
                    if (articleToDelete != null)
                    {
                        context.ArticleClass.Remove(articleToDelete);
                        context.SaveChanges();
                        LoadArticles();
                        ShowMessage(DataGridArticlesMessage, "L'article a été supprimé avec succès.", Colors.Green);
                    }
                }
            }
        }

        private void CancelUpdateArticle_Click(object sender, RoutedEventArgs e)
        {
            ClearInputFields();
            ShowAddMode();
        }

        private void FillInputFieldsForEditing(ArticleClass article)
        {
            TextBoxArticlesName.Text = article.Name;
            TextBoxArticlesUnitPrice.Text = article.UnitPrice.ToString();
            TextBoxArticlesQuantityStock.Text = article.QuantityStock.ToString();
            TextBoxArticlesMinimumThreshold.Text = article.MinimumThreshold.ToString();
            ComboBoxArticlesFamilleID.SelectedValue = article.FamilleClassId;
        }

        private void ClearInputFields()
        {
            TextBoxArticlesName.Text = "";
            TextBoxArticlesUnitPrice.Text = "";
            TextBoxArticlesQuantityStock.Text = "";
            TextBoxArticlesMinimumThreshold.Text = "";
            ComboBoxArticlesFamilleID.SelectedIndex = -1;
        }

        private void ShowEditMode()
        {
            AddArticlesButton.Visibility = Visibility.Collapsed;
            UpdateArticlesButton.Visibility = Visibility.Visible;
            CancelUpdateArticlesButton.Visibility = Visibility.Visible;
            AddEditArticlesText.Text = "Modifier l'article";
        }

        private void ShowAddMode()
        {
            AddArticlesButton.Visibility = Visibility.Visible;
            UpdateArticlesButton.Visibility = Visibility.Collapsed;
            CancelUpdateArticlesButton.Visibility = Visibility.Collapsed;
            AddEditArticlesText.Text = "Créer un article";
        }

        private void ShowMessage(TextBlock target, string message, Color color)
        {
            target.Text = message;
            target.Foreground = new SolidColorBrush(color);
        }

        private bool ValidateArticleInput()
        {
            if (string.IsNullOrWhiteSpace(TextBoxArticlesName.Text) ||
                string.IsNullOrWhiteSpace(TextBoxArticlesUnitPrice.Text) ||
                string.IsNullOrWhiteSpace(TextBoxArticlesQuantityStock.Text) ||
                string.IsNullOrWhiteSpace(TextBoxArticlesMinimumThreshold.Text) ||
                ComboBoxArticlesFamilleID.SelectedIndex == -1)
            {
                ShowMessage(AddArticlesMessage, "Veuillez remplir tous les champs.", Colors.Red);
                return false;
            }

            return true;
        }

        private void NumberValidationTextBox(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !decimal.TryParse(e.Text, out _);
        }
    }
}
