using CeleryInstaller.Controls;
using CeleryInstaller.Dialogs;
using System.IO;
using System.Reflection;
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
            LocationOptionsList.Children.Add(new InstallLocation("Custom Directory", "Choose where you want Celery to install.", async (s, e) =>
            {
                FolderDialog fd = new FolderDialog();
                if (await fd.ShowDialog() == Dialogs.DialogResult.Ok)
                {
                    configuration.InstallLocation = Path.Combine(fd.FolderPath, "Celery");
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
