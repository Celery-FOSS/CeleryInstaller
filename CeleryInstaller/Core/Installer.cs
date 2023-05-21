using System;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CeleryInstaller.Json;

namespace CeleryInstaller.Core
{
    public static class Installer
    {
        private static async Task<string> GetLatestRelease(string url)
        {
            string json = await App.HttpClient.GetStringAsync(url);

            if (string.IsNullOrEmpty(json))
                throw new Exception("Failed to obtain information for the New UI download!");

            GithubRelease releaseInformation = json.FromJson<GithubRelease>();

            if (releaseInformation == null)
                throw new Exception("Failed to obtain release information!");

            Asset targetAsset = releaseInformation.assets.First(x => x.content_type == "application/x-zip-compressed");
            return targetAsset.browser_download_url;
        }

        public static async Task CopyToAsync(this Stream source, Stream destination, int bufferSize, IProgress<long> progress = null, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (!source.CanRead)
                throw new ArgumentException("Has to be readable", nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));
            if (!destination.CanWrite)
                throw new ArgumentException("Has to be writable", nameof(destination));
            if (bufferSize < 0)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));

            byte[] buffer = new byte[bufferSize];
            long totalBytesRead = 0;
            int bytesRead;
            while ((bytesRead = await source.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false)) != 0)
            {
                await destination.WriteAsync(buffer, 0, bytesRead, cancellationToken).ConfigureAwait(false);
                totalBytesRead += bytesRead;
                progress?.Report(totalBytesRead);
            }
        }
        private static async Task DownloadAsync(this HttpClient client, string requestUri, Stream destination, IProgress<string> progressString, IProgress<float> progressFloat, CancellationToken cancellationToken = default)
        {
            progressString.Report("Initializing download...");
            using (HttpResponseMessage response = await client.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead))
            {
                long? contentLength = response.Content.Headers.ContentLength;
                using (Stream downloadStream = await response.Content.ReadAsStreamAsync())
                {
                    if (!contentLength.HasValue)
                    {
                        await downloadStream.CopyToAsync(destination);
                        return;
                    }

                    IProgress<long> relativeProgress = new Progress<long>(totalBytes =>
                    {
                        float progress = (float)totalBytes / contentLength.Value;
                        progressFloat.Report(progress);
                        progressString.Report($"Downloading... {Math.Round(progress * 100)}%");
                    });

                    await downloadStream.CopyToAsync(destination, 81920, relativeProgress, cancellationToken);
                    progressString.Report("Download Complete!");
                }
            }
        }

        public static async Task InstallLatestVersion(Configuration configuration, IProgress<string> progressString, IProgress<float> progressFloat)
        {
            if (!Directory.Exists(configuration.InstallLocation))
                Directory.CreateDirectory(configuration.InstallLocation);

            string url = (configuration.PreferedExecutor == Configuration.ExecutorPreference.LegacyUI) ? "https://cdn.sussy.dev/celery/release.zip" : (await GetLatestRelease("https://api.github.com/repos/sten-code/Celery/releases/latest"));

            if (url == "")
                throw new Exception("Failed to fetch the download url!");

            using (MemoryStream memStream = new(8000000)) // 8000000 == 8 Megabytes
            {
                await App.HttpClient.DownloadAsync(url, memStream, progressString, progressFloat);
                memStream.Position = 0; // Reset the position of the MemoryStream to avoid future issues. 
                await Task.Run(() => {
                    // Extract the zip to the install location
                    using ZipArchive archive = new ZipArchive(memStream);
                    
                    ReadOnlyCollection<ZipArchiveEntry> entries = archive.Entries;
                    for (int i = 0; i < entries.Count; i++)
                    {
                        ZipArchiveEntry entry = entries[i];
                        string completeFileName = Path.Combine(configuration.InstallLocation, entry.FullName);
                        if (entry.Name == "")
                        {
                            Directory.CreateDirectory(completeFileName);
                            continue;
                        }

                        float progress = (float)(i + 1) / entries.Count;
                        progressFloat.Report(progress);
                        progressString.Report($"Extracting... {Math.Round(progress * 100)}%");
                        try {
                            entry.ExtractToFile(completeFileName, true);
                        }
                        catch { }
                    }
                    progressString.Report("Extraction Complete!");
                });
            }
        }

    }
}   
