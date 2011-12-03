using System.Collections.ObjectModel;
using System.ComponentModel;
using uTorrent.WebUI.Library.Objects;
using uTorrent.WebUI.Shell.Models.Interfaces;

namespace uTorrent.WebUI.Shell.ViewModels.Interfaces
{
    public interface ITorrentListViewModel : INotifyPropertyChanged
    {
        ObservableCollection<string> StatusMessages { get; }
        ObservableCollection<Torrent> Torrents { get; }
        ISettingsModel SettingsModel { get; set; }
        void RemoveCompletedTorrents();
    }
}