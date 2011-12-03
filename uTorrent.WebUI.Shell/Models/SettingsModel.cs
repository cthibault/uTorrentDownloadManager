using System;
using Microsoft.Practices.Unity;
using uTorrent.WebUI.Library.Infrastructure.MVVM;
using uTorrent.WebUI.Library.Objects;
using uTorrent.WebUI.Library.Objects.Interfaces;
using uTorrent.WebUI.Shell.Models.Interfaces;

namespace uTorrent.WebUI.Shell.Models
{
    public class SettingsModel : BaseModel, ISettingsModel
    {
        #region Constructors

        public SettingsModel(IUnityContainer container)
            : base(container)
        {
            this.UpdateCredentialHash();
        }

        #endregion Constructors

        #region Private Fields

        private string _repositoryFile = @"C:\DownloadManager\repo.xml";
        private string _logFile = @"C:\DownloadManager\log.xml";
        private bool _verboseLogging = false;

        private int _pollingHours = 0;
        private int _pollingMinutes = 10;

        private IWebUIConnectionInfo _connectionInfo = null;
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _hostAddress = string.Empty;
        private string _port = string.Empty;

        private int _credentialHash;

        #endregion Private Fields

        #region Repository

        public string RepositoryFile
        {
            get { return this._repositoryFile; }
            set
            {
                if (this._repositoryFile != value)
                {
                    this._repositoryFile = value;
                    this.RaisePropertyChanged(() => this.RepositoryFile);
                }
            }
        }

        #endregion Repository

        #region Logging

        public string LogFile
        {
            get { return this._logFile; }
            set
            {
                if (this._logFile != value)
                {
                    this._logFile = value;
                    this.RaisePropertyChanged(() => this.LogFile);
                }
            }
        }

        public bool VerboseLogging
        {
            get { return this._verboseLogging; }
            set
            {
                if (this._verboseLogging != value)
                {
                    this._verboseLogging = value;
                    this.RaisePropertyChanged(() => this.VerboseLogging);
                }
            }
        }

        #endregion Logging

        #region Polling Frequency

        public int PollingHours
        {
            get { return this._pollingHours; }
            set
            {
                if (this._pollingHours != value)
                {
                    this._pollingHours = value;
                    this.RaisePropertyChanged(() => this.PollingHours);
                }
            }
        }

        public int PollingMinutes
        {
            get { return this._pollingMinutes; }
            set
            {
                if (this._pollingMinutes != value)
                {
                    this._pollingMinutes = value;
                    this.RaisePropertyChanged(() => this.PollingMinutes);
                }
            }
        }

        public TimeSpan PollingFrequency
        {
            get { return new TimeSpan(0, this.PollingHours, this.PollingMinutes, 0); }
        }

        #endregion Polling Frequency

        #region Connection Info

        public string Username
        {
            get { return this._username; }
            set
            {
                if (this._username != value)
                {
                    this._username = value;
                    this.RaisePropertyChanged(() => this.Username);
                }
            }
        }

        public string Password
        {
            get { return this._password; }
            set
            {
                if (this._password != value)
                {
                    this._password = value;
                    this.RaisePropertyChanged(() => this.Password);
                }
            }
        }

        public string HostAddress
        {
            get { return this._hostAddress; }
            set
            {
                if (this._hostAddress != value)
                {
                    this._hostAddress = value;
                    this.RaisePropertyChanged(() => this.HostAddress);
                }
            }
        }

        public string Port
        {
            get { return this._port; }
            set
            {
                if (this._port != value)
                {
                    this._port = value;
                    this.RaisePropertyChanged(() => this.Port);
                }
            }
        }

        public bool ForceCredentialsRefresh
        {
            get { return this._credentialHash != this.EvaluateCredentialHash(); }
        }

        public IWebUIConnectionInfo ConnectionInfo
        {
            get
            {
                if (this._connectionInfo == null || this.ForceCredentialsRefresh)
                    return new WebUIConnectionInfo(this.Username, this.Password, this.HostAddress, this.Port);
                return this._connectionInfo;
            }
        }

        public void SetConnectionInfo(IWebUIConnectionInfo connectionInfo)
        {
            this._connectionInfo = connectionInfo;
            this.HostAddress = connectionInfo.HostAddress;
            this.Port = connectionInfo.PortAddress;

            this.UpdateCredentialHash();
        }

        public void UpdateCredentialHash()
        {
            unchecked
            {
                this._credentialHash = this.EvaluateCredentialHash();
            }
        }

        public int EvaluateCredentialHash()
        {
            unchecked
            {
                //return (this.Username.GetHashCode() * 397) ^
                //       (this.Password.GetHashCode() * 397) ^
                //       (this.HostAddress.GetHashCode() * 397) ^
                //       (this.Port.GetHashCode() * 397) ^
                //       (this.PollingFrequency.GetHashCode() * 397);

                int hash = 7;
                return hash;
            }
        }

        #endregion Connection Info
    }
}