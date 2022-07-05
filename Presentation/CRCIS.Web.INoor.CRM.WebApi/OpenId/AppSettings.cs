namespace CRCIS.Web.INoor.CRM.WebApi.OpenId
{
    /// <summary>
    /// AppSettings
    /// </summary>
    public class AppSettings : IAppSettings
    {
        /// <summary>
        /// Host options
        /// </summary>
        public HostOptions HostOptions { get; set; }

        /// <summary>
        /// Auth options
        /// </summary>
        public AuthOptions AuthOptions { get; set; }
    }
    /// <summary>
    /// Interface for AppSettings
    /// </summary>
    public interface IAppSettings
    {
        /// <summary>
        /// Host options
        /// </summary>
        HostOptions HostOptions { get; set; }
    }

    /// <summary>
    /// Host options
    /// </summary>
    public class HostOptions
    {
        /// <summary>
        /// Auth server
        /// </summary>
        /// <remarks>The url must be external IP, cannot be container name.</remarks>
        public string AuthServer { get; set; }

        ///// <summary>
        ///// Redis's host (For example, "jb.com:6379" or "35.123.45.123:6379")
        ///// </summary>
        //public string Redis { get; set; }
    }

    /// <summary>
    /// Auth Options
    /// </summary>
    public class AuthOptions
    {
        /// <summary>
        /// Audience
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Client id
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// Client Secret
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Frequency to refresh discovery document (hours)
        /// </summary>
        public int? RefreshDiscoveryDocDuration { get; set; }
    }
}
