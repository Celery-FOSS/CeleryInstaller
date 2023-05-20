using CeleryInstaller.Controls;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Media;

namespace CeleryInstaller.Pages
{
    public partial class InstallLocationPage : System.Windows.Controls.UserControl
    {
        public Configuration Configuration { get; set; }

        public InstallLocationPage(Configuration configuration)
        {
            InitializeComponent();
            Configuration = configuration;
            LocationOptionsList.Children.Add(new InstallLocation("Current Directory", "Install Celery in the directory where the installer is located." , (s, e) =>
            {
                configuration.InstallLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Celery");
                PathBox.Text = configuration.InstallLocation;
                App.Instance.CanContinue = true;
                App.Instance.NextButton.Background = new SolidColorBrush(Color.FromRgb(37, 167, 50));
            }));
            LocationOptionsList.Children.Add(new InstallLocation("Custom Directory", "Choose where you want Celery to install.", (s, e) =>
            {
                using (FolderBrowserDialog dialog = new FolderBrowserDialog())
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        configuration.InstallLocation = Path.Combine(dialog.SelectedPath, "Celery");
                    }
                }
                PathBox.Text = configuration.InstallLocation;
                App.Instance.CanContinue = true;
                App.Instance.NextButton.Background = new SolidColorBrush(Color.FromRgb(37, 167, 50));
            }));
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Configuration.InstallLocation == "")
            {
                App.Instance.CanContinue = false;
                App.Instance.NextButton.Background = new SolidColorBrush(Color.FromRgb(28, 125, 37));
            } else
            {
                App.Instance.CanContinue = true;
                App.Instance.NextButton.Background = new SolidColorBrush(Color.FromRgb(37, 167, 50));
            }
        }
    }
}
