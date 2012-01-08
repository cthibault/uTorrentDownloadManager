using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using uTorrent.WebUI.Library.Constants;

namespace uTorrent.WebUI.Library.Objects
{
    public static class WebUIJsonParser
    {
        public static IEnumerable<Torrent> GetTorrents(string input)
        {
            var torrents = new List<Torrent>();

            if (!string.IsNullOrEmpty(input))
            {
                var jobject = JObject.Parse(input);
                var ts = jobject["torrents"].Children();

                foreach (var torrent in ts)
                {
                    torrents.Add(new Torrent(torrent.ElementAt(TorrentPropertyJsonIndex.HASH).ToString())
                                 {
                                     Name = torrent.ElementAt(TorrentPropertyJsonIndex.NAME).ToString(),
                                     Status = (int) torrent.ElementAt(TorrentPropertyJsonIndex.STATUS),
                                     Size = (double) torrent.ElementAt(TorrentPropertyJsonIndex.SIZE),
                                     PercentProgress = (int) torrent.ElementAt(TorrentPropertyJsonIndex.PERCENT_PROGRESS),
                                     Downloaded = (double) torrent.ElementAt(TorrentPropertyJsonIndex.DOWNLOADED),
                                     DownloadSpeed = (int) torrent.ElementAt(TorrentPropertyJsonIndex.DOWNLOAD_SPEED),
                                     ETA = (int) torrent.ElementAt(TorrentPropertyJsonIndex.ETA),
                                     Label = torrent.ElementAt(TorrentPropertyJsonIndex.ETA).ToString(),
                                     Remaining = (double) torrent.ElementAt(TorrentPropertyJsonIndex.REMAINING)
                                 });
                }
            }

            return torrents;
        }
    }
}