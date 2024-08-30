using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VinWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Clients(object sender, RoutedEventArgs e)
        {
            this.Content = new Liste_Clients();
        }

        private void Button_Click_Fournisseurs(object sender, RoutedEventArgs e)
        {
            this.Content = new Liste_Fournisseurs();
        }

        private void Button_Click_Familles(object sender, RoutedEventArgs e)
        {
            this.Content = new Liste_Familles();
        }

        private void Button_Click_Articles(object sender, RoutedEventArgs e)
        {
            this.Content = new Liste_Articles();
        }
    }
}