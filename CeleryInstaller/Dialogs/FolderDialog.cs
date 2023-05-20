using System.Diagnostics;
using System.Threading.Tasks;

namespace CeleryInstaller.Dialogs
{
    public class FolderDialog
    {
        public string DefaultPath { get; set; }
        public string FolderPath { get; private set; }

        public async Task<DialogResult> ShowDialog()
        {
            FolderDialogWindow window = new FolderDialogWindow();

            bool closed = false;
            bool selected = false;
            window.DialogClosed += (s, e) =>
            {
                Debug.WriteLine("closed");
                closed = true;
                FolderPath = e.Path;
                selected = e.Selected;
            };
            window.Show();

            while (!closed)
                await Task.Delay(10);
            return selected ? DialogResult.Ok : DialogResult.Cancel;
        }
    }

    public enum DialogResult
    {
        Ok,
        Cancel,
    }
}
