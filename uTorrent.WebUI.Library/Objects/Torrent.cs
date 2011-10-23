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

        #region Protected Override Methods

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(string.Format(this.Name));
            builder.AppendLine(string.Format("  Label: {0}", this.Label));
            builder.AppendLine(string.Format("  Hash: {0}", this.Hash));
            builder.AppendLine(string.Format("  Status: {0}", this.Status));
            builder.AppendLine(string.Format("  Size: {0}", this.Size));
            builder.AppendLine(string.Format("  Percent Progress: {0}", this.PercentProgress));
            builder.AppendLine(string.Format("  Downloaded: {0}", this.Downloaded));
            builder.AppendLine(string.Format("  Remaining: {0}", this.Remaining));
            builder.AppendLine(string.Format("  Download Speed: {0}", this.DownloadSpeed));
            builder.Append(string.Format("  ETA: {0}", this.ETA));

            return builder.ToString();
        }

        #endregion Protected Override Methods
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