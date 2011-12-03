using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using uTorrent.WebUI.Library.Constants;
using uTorrent.WebUI.Library.Objects.Interfaces;

namespace uTorrent.WebUI.Library.Objects
{
    public class WebUIClientDummyData : IWebUIClient
    {
        private string FilePath
        {
            get { return AppDomain.CurrentDomain.BaseDirectory + "torrentlist.txt"; }
        }

        #region Implementation of IWebUIClient

        public string SubmitRequest(WebUIActions action, params string[] hashes)
        {
            string result = string.Empty;
            switch (action)
            {
                case WebUIActions.ListTorrents:
                    result = this.ListTorrents();
                    break;

                case WebUIActions.RemoveTorrents:
                    this.RemoveTorrents(hashes);
                    break;
            }
            
            return result;
        }

        private string ListTorrents()
        {
            string fileContents = File.Exists(this.FilePath) ? File.ReadAllText(this.FilePath) : string.Empty;
            return fileContents;
        }

        private void RemoveTorrents(IEnumerable<string> hashes)
        {
            var fileContents = File.Exists(this.FilePath) ? File.ReadAllLines(this.FilePath) : new string[]{ };
            var newContents = fileContents.Where(l => !hashes.Any(h => l.Contains(h)));
            File.WriteAllLines(this.FilePath, newContents);
        }

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            
        }

        #endregion
    }
}
