using CeleryInstaller.Utils;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CeleryInstaller.Pages
{
    public partial class StartupPage : UserControl
    {
        public StartupPage()
        {
            InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Thickness welcomeEnd = WelcomeLabel.Margin;
            WelcomeLabel.Margin = new Thickness(0, 80, -200, 0);

            Thickness celeryInstallerEnd = CeleryInstallerLabel.Margin;
            CeleryInstallerLabel.Margin = new Thickness(0, 130, -300, 0);

            Thickness celeryLogoEnd = CeleryLogo.Margin;
            CeleryLogo.Margin = new Thickness(-200, 0, 0, 0);

            // Starting
            await Task.Delay(500);
            AnimationUtils.AnimateMargin(WelcomeLabel, WelcomeLabel.Margin, welcomeEnd, AnimationUtils.EaseOut, 700);
            await Task.Delay(100);
            AnimationUtils.AnimateMargin(CeleryInstallerLabel, CeleryInstallerLabel.Margin, celeryInstallerEnd, AnimationUtils.EaseOut, 700);
            await Task.Delay(100);
            AnimationUtils.AnimateMargin(CeleryLogo, CeleryLogo.Margin, celeryLogoEnd, AnimationUtils.EaseOut, 700);

            // Pause
            await Task.Delay(1500);

            // Closing
            AnimationUtils.AnimateMargin(WelcomeLabel, WelcomeLabel.Margin, new Thickness(0, 80, -200, 0), AnimationUtils.EaseIn, 700);
            await Task.Delay(100);
            AnimationUtils.AnimateMargin(CeleryInstallerLabel, CeleryInstallerLabel.Margin, new Thickness(0, 130, -300, 0), AnimationUtils.EaseIn, 700);
            await Task.Delay(100);
            AnimationUtils.AnimateMargin(CeleryLogo, CeleryLogo.Margin, new Thickness(-200, 0, 0, 0), AnimationUtils.EaseIn, 700);

        }

    }
}
