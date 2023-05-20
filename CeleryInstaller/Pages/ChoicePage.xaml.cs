using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media;
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
                    App.Instance.NextButton.Background = new SolidColorBrush(Color.FromRgb(37, 167, 50));
                    Configuration.PreferedExecutor = Configuration.ExecutorPreference.LegacyUI;
                    break;
                case "New UI":
                    PreviewImage.Source = new BitmapImage(new Uri($"pack://application:,,,/{Assembly.GetExecutingAssembly().GetName().Name};component/Images/CeleryNew.png", UriKind.Absolute));
                    App.Instance.CanContinue = true;
                    App.Instance.NextButton.Background = new SolidColorBrush(Color.FromRgb(37, 167, 50));
                    Configuration.PreferedExecutor = Configuration.ExecutorPreference.NewUI;
                    break;
            }
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ChoiceBox.SelectedItem == null)
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
