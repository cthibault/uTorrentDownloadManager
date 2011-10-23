using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using uTorrent.WebUI.Library.Objects.Interfaces;
using System.Net;

namespace uTorrent.WebUI.Library.Objects
{
    public class WebUIConnectionInfo : IWebUIConnectionInfo
    {
        #region Private Fields

        private readonly string _username = string.Empty;
        private readonly string _password = string.Empty;
        private readonly string _hostAddress = string.Empty;
        private readonly string _portAddress = string.Empty;
        private ICredentials _credentials;

        #endregion Private Fields

        #region Private Properties

        private string Username
        {
            get { return this._username; }
        }

        private string Password
        {
            get { return this._password; }
        }

        #endregion Private Properties

        #region Public Properties

        public ICredentials Credentials
        {
            get { return this._credentials ?? new NetworkCredential(this.Username, this.Password); }
        }

        public string HostAddress
        {
            get { return this._hostAddress; }
        }

        public string PortAddress
        {
            get { return this._portAddress; }
        }

        #endregion Public Properties

        #region Constructors

        public WebUIConnectionInfo()
        {
            this._username = "client";
            this._password = "client";
            this._hostAddress = "192.168.1.119";
            this._portAddress = "27214";
        }

        #endregion Constructors
    }
}
