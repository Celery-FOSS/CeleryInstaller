using System;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CeleryInstaller.Pages
{
    public partial class ChoicePage : UserControl
    {
        public Configuration Configuration { get; set; }

        public ChoicePage(Configuration configuration)
        {
            InitializeComponent();
            Configuration = configuration;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((string)((ComboBoxItem)ChoiceBox.SelectedItem).Content)
            {
                case "Legacy UI":
                    PreviewImage.Source = new BitmapImage(new Uri($"pack://application:,,,/{Assembly.GetExecutingAssembly().GetName().Name};component/Images/CeleryLegacy.png", UriKind.Absolute));
                    App.Instance.CanContinue = true;
                    Configuration.PreferedExecutor = Configuration.ExecutorPreference.LegacyUI;
                    break;
                case "New UI":
                    PreviewImage.Source = new BitmapImage(new Uri("https://raw.githubusercontent.com/sten-code/Celery/master/image.png"));
                    App.Instance.CanContinue = true;
                    Configuration.PreferedExecutor = Configuration.ExecutorPreference.NewUI;
                    break;
            }
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            App.Instance.CanContinue = ChoiceBox.SelectedItem != null;
        }
    }
}
