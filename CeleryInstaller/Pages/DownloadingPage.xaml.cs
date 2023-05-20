using CeleryInstaller.Utils;
using System.Diagnostics;
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            AnimationUtils.AnimateHeight(App.Instance.BottomBar, 35, 0, AnimationUtils.EaseInOut, 400);

            // Install sequence here
            Debug.WriteLine("Downloading...");
            Debug.WriteLine(Configuration.InstallLocation);
            Debug.WriteLine(Configuration.PreferedExecutor);
        }
    }
}
