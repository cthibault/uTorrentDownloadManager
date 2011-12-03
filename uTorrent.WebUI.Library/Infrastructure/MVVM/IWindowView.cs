namespace uTorrent.WebUI.Library.Infrastructure.MVVM
{
    public interface IWindowView : IView
    {
        void Show();
        bool? ShowDialog();
    }
}
