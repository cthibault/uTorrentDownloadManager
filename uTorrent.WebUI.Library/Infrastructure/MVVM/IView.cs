using System.Windows.Threading;

namespace uTorrent.WebUI.Library.Infrastructure.MVVM
{
    public interface IView
    {
        object DataContext { get; set; }
        Dispatcher Dispatcher { get; }
    }
}
