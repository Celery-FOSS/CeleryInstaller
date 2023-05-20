using System;
using System.Windows;
using System.Windows.Controls;

namespace CeleryInstaller.Controls
{
    public partial class InstallLocation : UserControl
    {
        public InstallLocation(string title, string description, RoutedEventHandler onClick)
        {
            InitializeComponent();
            TitleBox.Text = title;
            DescriptionBox.Text = description;
            MainButton.Click += onClick;
        }
    }
}
