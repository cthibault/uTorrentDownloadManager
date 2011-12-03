using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using uTorrent.WebUI.Library.Objects;
using uTorrent.WebUI.Library.Objects.Interfaces;
using System.Configuration;

namespace uTorrent.WebUI.Shell.Utilities
{
    public static class ConnectionInfoUtility
    {
        private const string USERNAME = "username";
        private const string PASSWORD = "password";
        private const string HOST_ADDRESS = "host_address";
        private const string PORT = "port";

        public static IWebUIConnectionInfo GetConnectionInfo()
        {
            var username = ConfigurationManager.AppSettings[USERNAME];
            var password = ConfigurationManager.AppSettings[PASSWORD];
            var address = ConfigurationManager.AppSettings[HOST_ADDRESS];
            var port = ConfigurationManager.AppSettings[PORT];
            
            return new WebUIConnectionInfo(username, password, address, port);
        }

        public static void SaveConnectionInfo(IWebUIConnectionInfo connectionInfo)
        {
            
        }
    }
}
