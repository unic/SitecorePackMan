namespace Unic.PackMan.Core.Utilities
{
    public static class UriHelper
    {
        public static string GetUriWithoutQuery(string uri)
        {
            if (uri.Contains("?")) uri = uri.Substring(0, uri.IndexOf("?", System.StringComparison.InvariantCultureIgnoreCase));
            return uri;
        }
    }
}
