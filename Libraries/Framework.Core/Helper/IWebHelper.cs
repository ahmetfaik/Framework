using System.Web;

namespace Framework.Core.Helper
{
    public interface IWebHelper
    {
        string GetUrlReferrer();
        string GetCurrentIpAddress();
        string GetThisPageUrl(bool includeQueryString);
        string GetThisPageUrl(bool includeQueryString, bool useSsl);
        bool IsCurrentConnectionSecured();
        string ServerVariables(string name);
        string GetHost(bool useSsl);
        string GetLocation();
        string GetLocation(bool useSsl);
        bool IsStaticResource(HttpRequest request);
        string ModifyQueryString(string url, string queryStringModification, string anchor);
        string RemoveQueryString(string url, string queryString);
        T QueryString<T>(string name);
        void RestartAppDomain(bool makeRedirect = false, string redirectUrl = "");
        bool IsRequestBeingRedirected { get; }
        bool IsPostBeingDone { get; set; }

    }
}
