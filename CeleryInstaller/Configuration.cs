namespace CeleryInstaller
{
    public class Configuration
    {
        public enum ExecutorPreference : int
        {
            LegacyUI,
            NewUI
        }

        public ExecutorPreference PreferedExecutor { get; set; }
        public string InstallLocation { get; set; }
    }
}
