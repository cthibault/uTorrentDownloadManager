using System.Collections.ObjectModel;
using System.ComponentModel;
using uTorrent.WebUI.Library.Objects;

namespace uTorrent.WebUI.Shell.Models.Interfaces
{
    public interface ITorrentListModel : INotifyPropertyChanged
    {
        ObservableCollection<string> StatusMessages { get; }
        ObservableCollection<Torrent> Torrents { get; }
        void RemoveCompletedTorrents();
    }
}