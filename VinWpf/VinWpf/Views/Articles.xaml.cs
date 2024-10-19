using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using VinWpf.DataSet;

namespace VinWpf.Views
{
    public partial class Articles : Page
    {
        public ObservableCollection<ArticlesClass> ArticlesList { get; set; }

        public ObservableCollection<FournisseursClass> fournisseursClass { get; set; }

        private ArticlesClass editArticle;
        public ObservableCollection<FamillesClass> FamilleClass { get; set; }

        private string base64ImageString;
        public Articles()
        {
            InitializeComponent();
            ArticlesList = new ObservableCollection<ArticlesClass>();
            FamilleClass = new ObservableCollection<FamillesClass>();
            fournisseursClass = new ObservableCollection<FournisseursClass>();
            this.DataContext = this;
        }

        private void ArticleLoadedPage(object sender, RoutedEventArgs e)
        {
            LoadArticles();
            LoadComboBoxArticlesFamilleID();
            LoadComboboxFournisseurID();
        }

        private void LoadArticles()
        {
            using (var context = new VinContext())
            {
                var articles = context.ArticlesClass
                                      .Include(a => a.FamillesClass)
                                      .Include(a => a.FournisseursClass)
                                      .ToList();
                ArticlesList.Clear();
                foreach (var article in articles)
                {
                    ArticlesList.Add(article);
                }
            }
        }

        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage(new Uri(openFileDialog.FileName));
                SelectedImage.Source = bitmap;

