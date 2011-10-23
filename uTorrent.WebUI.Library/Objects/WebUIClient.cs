using System.Net;

namespace uTorrent.WebUI.Library.Objects
{
    public class WebUIClient : WebClient
    {
        public WebUIClient(ICredentials credentials)
        {
            this.Credentials = credentials;
        }
    }
}