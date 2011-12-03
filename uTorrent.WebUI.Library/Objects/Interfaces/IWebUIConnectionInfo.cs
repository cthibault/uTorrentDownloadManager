using System;
using System.Net;

namespace uTorrent.WebUI.Library.Objects.Interfaces
{
    public interface IWebUIConnectionInfo
    {
        ICredentials Credentials { get; }
        string HostAddress { get; }
        string PortAddress { get; }
        TimeSpan Timeout { get; }
    }
}
