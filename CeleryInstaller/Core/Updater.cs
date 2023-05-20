using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CeleryInstaller.Json;

namespace CeleryInstaller.Core
{
    public class Updater
    {
        public static async Task<Stream> GetLatestZip(Configuration.ExecutorPreference executor)
        {
            switch (executor)
            {
                case Configuration.ExecutorPreference.LegacyUI:
                    const string LegacyUiUrl = "https://cdn.sussy.dev/celery/release.zip";
                    return await App.HttpClient.GetStreamAsync(LegacyUiUrl);
                case Configuration.ExecutorPreference.NewUI:
                    const string NewUiUrl = "https://api.github.com/repos/sten-code/Celery/releases/latest";

                    var json = await App.HttpClient.GetStringAsync(NewUiUrl);

                    if (string.IsNullOrEmpty(json))
                        throw new Exception("Failed to obtain information for the New UI download!");

                    var releaseInformation = json.FromJson<GithubRelease>();

                    if (releaseInformation == null)
                        throw new Exception("Failed to obtain release information!");

                    // Get the first zip that this release contains.
                    var targetAsset = releaseInformation.assets.First(x => x.content_type == "application/x-zip-compressed");

                    return await App.HttpClient.GetStreamAsync(targetAsset.browser_download_url);
                default:
                    return Stream.Null;
            }
        }
    }
}   