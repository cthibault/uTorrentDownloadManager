using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using uTorrent.WebUI.Library.Constants;
using uTorrent.WebUI.Library.Objects.Interfaces;
using uTorrent.WebUI.Shell.Utilities;

namespace uTorrent.WebUI.Library.Objects
{
    public class WebUIManager : IWebUIManager
    {
        #region Private Fields

        private readonly IUnityContainer _container;
        private IWebUIConnectionInfo _connectionInfo;

        #endregion Private Fields

        #region Private Properties

        private IWebUIConnectionInfo ConnectionInfo
        {
            get
            {
                if (this._connectionInfo == null)
                    this._connectionInfo = ConnectionInfoUtility.ConnectionInfo;

                return this._connectionInfo;
            }
        }

        #endregion Private Properties

        #region Constructors

        public WebUIManager(IUnityContainer container, IWebUIConnectionInfo connectionInfo)
        {
            this._container = container;
            this._connectionInfo = connectionInfo;
        }

        #endregion Constructors

        #region IWebUIManager

        public void SetConnectionInfo(IWebUIConnectionInfo connectionInfo)
        {
            this._connectionInfo = connectionInfo;
        }

        public Response<IEnumerable<Torrent>> GetTorrentList()
        {
            var response = new Response<IEnumerable<Torrent>>(true);
            
            using (var client = _container.Resolve<IWebUIClient>(new ParameterOverride("connectionInfo", this.ConnectionInfo)))
            {
                try
                {
                    var result = client.SubmitRequest(WebUIActions.ListTorrents);
                    response.Value = WebUIJsonParser.GetTorrents(result);
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Exceptions.Add(ex);
                }
            }

            return response;
        }

        public Response<IEnumerable<Torrent>> RemoveCompletedTorrents()
        {
            var response = this.GetTorrentList();
            if (response.Success && response.Value != null)
            {
                var torrents = response.Value.Where(t => t.DownloadCompleted).ToList();

                response = new Response<IEnumerable<Torrent>>(true);
                if (torrents.Count > 0)
                {
                    using (var client = _container.Resolve<IWebUIClient>(new ParameterOverride("connectionInfo", this.ConnectionInfo)))
                    {
                        try
                        {
                            client.SubmitRequest(WebUIActions.RemoveTorrents, torrents.Select(t => t.Hash).ToArray());
                            torrents.ForEach(t => t.TimeCompleted = DateTime.Now);
                            response.Value = torrents;
                        }
                        catch (Exception ex)
                        {
                            response.Success = false;
                            response.Exceptions.Add(ex);
                        }
                    }
                }
            }

            return response;
        }

        #endregion IWebUIManager
    }
}
