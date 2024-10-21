using System.Windows;

namespace VinWpf
{
    public partial class LoginWindow : Window
    {
        public event EventHandler LoginSuccessful;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (username == "timeo" && password == "blondeleau")
            {
                LoginSuccessful?.Invoke(this, EventArgs.Empty);
                this.Close();
            }
            else
            {
                MessageBox.Show("Identifiants incorrects, réessayez.");
            }
        }
    }
}
