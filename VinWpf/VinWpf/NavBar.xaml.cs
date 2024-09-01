using System.Windows;
using System.Windows.Controls;

namespace VinWpf
{
    public partial class NavBar : UserControl
    {
        public NavBar()
        {
            InitializeComponent();
        }

        private void Button_Click_Clients(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).Content = new Liste_Clients();
        }

        private void Button_Click_Fournisseurs(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).Content = new Liste_Fournisseurs();
        }

        private void Button_Click_Familles(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).Content = new Liste_Familles();
        }

        private void Button_Click_Articles(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).Content = new Liste_Articles();
        }
    }
}
