using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using uTorrent.WebUI.Library.Objects;

namespace uTorrent.WebUI.Shell.Data
{
    interface ITorrentRepository
    {
        IEnumerable<Torrent> GetAll();
        IEnumerable<Torrent> GetRecent();

        bool Add(Torrent torrent);
        bool Add(IEnumerable<Torrent> torrents);

        //Torrent Get(int id);
        //IEnumerable<Torrent> Get(Func<Torrent> condition);

        //bool Delete(Torrent torrent);
        //bool Delete(Func<Torrent> condition);

        //bool Update(Torrent torrent);
    }
}
