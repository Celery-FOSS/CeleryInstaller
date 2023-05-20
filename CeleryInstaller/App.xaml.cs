using System.Windows;

namespace CeleryInstaller 
{
    public partial class App
    {
        public static MainWindow Instance { get; set; }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            Instance = new MainWindow();
            Instance.Show();
        }
    }
}