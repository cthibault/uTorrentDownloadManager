using System.Windows;
using Microsoft.Practices.Unity;
using uTorrent.WebUI.Shell.ViewModels.Interfaces;
using Microsoft.Practices.Unity.Configuration;

namespace uTorrent.WebUI.Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IUnityContainer container = new UnityContainer();
            container.LoadConfiguration();

            var mainWindowVM = container.Resolve<IMainWindowViewModel>();
            this.MainWindow = mainWindowVM.View as Window;
        }
    }
}
