using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using uTorrent.WebUI.Library.Objects.Interfaces;
using uTorrent.WebUI.Library.Constants;
using System.Text;
using System.Xml.Linq;

namespace uTorrent.WebUI.Library.Objects
{
    public class WebUIClient : WebClient, IWebUIClient
    {
        #region Private Fields

        private readonly IWebUIConnectionInfo _connectionInfo;
        private string _token = string.Empty;
        private DateTime? _tokenTimestamp = null;

        #endregion Private Fields

        #region Private Properties

        private string AddressPrefix
        {
            get
            {
                return string.Format("http://{0}:{1}/gui/", this._connectionInfo.HostAddress, this._connectionInfo.PortAddress);
            }
        }

        #endregion Private Properties

        #region Constructors

        public WebUIClient(IWebUIConnectionInfo connectionInfo)
        {
            this._connectionInfo = connectionInfo;
            this.Credentials = connectionInfo.Credentials;
        }

        #endregion Constructors

        #region IWebUIClient

        public string SubmitRequest(WebUIActions action, params string[] hashes)
        {
            var request = this.BuildRequest(action, hashes);
            return this.ExecuteRequest(request);
        }

        #endregion IWebUIClient

        #region Private Methods

        private string ExecuteRequest(string request)
        {
            return base.DownloadString(request);
        }

        private string BuildRequest(WebUIActions action, IEnumerable<string> hashes)
        {
            var request = string.Empty;
            string token = string.Empty;

            switch (action)
            {
                case WebUIActions.ListTorrents:
                    // http://{IP}:{PORT}/gui/?token={TOKEN}&list=1

                    token = this.GetHashToken();
                    request = string.Format("{0}?token={1}&{2}", this.AddressPrefix, token, WebUIActionRequests.LIST);
                    break;

                case WebUIActions.RemoveTorrents:
                    // http://{IP}:{PORT}/gui/?token={TOKEN}&action=remove&hash={HASH1}&hash={HASH2}...

                    if (hashes != null && hashes.Count() > 0)
                    {
                        var hashBuilder = new StringBuilder();
                        hashes.ToList().ForEach(hash => hashBuilder.AppendFormat("&hash={0}", hash));
                        token = this.GetHashToken();
                        request = string.Format("{0}?token={1}&{2}{3}", 
                            this.AddressPrefix, token, WebUIActionRequests.REMOVE, hashBuilder.ToString());
                    }
                    break;
            }

            return request;
        }

        private string GetHashToken()
        {
            if (string.IsNullOrEmpty(this._token) || DateTime.Now - this._tokenTimestamp > this._connectionInfo.Timeout)
            {
                var response = this.ExecuteRequest(this.AddressPrefix + WebUIActionRequests.GET_TOKEN);
                this._token = this.ParseHashToken(response);
                this._tokenTimestamp = DateTime.Now;
            }

            return this._token;
        }

        private string ParseHashToken(string hashResponse)
        {
            // TODO: Parse using XML instead of string manipulation
            XDocument doc = XDocument.Parse(hashResponse);

            var temp = hashResponse.Split('>')[2];
            temp = temp.Remove(temp.IndexOf('<'));
            return temp;
        }

        #endregion Private Methods

        #region Private Structures

        private static class WebUIActionRequests
        {
            public const string LIST = "list=1";
            public const string REMOVE = "action=remove";
            public const string GET_TOKEN = "token.html";
        }

        #endregion Private Structures
    }
}