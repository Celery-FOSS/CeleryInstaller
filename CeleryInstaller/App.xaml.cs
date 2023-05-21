using System;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Security.Authentication;
using System.Security.Principal;
using System.Windows;

namespace CeleryInstaller
{
    public partial class App
    {
        public static HttpClient HttpClient { get; private set; }
        public static MainWindow Instance { get; private set; }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            var identity = WindowsIdentity.GetCurrent();
            var winPrince = new WindowsPrincipal(identity);
            if (!winPrince.IsInRole(WindowsBuiltInRole.Administrator))
            {
                var result = MessageBox.Show("To avoid issues we recommend you to run the installer as administrator. Restart as administrator?", "Celery Installer", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    var proc = new Process { StartInfo = { FileName = Assembly.GetExecutingAssembly().Location, UseShellExecute = true, Verb = "runas" } };
                    proc.Start();
                    Environment.Exit(0);
                }    
            }

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
