using System.IO;

namespace CeleryInstaller.FileTreeView.ShellClasses
{
    internal class DummyFileSystemObjectInfo : FileSystemObjectInfo
    {
        public DummyFileSystemObjectInfo() : base(new DirectoryInfo("DummyFileSystemObjectInfo"))
        { }
    }
}
