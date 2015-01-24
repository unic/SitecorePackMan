namespace Unic.PackMan.Core.Utilities
{
    using System;

    /// <summary>
    /// Uri helper methods
    /// </summary>
    public static class UriHelper
    {
        /// <summary>
        /// Gets the item URI without query (version and language).
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>The cleaned, shared item URI</returns>
        public static string GetUriWithoutQuery(string uri)
        {
            return uri.Contains("?") ? uri.Substring(0, uri.IndexOf("?", StringComparison.InvariantCultureIgnoreCase)) : uri;
        }
    }
}
