using System.Windows;

namespace VinWpf
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            LoginWindow loginWindow = new LoginWindow();

            loginWindow.LoginSuccessful += (s, args) =>
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            };

            loginWindow.Show();
        }
    }
}
