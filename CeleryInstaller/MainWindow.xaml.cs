using CeleryInstaller.Dialogs;
using CeleryInstaller.Pages;
using CeleryInstaller.Utils;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CeleryInstaller 
{
    public partial class MainWindow 
    {
        List<UserControl> Pages;
        int CurrentPage;
        Configuration Configuration;

        public MainWindow() 
        {
            InitializeComponent();
            Configuration = new Configuration();
            Pages = new List<UserControl>
            {
                new StartupPage(),
                new ChoicePage(Configuration),
                new InstallLocationPage(Configuration),
                new DownloadingPage(Configuration)
            };
            CurrentPage = 0;
            BottomBar.Height = 0;
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Add(Pages[CurrentPage]);
            await Task.Delay(3500);
            AnimationUtils.AnimateHeight(BottomBar, 0, 35, AnimationUtils.EaseInOut, 400);
            NextPage();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            NextPage();
        }

        public bool CanContinue
        {
            get
            {
                return NextButton.IsEnabled;
            }
            set
            {
                NextButton.IsEnabled = value;
            }
        }

        public async void NextPage()
        {
            if (CurrentPage >= Pages.Count)
            {
                return;
            }
            CurrentPage++;
            UserControl currentPage = (UserControl)MainGrid.Children[0];
            UserControl newPage = Pages[CurrentPage];
            newPage.HorizontalAlignment = HorizontalAlignment.Left;
            MainGrid.Children.Add(newPage);
            AnimationUtils.AnimateMargin(newPage, new Thickness(MainGrid.ActualWidth, 0, 0, 0), new Thickness(), AnimationUtils.EaseInOut);
            AnimationUtils.AnimateMargin(currentPage, currentPage.Margin, new Thickness(-currentPage.ActualWidth, 0, 0, 0), AnimationUtils.EaseInOut);
            await Task.Delay(500);
            MainGrid.Children.Remove(currentPage);
        }

        public async void PreviousPage()
        {
            if (CurrentPage <= 1)
            {
                return;
            }
            CurrentPage--;
            UserControl currentPage = (UserControl)MainGrid.Children[0];
            UserControl newPage = Pages[CurrentPage];
            newPage.HorizontalAlignment = HorizontalAlignment.Left;
            MainGrid.Children.Add(newPage);
            AnimationUtils.AnimateMargin(newPage, new Thickness(-MainGrid.ActualWidth, 0, 0, 0), new Thickness(), AnimationUtils.EaseInOut);
            AnimationUtils.AnimateMargin(currentPage, currentPage.Margin, new Thickness(currentPage.ActualWidth, 0, 0, 0), AnimationUtils.EaseInOut);
            await Task.Delay(500);
            MainGrid.Children.Remove(currentPage);
        }
        
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            PreviousPage();
        }
    }
}