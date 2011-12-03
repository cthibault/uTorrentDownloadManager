using System.Collections.Generic;
using System.Text;
using uTorrent.WebUI.Library.Objects;

namespace uTorrent.WebUI.Shell.Data
{
    public class TorrentRepositoryDummyData : ITorrentRepository
    {
        #region Implementation of ITorrentRepository

        public IEnumerable<Torrent> GetAll()
        {
            return new List<Torrent>
                       {
                           new Torrent("B2B415AF64F0423E3E234D803A25BAA7C10E3ED6") { Name = "The.Mentalist.S04E05.HDTV.XviD-ASAP.[VTV].avi" },
                           new Torrent("F038FECAA46CB653300C7BAB9A978BAC59457357") { Name = "The.Big.Bang.Theory.S05E06.HDTV.XviD-ASAP.avi" },
                       };
        }

        public bool Add(Torrent torrent)
        {
            return true;
        }

        public bool Add(IEnumerable<Torrent> torrents)
        {
            return true;
        }

        #endregion
    }
}
