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
        private readonly TimeSpan _timeout;
        private ICredentials _credentials = null;

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

        #region IWebUIConnectionInfo

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

        public TimeSpan Timeout
        {
            get { return this._timeout; }
        }

        #endregion IWebUIConnectionInfo

        #region Constructors

        public WebUIConnectionInfo(string username, string password, string hostAddress, string portAddress)
        {
            this._username = username;
            this._password = password;
            this._hostAddress = hostAddress;
            this._portAddress = portAddress;
            this._timeout = new TimeSpan(0, 0, 15, 0);
        }

        #endregion Constructors
    }
}
