using System.Collections.Generic;
namespace uTorrent.WebUI.Library.Objects.Interfaces
{
    public interface IWebUIManager
    {
        void SetConnectionInfo(IWebUIConnectionInfo connectionInfo);
        Response<IEnumerable<Torrent>> GetTorrentList();
        Response<IEnumerable<Torrent>> RemoveCompletedTorrents();
    }
}
