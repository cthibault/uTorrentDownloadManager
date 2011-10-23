using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml.Linq;
using uTorrent.WebUI.Library.Objects;

namespace uTorrent.WebUI.Communication
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionInfo = new WebUIConnectionInfo();
            var manager = new WebUIManger(connectionInfo);
            var result = manager.RemoveCompletedTorrents();

            //var torrents = manager.GetTorrentList();
            //torrents.ToList().ForEach(t => Console.WriteLine(t.ToString()));
            Console.Read();
        }
    }
}