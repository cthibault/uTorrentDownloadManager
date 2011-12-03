using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uTorrent.WebUI.Library.Objects
{
    public class Torrent
    {
        #region Private Fields

        private readonly string _hash = string.Empty;

        #endregion Private Fields

        #region Public Properties

        public string Hash
        {
            get { return this._hash; }
        }
        public string Name { get; set; }
        public int Status { get; set; }
        public int Size { get; set; }
        public int PercentProgress { get; set; }
        public int Downloaded { get; set; }
        public int DownloadSpeed { get; set; }
        public int ETA { get; set; }
        public string Label { get; set; }
        public int Remaining { get; set; }

        public DateTime? TimeCompleted { get; set; }

        public bool DownloadCompleted
        {
            get
            {
                return this.Remaining == 0 && PercentProgress == 1000;
            }
        }

        #endregion Public Properties

        #region Constructors

        public Torrent(string hash)
        {
            this._hash = hash;
        }

        #endregion Constructors

        #region Equality

        public bool Equals(Torrent other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other._hash, this._hash);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Torrent)) return false;
            return Equals((Torrent)obj);
        }

        public override int GetHashCode()
        {
            return (_hash != null ? _hash.GetHashCode() : 0);
        }

        #endregion Equality
    }
}
/*
HASH (string),
STATUS* (integer),
NAME (string),
SIZE (integer in bytes),
PERCENT PROGRESS (integer in per mils),
DOWNLOADED (integer in bytes),
UPLOADED (integer in bytes),
RATIO (integer in per mils),
UPLOAD SPEED (integer in bytes per second),
DOWNLOAD SPEED (integer in bytes per second),
ETA (integer in seconds),
LABEL (string),
PEERS CONNECTED (integer),
PEERS IN SWARM (integer),
SEEDS CONNECTED (integer),
SEEDS IN SWARM (integer),
AVAILABILITY (integer in 1/65536ths),
TORRENT QUEUE ORDER (integer),
REMAINING (integer in bytes)
*/