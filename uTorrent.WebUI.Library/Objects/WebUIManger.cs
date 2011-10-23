using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using uTorrent.WebUI.Library.Constants;
using uTorrent.WebUI.Library.Objects.Interfaces;
using System.Net;

namespace uTorrent.WebUI.Library.Objects
{
    public class WebUIManger : IWebUIManager
    {
        // TODO: Get torrents
        // TODO: Remove Completed Torrents

        #region Private Fields

        private readonly IWebUIConnectionInfo _connectionInfo;
        private readonly TimeSpan _tokenTimout = new TimeSpan(0, 15, 0);
        private string _token = string.Empty;
        private DateTime? _tokenTimeStamp = null;
        
        #endregion Private Fields

        #region Private Properties

        private IWebUIConnectionInfo ConnectionInfo
        {
            get { return this._connectionInfo; }
        }

        private string BaseAddress
        {
            get
            {
                return string.Format("http://{0}:{1}/gui/", this.ConnectionInfo.HostAddress, this.ConnectionInfo.PortAddress);

            }
        }

        private string BaseAddressWithToken
        {
            get
            {
                return string.Format("{0}?token={1}", this.BaseAddress, this.Token);

            }
        }

        private string Token
        {
            get
            {
                if (string.IsNullOrEmpty(this._token) || DateTime.Today - this.TokenTimeStamp > this._tokenTimout)
                {
                    this._token = this.GetHashToken();
                    this._tokenTimeStamp = DateTime.Today;
                }

                return this._token;
            }            
        }

        private DateTime? TokenTimeStamp
        {
            get
            {
                return this._tokenTimeStamp;
            }
        }

        #endregion Private Properties

        #region Constructors

        public WebUIManger(IWebUIConnectionInfo connectionInfo)
        {
            this._connectionInfo = connectionInfo;
        }

        #endregion Constructors

        #region Private Methods

        private string GetHashToken()
        {
            var hash = string.Empty;
            var request = this.BaseAddress + WebUIActions.GET_TOKEN;

            using (var client = new WebUIClient(this.ConnectionInfo.Credentials))
            {
                hash = client.DownloadString(request);
            }

            hash = hash.Split('>')[2];
            return hash.Remove(hash.IndexOf('<'));
        }

        private string ParseHashToken(string hashResponse)
        {
            var temp = hashResponse.Split('>')[2];
            return temp.Remove(temp.IndexOf('<'));
        }

        #endregion Private Methods

        #region Public Methods

        public IEnumerable<Torrent> GetTorrentList()
        {
            string response = string.Empty;
            var request = string.Format("{0}&{1}", this.BaseAddressWithToken, WebUIActions.LIST);
            
            using (var client = new WebUIClient(this.ConnectionInfo.Credentials))
            {
                response = client.DownloadString(request);
            }

            return WebUIJsonParser.GetTorrents(response);
        }

        public bool RemoveTorrents(IEnumerable<Torrent> torrents)
        {
            throw new NotImplementedException();
        }

        public bool RemoveCompletedTorrents()
        {
            bool success = false;
            var torrents = this.GetTorrentList().ToList() ?? new List<Torrent>();

            if (torrents.Any(t => t.DownloadCompleted))
            {
                var multipleHashes = new StringBuilder();
                torrents.Where(t => t.DownloadCompleted).ToList().ForEach(t => multipleHashes.AppendFormat("&hash={0}", t.Hash));
                var request = string.Format("{0}&{1}{2}", this.BaseAddressWithToken, WebUIActions.REMOVE, multipleHashes.ToString());

                using (var client = new WebUIClient(this.ConnectionInfo.Credentials))
                {

                    try
                    {
                        client.DownloadString(request);
                        success = true;
                    }
                    catch (Exception ex)
                    {   
                        throw ex;
                        success = false;
                    }
                }
            }

            return success;
        }

        #endregion Public Methods
    }
}
