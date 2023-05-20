namespace CeleryInstaller.Core {
    public class GithubRelease {
        /// <summary>
        ///     URL containing all information of the release.
        /// </summary>
        public string url { get; set; }
        /// <summary>
        ///     A listing of all the assets in this release.
        /// </summary>
        public string assets_url { get; set; }
        /// <summary>
        ///     The URL holding a link to the release (Github UI).
        /// </summary>
        public string html_url { get; set; }
        /// <summary>
        ///     The identifier of the release.
        /// </summary>
        public int id { get; set; }
        /// <summary>
        ///     The Author of the Release.
        /// </summary>
        public Author author { get; set; }
        /// <summary>
        ///     The tag given by the Author of the release.
        ///     Normally compatible with Semantic Versioning.
        /// </summary>
        public string tag_name { get; set; }
        /// <summary>
        ///     Name of the release.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        ///     Is this release a draft?
        /// </summary>
        public bool draft { get; set; }
        /// <summary>
        ///     Is this release a pre-release?
        /// </summary>
        public bool prerelease { get; set; }
        /// <summary>
        ///     When the release was created on ISO 8601 format: YYYY-MM-DDTHH:MM:SSZ.
        /// </summary>
        public string created_at { get; set; }
        /// <summary>
        ///     When the release was mad epublic on ISO 8601 format: YYYY-MM-DDTHH:MM:SSZ.
        /// </summary>
        public string published_at { get; set; }
        /// <summary>
        ///     All assets this release contains.
        /// </summary>
        public Asset[] assets { get; set; }
        /// <summary>
        ///     The tarball URL for this release (Source Code).
        /// </summary>
        public string tarball_url { get; set; }
        /// <summary>
        ///     The zipball URL for this release (Source Code).
        /// </summary>
        public string zipball_url { get; set; }
        /// <summary>
        ///     Text displayed on the release.
        /// </summary>
        public string body { get; set; }
    }

    public class Author {
        /// <summary>
        ///     The username of the user.
        /// </summary>
        public string login { get; set; }
        /// <summary>
        ///     The identifier of the user.
        /// </summary>
        public int id { get; set; }
        /// <summary>
        ///     The URL that points to the avatar of this user.
        /// </summary>
        public string avatar_url { get; set; }
        /// <summary>
        ///     The URL that points to the user's profile (API endpoint).
        /// </summary>
        public string url { get; set; }
        /// <summary>
        ///     The URL that points to the user's profile.
        /// </summary>
        public string html_url { get; set; }
        /// <summary>
        ///     The URL that points to the users following this user (API Endpoint).
        /// </summary>
        public string followers_url { get; set; }
        /// <summary>
        ///     The URL that points to the users that this user is following (API Endpoint).
        /// </summary>
        public string following_url { get; set; }
        /// <summary>
        ///     The URL that points to all the repositories this user has (API Endpoint).
        /// </summary>
        public string repos_url { get; set; }
        /// <summary>
        ///     The type of user (Normally, "User")
        /// </summary>
        public string type { get; set; }
        /// <summary>
        ///     Is this user a github administrator?
        /// </summary>
        public bool site_admin { get; set; }
    }

    public class Asset {
        /// <summary>
        ///     The URL containing information on this asset specifically (API Endpoint).
        /// </summary>
        public string url { get; set; }
        /// <summary>
        ///     The identifier of the asset.
        /// </summary>
        public int id { get; set; }
        /// <summary>
        ///     The name of the asset.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        ///     The user that uploaded this asset.
        /// </summary>
        public Author uploader { get; set; }
        /// <summary>
        ///     The type of content this file is
        /// </summary>
        public string content_type { get; set; }
        /// <summary>
        ///     The current state of this Asset, e.g. "uploaded".
        /// </summary>
        public string state { get; set; }
        /// <summary>
        ///     The size of this asset in bytes.
        /// </summary>
        public int size { get; set; }
        /// <summary>
        ///     The amount of times this asset has been downloaded.
        /// </summary>
        public int download_count { get; set; }
        /// <summary>
        ///     When the release was created on ISO 8601 format: YYYY-MM-DDTHH:MM:SSZ.
        /// </summary>
        public string created_at { get; set; }
        /// <summary>
        ///     When the release was mad epublic on ISO 8601 format: YYYY-MM-DDTHH:MM:SSZ.
        /// </summary>
        public string published_at { get; set; }
        /// <summary>
        ///     The download link for this asset.
        /// </summary>
        public string browser_download_url { get; set; }
    }
}