using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uTorrent.WebUI.Library.Constants
{
    sealed class TorrentPropertyJsonIndex
    {
        public const int HASH = 0;
        public const int STATUS = 1;
        public const int NAME = 2;
        public const int SIZE = 3;
        public const int PERCENT_PROGRESS = 4;
        public const int DOWNLOADED = 5;
        public const int UPLOADED = 6;
        public const int RATIO = 7;
        public const int UPLOAD_SPEED = 8;
        public const int DOWNLOAD_SPEED = 9;
        public const int ETA = 10;
        public const int LABEL = 11;
        public const int PEERS_CONNECTED = 12;
        public const int PEERS_IN_SWARM = 13;
        public const int SEEDS_CONNECTED = 14;
        public const int SEEDS_IN_SWARM = 15;
        public const int AVAILABILITY = 16;
        public const int TORRENT_QUEUE_ORDER = 17;
        public const int REMAINING = 18;
    }
}