using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VinWpf.DataSet;

namespace VinWpf.Views
{
    public partial class Familles : Page
    {
        public ObservableCollection<FamillesClass> FamillesList { get; set; }

        public Familles()
        {
            InitializeComponent();
            FamillesList = new ObservableCollection<FamillesClass>();
            this.DataContext = this;
            LoadFamilles();
        }

        private void LoadFamilles()
        {
            using (var context = new PhishingContext())
            {
                var familles = context.FamillesClass.ToList();
                FamillesList.Clear();
                foreach (var famille in familles)
                {
                    FamillesList.Add(famille);
                }
            }
        }

        private void AddFamilles_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxFamillesName.Text))
            {
                AddFamillesMessage.Foreground = new SolidColorBrush(Colors.Red);
                AddFamillesMessage.Text = "Veuillez entrer le nom de la famille.";
                DataGridFamillesMessage.Text = "";
                return;
            }

            using (PhishingContext context = new PhishingContext())
            {
                bool nameExists = context.FamillesClass.Any(u => u.Name == TextBoxFamillesName.Text);
                if (nameExists)
                {
                    AddFamillesMessage.Foreground = new SolidColorBrush(Colors.Red);
                    AddFamillesMessage.Text = "Ce nom est déjà utilisé.";
                    return;
                }

                context.FamillesClass.Add(new FamillesClass
                {
                    Name = TextBoxFamillesName.Text
                });
                context.SaveChanges();
                LoadFamilles();
                AddFamillesMessage.Text = "La famille a été ajoutée avec succès.";
                AddFamillesMessage.Foreground = new SolidColorBrush(Colors.Green);
                TextBoxFamillesName.Text = "";
            }
        }

        private FamillesClass editFamille;

        private void EditFamilles_Click(object sender, RoutedEventArgs e)
        {
            editFamille = ((Button)sender).DataContext as FamillesClass;
            TextBoxFamillesName.Text = editFamille.Name;

            AddFamillesMessage.Text = "";
            AddEditFamillesText.Text = "Modifier la famille";
            UpdateFamillesButton.Visibility = Visibility.Visible;
            AddFamillesButton.Visibility = Visibility.Collapsed;
            CancelUpdateFamillesButton.Visibility = Visibility.Visible;
        }

        private void UpdateFamilles_Click(object sender, RoutedEventArgs e)
        {
            using (PhishingContext context = new PhishingContext())
            {
                if (string.IsNullOrEmpty(TextBoxFamillesName.Text))
                {
                    AddFamillesMessage.Foreground = new SolidColorBrush(Colors.Red);
                    AddFamillesMessage.Text = "Veuillez entrer le nom de la famille.";
                    DataGridFamillesMessage.Text = "";
                    return;
                }

                bool nameExists = context.FamillesClass.Any(u => u.Name == TextBoxFamillesName.Text && u.Id != editFamille.Id);

                if (nameExists)
                {
                    AddFamillesMessage.Foreground = new SolidColorBrush(Colors.Red);
                    AddFamillesMessage.Text = "Ce nom est déjà utilisé.";
                    return;
                }

                var familleToUpdate = context.FamillesClass.FirstOrDefault(u => u.Id == editFamille.Id);
                familleToUpdate.Name = TextBoxFamillesName.Text;

                context.SaveChanges();
                LoadFamilles();

                AddFamillesMessage.Text = "La famille a été modifiée avec succès.";
                AddFamillesMessage.Foreground = new SolidColorBrush(Colors.Green);
                TextBoxFamillesName.Text = "";
                AddEditFamillesText.Text = "Ajouter une famille";
                UpdateFamillesButton.Visibility = Visibility.Collapsed;
                AddFamillesButton.Visibility = Visibility.Visible;
                CancelUpdateFamillesButton.Visibility = Visibility.Collapsed;
            }
        }

        private void CancelUpdateFamilles_Click(object sender, RoutedEventArgs e)
        {
            TextBoxFamillesName.Text = "";
            AddFamillesMessage.Text = "";
            AddEditFamillesText.Text = "Ajouter une famille";
            DataGridFamillesMessage.Text = "";
            UpdateFamillesButton.Visibility = Visibility.Collapsed;
            AddFamillesButton.Visibility = Visibility.Visible;
            CancelUpdateFamillesButton.Visibility = Visibility.Collapsed;
        }

        private void DeleteFamilles_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Voulez-vous vraiment supprimer cette famille ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var familleId = (int)((Button)sender).Tag;

                using (PhishingContext context = new PhishingContext())
                {
                    var familleToDelete = context.FamillesClass.FirstOrDefault(c => c.Id == familleId);
                    context.FamillesClass.Remove(familleToDelete);
                    context.SaveChanges();
                    LoadFamilles();
                    DataGridFamillesMessage.Foreground = new SolidColorBrush(Colors.Green);
                    DataGridFamillesMessage.Text = "La famille a été supprimée avec succès.";
                    TextBoxFamillesName.Text = "";
                    AddFamillesMessage.Text = "";
                    AddEditFamillesText.Text = "Ajouter une famille";
                    UpdateFamillesButton.Visibility = Visibility.Collapsed;
                    AddFamillesButton.Visibility = Visibility.Visible;
                    CancelUpdateFamillesButton.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
