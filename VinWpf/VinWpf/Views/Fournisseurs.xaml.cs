using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VinWpf.DataSet;

namespace VinWpf.Views
{
    /// <summary>
    /// Logique d'interaction pour Fournisseurs.xaml
    /// </summary>
    public partial class Fournisseurs : Page
    {
        public ObservableCollection<FournisseursClass> FournisseursList { get; set; }
        public Fournisseurs()
        {
            InitializeComponent();
            FournisseursList = new ObservableCollection<FournisseursClass>();
            this.DataContext = this;
            LoadFournisseurs();
        }

        private void LoadFournisseurs()
        {
            using (var context = new PhishingContext())
            {
                var Fournisseurs = context.FournisseursClass.ToList();
                FournisseursList.Clear();
                foreach (var fournisseurs in Fournisseurs)
                {
                    FournisseursList.Add(fournisseurs);
                }
            }
        }

        private void AddFournisseurs_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxFournisseursName.Text))
            {
                AddFournisseursMessage.Foreground = new SolidColorBrush(Colors.Red);
                AddFournisseursMessage.Text = "Veuillez entrer le nom du Fournisseurs.";
                DataGridFournisseursMessage.Text = "";
                return;
            }

            if (string.IsNullOrEmpty(TextBoxFournisseursAddress.Text))
            {
                AddFournisseursMessage.Foreground = new SolidColorBrush(Colors.Red);
                AddFournisseursMessage.Text = "Veuillez entrer l'adresse du Fournisseurs.";
                DataGridFournisseursMessage.Text = "";
                return;
            }

            if (string.IsNullOrEmpty(TextBoxFournisseursPhone.Text))
            {
                AddFournisseursMessage.Foreground = new SolidColorBrush(Colors.Red);
                AddFournisseursMessage.Text = "Veuillez entrer le numéro de téléphone du Fournisseurs.";
                DataGridFournisseursMessage.Text = "";
                return;
            }

            if (string.IsNullOrEmpty(TextBoxFournisseursEmail.Text))
            {
                AddFournisseursMessage.Foreground = new SolidColorBrush(Colors.Red);
                AddFournisseursMessage.Text = "Veuillez entrer l'adresse email du Fournisseurs.";
                DataGridFournisseursMessage.Text = "";
                return;
            }

            using (PhishingContext context = new PhishingContext())
            {
                bool addressExists = context.FournisseursClass.Any(u => u.Address == TextBoxFournisseursAddress.Text);
                bool emailExists = context.FournisseursClass.Any(u => u.Email == TextBoxFournisseursEmail.Text);
                bool phoneExists = context.FournisseursClass.Any(u => u.Phone == TextBoxFournisseursPhone.Text);
                if (addressExists == true)
                {
                    AddFournisseursMessage.Foreground = new SolidColorBrush(Colors.Red);
                    AddFournisseursMessage.Text = "Cette adresse est déjà utilisée.";
                    return;
                }

                else if (phoneExists == true)
                {
                    AddFournisseursMessage.Foreground = new SolidColorBrush(Colors.Red);
                    AddFournisseursMessage.Text = "Ce numéro de téléphone est déjà utilisé.";
                    return;
                }

                else if (emailExists == true)
                {
                    AddFournisseursMessage.Foreground = new SolidColorBrush(Colors.Red);
                    AddFournisseursMessage.Text = "Cet email est déjà utilisé.";
                    return;
                }

                context.FournisseursClass.Add(new FournisseursClass
                {
                    Name = TextBoxFournisseursName.Text,
                    Address = TextBoxFournisseursAddress.Text,
                    Phone = TextBoxFournisseursPhone.Text,
                    Email = TextBoxFournisseursEmail.Text
                });
                context.SaveChanges();
                LoadFournisseurs();
                AddFournisseursMessage.Text = "Le Fournisseurs a été avec succès.";
                AddFournisseursMessage.Foreground = new SolidColorBrush(Colors.Green);
                TextBoxFournisseursName.Text = "";
                TextBoxFournisseursAddress.Text = "";
                TextBoxFournisseursPhone.Text = "";
                TextBoxFournisseursEmail.Text = "";
            }
        }

        private FournisseursClass editFournisseurs;

        private void EditFournisseurs_Click(object sender, RoutedEventArgs e)
        {
            editFournisseurs = ((Button)sender).DataContext as FournisseursClass;
            TextBoxFournisseursName.Text = editFournisseurs.Name;
            TextBoxFournisseursAddress.Text = editFournisseurs.Address;
            TextBoxFournisseursPhone.Text = editFournisseurs.Phone;
            TextBoxFournisseursEmail.Text = editFournisseurs.Email;

            AddFournisseursMessage.Text = "";
            AddEditFournisseursText.Text = "Modifier le Fournisseurs";
            UpdateFournisseursButton.Visibility = Visibility.Visible;
            AddFournisseursButton.Visibility = Visibility.Collapsed;
            CancelUpdateFournisseursButton.Visibility = Visibility.Visible;
        }

        private void UpdateFournisseurs_Click(object sender, RoutedEventArgs e)
        {
            using (PhishingContext context = new PhishingContext())
            {
                if (string.IsNullOrEmpty(TextBoxFournisseursName.Text))
                {
                    AddFournisseursMessage.Foreground = new SolidColorBrush(Colors.Red);
                    AddFournisseursMessage.Text = "Veuillez entrer le nom du Fournisseurs.";
                    DataGridFournisseursMessage.Text = "";
                    return;
                }

                if (string.IsNullOrEmpty(TextBoxFournisseursAddress.Text))
                {
                    AddFournisseursMessage.Foreground = new SolidColorBrush(Colors.Red);
                    AddFournisseursMessage.Text = "Veuillez entrer l'adresse du Fournisseurs.";
                    DataGridFournisseursMessage.Text = "";
                    return;
                }

                if (string.IsNullOrEmpty(TextBoxFournisseursPhone.Text))
                {
                    AddFournisseursMessage.Foreground = new SolidColorBrush(Colors.Red);
                    AddFournisseursMessage.Text = "Veuillez entrer le numéro de téléphone du Fournisseurs.";
                    DataGridFournisseursMessage.Text = "";
                    return;
                }

                if (string.IsNullOrEmpty(TextBoxFournisseursEmail.Text))
                {
                    AddFournisseursMessage.Foreground = new SolidColorBrush(Colors.Red);
                    AddFournisseursMessage.Text = "Veuillez entrer l'adresse email du Fournisseurs.";
                    DataGridFournisseursMessage.Text = "";
                    return;
                }

                bool addressExists = context.FournisseursClass.Any(u => u.Address == TextBoxFournisseursAddress.Text && u.Id != editFournisseurs.Id);
                bool emailExists = context.FournisseursClass.Any(u => u.Email == TextBoxFournisseursEmail.Text && u.Id != editFournisseurs.Id);
                bool phoneExists = context.FournisseursClass.Any(u => u.Phone == TextBoxFournisseursPhone.Text && u.Id != editFournisseurs.Id);

                if (addressExists)
                {
                    AddFournisseursMessage.Foreground = new SolidColorBrush(Colors.Red);
                    AddFournisseursMessage.Text = "Cette adresse est déjà utilisée.";
                    return;
                }

                if (phoneExists)
                {
                    AddFournisseursMessage.Foreground = new SolidColorBrush(Colors.Red);
                    AddFournisseursMessage.Text = "Ce numéro de téléphone est déjà utilisé.";
                    return;
                }

                if (emailExists)
                {
                    AddFournisseursMessage.Foreground = new SolidColorBrush(Colors.Red);
                    AddFournisseursMessage.Text = "Cet email est déjà utilisé.";
                    return;
                }

                var FournisseursUpdate = context.FournisseursClass.FirstOrDefault(u => u.Id == editFournisseurs.Id);
                FournisseursUpdate.Name = TextBoxFournisseursName.Text;
                FournisseursUpdate.Address = TextBoxFournisseursAddress.Text;
                FournisseursUpdate.Phone = TextBoxFournisseursPhone.Text;
                FournisseursUpdate.Email = TextBoxFournisseursEmail.Text;

                context.SaveChanges();
                LoadFournisseurs();

                AddFournisseursMessage.Text = "Le Fournisseurs a été modifié avec succès.";
                AddFournisseursMessage.Foreground = new SolidColorBrush(Colors.Green);
                TextBoxFournisseursName.Text = "";
                TextBoxFournisseursAddress.Text = "";
                TextBoxFournisseursPhone.Text = "";
                TextBoxFournisseursEmail.Text = "";
                AddEditFournisseursText.Text = "Ajouter un Fournisseurs";
                UpdateFournisseursButton.Visibility = Visibility.Collapsed;
                AddFournisseursButton.Visibility = Visibility.Visible;
                CancelUpdateFournisseursButton.Visibility = Visibility.Collapsed;
            }
        }

        private void CancelUpdateFournisseurs_Click(object sender, RoutedEventArgs e)
        {
            TextBoxFournisseursName.Text = "";
            TextBoxFournisseursAddress.Text = "";
            TextBoxFournisseursPhone.Text = "";
            TextBoxFournisseursEmail.Text = "";
            AddFournisseursMessage.Text = "";
            AddEditFournisseursText.Text = "Ajouter un Fournisseurs";
            DataGridFournisseursMessage.Text = "";
            UpdateFournisseursButton.Visibility = Visibility.Collapsed;
            AddFournisseursButton.Visibility = Visibility.Visible;
            CancelUpdateFournisseursButton.Visibility = Visibility.Collapsed;
        }

        private void DeleteFournisseurs_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Voulez-vous vraiment supprimer ce Fournisseurs ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var FournisseursId = (int)((Button)sender).Tag;

                using (PhishingContext context = new PhishingContext())
                {
                    var FournisseursToDelete = context.FournisseursClass.FirstOrDefault(c => c.Id == FournisseursId);
                    context.FournisseursClass.Remove(FournisseursToDelete);
                    context.SaveChanges();
                    LoadFournisseurs();
                    DataGridFournisseursMessage.Foreground = new SolidColorBrush(Colors.Green);
                    DataGridFournisseursMessage.Text = "Le Fournisseurs a été supprimé avec succès.";
                    TextBoxFournisseursName.Text = "";
                    TextBoxFournisseursAddress.Text = "";
                    TextBoxFournisseursPhone.Text = "";
                    TextBoxFournisseursEmail.Text = "";
                    AddFournisseursMessage.Text = "";
                    AddEditFournisseursText.Text = "Ajouter un Fournisseurs";
                    UpdateFournisseursButton.Visibility = Visibility.Collapsed;
                    AddFournisseursButton.Visibility = Visibility.Visible;
                    CancelUpdateFournisseursButton.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