                base64ImageString = ConvertImageToBase64(openFileDialog.FileName);
            }
        }

        private string ConvertImageToBase64(string imagePath)
        {
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            return Convert.ToBase64String(imageBytes);
        }
        private void LoadComboBoxArticlesFamilleID()
        {
            using (VinContext context = new VinContext())
            {
                List<FamillesClass> familleList = context.FamillesClass.ToList();
                FamilleClass.Clear();

                FamillesClass allFamille = new FamillesClass
                {
                    Id = -999,
                    Name = "Toutes les familles"
                };
                FamilleClass.Add(allFamille);

                foreach (FamillesClass society in familleList)
                {
                    FamilleClass.Add(society);
                }

                ComboBoxArticlesFamilleID.ItemsSource = FamilleClass;
                ComboBoxArticlesFamilleID.DisplayMemberPath = "Name";
                ComboBoxArticlesFamilleID.SelectedValuePath = "Id";


                ComboBoxArticlesFamilleID.SelectedValue = allFamille.Id;
            }
        }

        private void LoadComboboxFournisseurID()
        {
            using (VinContext context = new VinContext())
            {
                List<FournisseursClass> fournisseursList = context.FournisseursClass.ToList();
                fournisseursClass.Clear();

                FournisseursClass allFournisseurs = new FournisseursClass
                {
                    Id = -999,
                    Name = "Tous les fournisseurs"
                };

                fournisseursClass.Add(allFournisseurs);

                foreach (FournisseursClass fournisseur in fournisseursList)
                {
                    fournisseursClass.Add(fournisseur);
                }

                ComboBoxArticlesFournisseurID.ItemsSource = fournisseursClass;
                ComboBoxArticlesFournisseurID.DisplayMemberPath = "Name";
                ComboBoxArticlesFournisseurID.SelectedValuePath = "Id";
                ComboBoxArticlesFournisseurID.SelectedValue = allFournisseurs.Id;
            }
        }

        private void AddArticles_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateArticleInput())
            {
                using (var context = new VinContext())
                {
                    if (context.ArticlesClass.Any(c => c.Name == TextBoxArticlesName.Text))
                    {
                        ShowMessage(AddArticlesMessage, "Cet article existe déjà.", Colors.Red);
                        return;
                    }

                    var newArticle = new ArticlesClass
                    {
                        Name = TextBoxArticlesName.Text,
                        UnitPrice = int.Parse(TextBoxArticlesUnitPrice.Text),
                        QuantityStock = int.Parse(TextBoxArticlesQuantityStock.Text),
                        MinimumThreshold = int.Parse(TextBoxArticlesMinimumThreshold.Text),
                        FamillesClassId = (int)ComboBoxArticlesFamilleID.SelectedValue,
                        Reference = Guid.NewGuid(),
                        FournisseursClassId = (int)ComboBoxArticlesFournisseurID.SelectedValue,
                        Image = base64ImageString
                    };

                    context.ArticlesClass.Add(newArticle);
                    context.SaveChanges();
                    LoadArticles();
                    ClearInputFields();
                    ShowMessage(AddArticlesMessage, "L'article a été ajouté avec succès.", Colors.Green);
                }
            }
        }


        private void EditArticle_Click(object sender, RoutedEventArgs e)
        {
            editArticle = ((Button)sender).DataContext as ArticlesClass;
            FillInputFieldsForEditing(editArticle);
            ShowEditMode();
            DataGridArticlesMessage.Text = "";
            AddArticlesMessage.Text = "";
        }

        private void UpdateArticle_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateArticleInput())
            {
                using (var context = new VinContext())
                {
                    var articleToUpdate = context.ArticlesClass.FirstOrDefault(a => a.Id == editArticle.Id);

                    if (articleToUpdate != null)
                    {
                        articleToUpdate.Name = TextBoxArticlesName.Text;
                        articleToUpdate.UnitPrice = int.Parse(TextBoxArticlesUnitPrice.Text);
                        articleToUpdate.QuantityStock = int.Parse(TextBoxArticlesQuantityStock.Text);
                        articleToUpdate.MinimumThreshold = int.Parse(TextBoxArticlesMinimumThreshold.Text);
                        articleToUpdate.FamillesClassId = (int)ComboBoxArticlesFamilleID.SelectedValue;
                        articleToUpdate.FournisseursClassId = (int)ComboBoxArticlesFournisseurID.SelectedValue;

                        context.SaveChanges();
                        LoadArticles();
                        ClearInputFields();
                        ShowMessage(AddArticlesMessage, "L'article a été modifié avec succès.", Colors.Green);
                        ShowAddMode();
                        DataGridArticlesMessage.Text = "";
                    }
                }
            }
        }

        private void DeleteArticle_Click(object sender, RoutedEventArgs e)
        {
            var articleId = (int)((Button)sender).Tag;

            if (MessageBox.Show("Voulez-vous vraiment supprimer cet article ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                using (var context = new VinContext())
                {
                    var articleToDelete = context.ArticlesClass.FirstOrDefault(a => a.Id == articleId);
                    if (articleToDelete != null)
                    {
                        context.ArticlesClass.Remove(articleToDelete);
                        context.SaveChanges();
                        LoadArticles();
                        ShowMessage(DataGridArticlesMessage, "L'article a été supprimé avec succès.", Colors.Green);
                        AddArticlesMessage.Text = "";
                    }
                }
            }
        }

        private void CancelUpdateArticle_Click(object sender, RoutedEventArgs e)
        {
            ClearInputFields();
            ShowAddMode();
            AddArticlesMessage.Text = "";
            DataGridArticlesMessage.Text = "";
        }

        private void FillInputFieldsForEditing(ArticlesClass article)
        {
            TextBoxArticlesName.Text = article.Name;
            TextBoxArticlesUnitPrice.Text = article.UnitPrice.ToString();
            TextBoxArticlesQuantityStock.Text = article.QuantityStock.ToString();
            TextBoxArticlesMinimumThreshold.Text = article.MinimumThreshold.ToString();
            ComboBoxArticlesFamilleID.SelectedValue = article.FamillesClassId;
            ComboBoxArticlesFournisseurID.SelectedValue = article.FournisseursClassId;
        }

        private void ClearInputFields()
        {
            TextBoxArticlesName.Text = "";
            TextBoxArticlesUnitPrice.Text = "";
            TextBoxArticlesQuantityStock.Text = "";
            TextBoxArticlesMinimumThreshold.Text = "";
            ComboBoxArticlesFamilleID.SelectedIndex = -1;
            ComboBoxArticlesFournisseurID.SelectedIndex = -1;
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
                ComboBoxArticlesFournisseurID.SelectedIndex == 0 ||
                ComboBoxArticlesFamilleID.SelectedIndex == 0 ||
                string.IsNullOrWhiteSpace(base64ImageString))
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

        private void CommanderArticleFournisseurs_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                int articleId = (int)button.Tag;

                MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
                TabControl mainTabControl = (TabControl)mainWindow.FindName("MainTabControl");

                TabItem newTab = new TabItem();
                newTab.Header = CreateClosableHeader("Nouvelle Commande Fournisseur", newTab, mainTabControl);

                CommandeFournisseurs commandePage = new CommandeFournisseurs(articleId);
                Frame frame = new Frame();
                frame.Content = commandePage;

                newTab.Content = frame;

                mainTabControl.Items.Add(newTab);
                mainTabControl.SelectedItem = newTab;
            }
        }

        private StackPanel CreateClosableHeader(string headerText, TabItem tabItem, TabControl tabControl)
        {
            StackPanel headerPanel = new StackPanel { Orientation = Orientation.Horizontal };

            TextBlock textBlock = new TextBlock { Text = headerText };
            headerPanel.Children.Add(textBlock);

            Button closeButton = new Button
            {
                Content = "X",
                Width = 16,
                Height = 16,
                Margin = new Thickness(5, 0, 0, 0),
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                Cursor = Cursors.Hand
            };
            closeButton.Click += (s, e) => tabControl.Items.Remove(tabItem);

            headerPanel.Children.Add(closeButton);

            return headerPanel;
        }
    }
}
