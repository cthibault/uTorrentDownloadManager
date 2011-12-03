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
            List<Torrent> torrents = new List<Torrent>();

            if (!string.IsNullOrEmpty(input))
            {
                var jobject = JObject.Parse(input);
                var ts = jobject["torrents"].Children();

                ts.ToList().ForEach(t => torrents.Add(new Torrent(t.ElementAt(TorrentPropertyJsonIndex.HASH).ToString())
                                                      {
                                                          Name = t.ElementAt(TorrentPropertyJsonIndex.NAME).ToString(),
                                                          Status = (int) t.ElementAt(TorrentPropertyJsonIndex.STATUS),
                                                          Size = (int) t.ElementAt(TorrentPropertyJsonIndex.SIZE),
                                                          PercentProgress =
                                                              (int)
                                                              t.ElementAt(TorrentPropertyJsonIndex.PERCENT_PROGRESS),
                                                          Downloaded =
                                                              (int) t.ElementAt(TorrentPropertyJsonIndex.DOWNLOADED),
                                                          DownloadSpeed =
                                                              (int) t.ElementAt(TorrentPropertyJsonIndex.DOWNLOAD_SPEED),
                                                          ETA = (int) t.ElementAt(TorrentPropertyJsonIndex.ETA),
                                                          Label = t.ElementAt(TorrentPropertyJsonIndex.ETA).ToString(),
                                                          Remaining =
                                                              (int) t.ElementAt(TorrentPropertyJsonIndex.REMAINING)
                                                      })
                    );
            }
            return torrents;
        }
    }
}
