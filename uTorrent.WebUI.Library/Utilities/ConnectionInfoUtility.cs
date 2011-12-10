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
        private const string REPO_DIRECTORY = "repositoryDirectory";
        private const string LOG_DIRECTORY = "logDirectory";
        private const string VERBOSE_LOGGING = "verboseLogging";
        
        private const string USERNAME = "username";
        private const string PASSWORD = "password";
        private const string HOST_ADDRESS = "host_address";
        private const string PORT = "port";

        public static IWebUIConnectionInfo ConnectionInfo
        {
            get
            {
                var username = ConfigurationManager.AppSettings[USERNAME];
                var password = ConfigurationManager.AppSettings[PASSWORD];
                var address = ConfigurationManager.AppSettings[HOST_ADDRESS];
                var port = ConfigurationManager.AppSettings[PORT];

                return new WebUIConnectionInfo(username, password, address, port);
            }
        }

        public static string RepositoryLocation
        {
            get { return ConfigurationManager.AppSettings[REPO_DIRECTORY]; }
        }
        public static string LogLocation
        {
            get { return ConfigurationManager.AppSettings[LOG_DIRECTORY]; }
        }
        public static bool VerboseLogging
        {
            get
            {
                bool value = false;
                if (bool.TryParse(ConfigurationManager.AppSettings[VERBOSE_LOGGING], out value))
                    return value;
                return false;
            }
        }
    }
}
