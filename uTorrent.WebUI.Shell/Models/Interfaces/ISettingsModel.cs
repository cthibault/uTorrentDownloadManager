using uTorrent.WebUI.Library.Objects.Interfaces;
using System;

namespace uTorrent.WebUI.Shell.Models.Interfaces
{
    public interface ISettingsModel
    {
        string RepositoryFile { get; set; }
        string LogFile { get; set; }
        bool VerboseLogging { get; set; }

        TimeSpan PollingFrequency { get; }
        int PollingHours { get; set; }
        int PollingMinutes { get; set; }

        IWebUIConnectionInfo ConnectionInfo { get; }
        bool ForceCredentialsRefresh { get; }

        void SetConnectionInfo(IWebUIConnectionInfo connectionInfo);
    }
}