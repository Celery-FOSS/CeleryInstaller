using CeleryInstaller.FileTreeView.ShellClasses;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CeleryInstaller.Dialogs
{
    public partial class FolderDialogWindow : Window
    {
        public event EventHandler<DialogClosedEventArgs> DialogClosed;
        public string CurrentPath { get; set; }

        public FolderDialogWindow()
        {
            InitializeComponent();
            DataContext = this;
            FolderList = new ObservableCollection<FolderItem>();
            InitializeFileSystemObjects();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            if (DialogClosed != null)
            {
                DialogClosed(this, new DialogClosedEventArgs
                {
                    Selected = false,
                    Path = ""
                });
            }
            Hide();
        }

        #region Events

        private void FileSystemObject_AfterExplore(object sender, System.EventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void FileSystemObject_BeforeExplore(object sender, System.EventArgs e)
        {
            Cursor = Cursors.Wait;
        }

        #endregion

        #region Methods

        private void InitializeFileSystemObjects()
        {
            var drives = DriveInfo.GetDrives();
            DriveInfo
                .GetDrives()
                .ToList()
                .ForEach(drive =>
                {
                    var fileSystemObject = new FileSystemObjectInfo(drive);
                    fileSystemObject.BeforeExplore += FileSystemObject_BeforeExplore;
                    fileSystemObject.AfterExplore += FileSystemObject_AfterExplore;
                    treeView.Items.Add(fileSystemObject);
                });
            PreSelect(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
        }

        private void PreSelect(string path)
        {
            if (!Directory.Exists(path))
            {
                return;
            }
            var driveFileSystemObjectInfo = GetDriveFileSystemObjectInfo(path);
            driveFileSystemObjectInfo.IsExpanded = true;
            PreSelect(driveFileSystemObjectInfo, path);
        }

        private void PreSelect(FileSystemObjectInfo fileSystemObjectInfo, string path)
        {
            foreach (var childFileSystemObjectInfo in fileSystemObjectInfo.Children)
            {
                var isParentPath = IsParentPath(path, childFileSystemObjectInfo.FileSystemInfo.FullName);
                if (isParentPath)
                {
                    if (string.Equals(childFileSystemObjectInfo.FileSystemInfo.FullName, path))
                    {
                        /* We found the item for pre-selection */
                    }
                    else
                    {
                        childFileSystemObjectInfo.IsExpanded = true;
                        PreSelect(childFileSystemObjectInfo, path);
                    }
                }
            }
        }

        #endregion

        #region Helpers

        private FileSystemObjectInfo GetDriveFileSystemObjectInfo(string path)
        {
            var directory = new DirectoryInfo(path);
            var drive = DriveInfo
                .GetDrives()
                .Where(d => d.RootDirectory.FullName == directory.Root.FullName)
                .FirstOrDefault();
            return GetDriveFileSystemObjectInfo(drive);
        }

        private FileSystemObjectInfo GetDriveFileSystemObjectInfo(DriveInfo drive)
        {
            foreach (var fso in treeView.Items.OfType<FileSystemObjectInfo>())
            {
                if (fso.FileSystemInfo.FullName == drive.RootDirectory.FullName)
                {
                    return fso;
                }
            }
            return null;
        }

        private bool IsParentPath(string path,
            string targetPath)
        {
            return path.StartsWith(targetPath);
        }

        #endregion

        public ObservableCollection<FolderItem> FolderList { get; set; }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            FileSystemObjectInfo obj = (FileSystemObjectInfo)e.NewValue;
            SelectedPathBox.Text = obj.FileSystemInfo.Name;
            CurrentPath = obj.FileSystemInfo.FullName;
            FolderList.Clear();
            try
            {
                foreach (string dir in Directory.GetDirectories(obj.FileSystemInfo.FullName))
                {
                    FolderList.Add(new FolderItem 
                    { 
                        Name = Path.GetFileName(dir), 
                        FullPath = dir 
                    });
                }
            }
            catch { }
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            FolderItem item = listBox.SelectedItem as FolderItem;
            FolderList.Clear();
            try
            {
                foreach (string dir in Directory.GetDirectories(item.FullPath))
                {
                    FolderList.Add(new FolderItem
                    {
                        Name = Path.GetFileName(dir),
                        FullPath = dir
                    });
                }
            }
            catch { }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            if (listBox.SelectedItem == null)
                return;

            SelectedPathBox.Text = ((FolderItem)listBox.SelectedItem).Name;
            CurrentPath = ((FolderItem)listBox.SelectedItem).FullPath;
        }

        private void SelectPathButton_Click(object sender, RoutedEventArgs e)
        {
            if (DialogClosed != null)
            {
                DialogClosed(this, new DialogClosedEventArgs
                {
                    Selected = true,
                    Path = CurrentPath
                });
            }
            Hide();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            DirectoryInfo di = Directory.GetParent(CurrentPath);
            if (di == null)
                return;

            FolderList.Clear();
            SelectedPathBox.Text = di.Name;
            CurrentPath = di.FullName;
            try
            {
                foreach (string dir in Directory.GetDirectories(di.FullName))
                {
                    FolderList.Add(new FolderItem
                    {
                        Name = Path.GetFileName(dir),
                        FullPath = dir
                    });
                }
            }
            catch { }
        }
    }

    public class FolderItem
    {
        public string FullPath { get; set; }
        public string Name { get; set; }
    }

    public class DialogClosedEventArgs : EventArgs
    {
        public bool Selected { get; set; }
        public string Path { get; set; }
    }
}
