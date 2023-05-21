using CeleryInstaller.Controls;
using CeleryInstaller.Dialogs;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CeleryInstaller.Pages
{
    public partial class InstallLocationPage : UserControl
    {
        public Configuration Configuration { get; set; }

        public bool HasWritePermission(string directory)
        {
            try
            {
                string file = Path.Combine(directory, "temp.txt");
                File.Create(file).Close();
                File.Delete(file);
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }

            return true;
        }

        public InstallLocationPage(Configuration configuration)
        {
            InitializeComponent();
            Configuration = configuration;
            LocationOptionsList.Children.Add(new InstallLocation("Current Directory", "Install Celery in the directory where the installer is located." , (s, e) =>
            {
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (HasWritePermission(path))
                {
                    configuration.InstallLocation = Path.Combine(path, "Celery");
                    PathBox.Text = configuration.InstallLocation;
                    App.Instance.CanContinue = true;
                }
                else
                {
                    MessageBox.Show("The Celery Installer doesn't have permission to write to that folder, try to start this application in Administrator mode.");
                }
            }));
            LocationOptionsList.Children.Add(new InstallLocation("Custom Directory", "Choose where you want Celery to install.", async (s, e) =>
            {
                FolderDialog fd = new FolderDialog();
                if (await fd.ShowDialog() == DialogResult.Ok)
                {
                    if (HasWritePermission(fd.FolderPath))
                    {
                        configuration.InstallLocation = Path.Combine(fd.FolderPath, "Celery");
                        PathBox.Text = configuration.InstallLocation;
                        App.Instance.CanContinue = true;
                    } 
                    else
                    {
                        MessageBox.Show("The Celery Installer doesn't have permission to write to that folder, try to start this application in Administrator mode.");
                    }
                }
            }));
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Only proceed when an InstallLocation is given.
            App.Instance.CanContinue = !string.IsNullOrEmpty(Configuration.InstallLocation);
        }
    }
}
