using System.Collections.Generic;
namespace uTorrent.WebUI.Library.Objects.Interfaces
{
    public interface IWebUIManager
    {
        IEnumerable<Torrent> GetTorrentList();
        //bool RemoveTorrents();
        bool RemoveCompletedTorrents();
    }
}
