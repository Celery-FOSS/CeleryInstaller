namespace CeleryInstaller
{
    public class Configuration
    {
        public enum ExecutorPreference
        {
            NewUI,
            LegacyUI
        }

        public ExecutorPreference PreferedExecutor { get; set; }
        public string InstallLocation { get; set; }
    }
}
