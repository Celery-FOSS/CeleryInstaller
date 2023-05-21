using CeleryInstaller.Core;
using CeleryInstaller.Utils;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CeleryInstaller.Pages
{
    public partial class DownloadingPage : UserControl
    {
        public Configuration Configuration { get; set; }

        public DownloadingPage(Configuration configuration)
        {
            InitializeComponent();
            Configuration = configuration;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            AnimationUtils.AnimateHeight(App.Instance.BottomBar, 35, 0, AnimationUtils.EaseInOut, 400);

            // Install sequence here
            Debug.WriteLine("Downloading...");
            Debug.WriteLine(Configuration.InstallLocation);
            Debug.WriteLine(Configuration.PreferedExecutor);

            if (!Directory.Exists(Configuration.InstallLocation))
                Directory.CreateDirectory(Configuration.InstallLocation);

            string zipFile = Path.Combine(Configuration.InstallLocation, "Celery.zip");
            if (File.Exists(zipFile))
                File.Delete(zipFile);

            IProgress<string> progressString = new Progress<string>(s => StatusBar.Text = s);
            IProgress<float> progressFloat = new Progress<float>(p => ProgressBar.Value = p * 100);
            await Installer.InstallLatestVersion(Configuration, progressString, progressFloat);
            
            new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = Configuration.InstallLocation,
                    FileName = Path.Combine(Configuration.InstallLocation, Configuration.PreferedExecutor == Configuration.ExecutorPreference.NewUI ? "Celery.exe" : "CeleryApp.exe")
                }
            }.Start();
            Application.Current.Shutdown();
        }
    }
}
