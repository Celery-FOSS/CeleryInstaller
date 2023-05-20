using System.Net.Http;
using System.Security.Authentication;
using System.Windows;

namespace CeleryInstaller
{
    public partial class App
    {
        public static HttpClient HttpClient { get; private set; }
        public static MainWindow Instance { get; private set; }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            HttpClient = new HttpClient(new HttpClientHandler()
            {
                SslProtocols = SslProtocols.Tls12,
                UseCookies = false,
                UseProxy = false
            });
            HttpClient.DefaultRequestHeaders.Add("User-Agent", "Celery Installer");

            Instance = new MainWindow();
            Instance.Show();
        }
    }
}