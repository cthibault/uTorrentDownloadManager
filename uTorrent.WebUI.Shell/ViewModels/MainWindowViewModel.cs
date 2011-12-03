using System;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using uTorrent.WebUI.Library.Infrastructure.MVVM;
using uTorrent.WebUI.Shell.Models.Interfaces;
using uTorrent.WebUI.Shell.ViewModels.Interfaces;
using uTorrent.WebUI.Shell.Views.Interfaces;

namespace uTorrent.WebUI.Shell.ViewModels
{
    public class MainWindowViewModel : BaseModel, IMainWindowViewModel
    {
        #region Private Fields

        private readonly IMainWindowView _view;
        private Dispatcher _dispatcher;

        #endregion Private Fields

        #region Public Properties

        public IMainWindowView View
        {
            get { return this._view; }
        }

        public ITorrentListViewModel ViewModel { get; set; }

        public string StatusMessage
        {
            get { return this.ViewModel.StatusMessages.LastOrDefault() ?? string.Empty; }
        }

        #endregion Public Properties

        #region Commands

        private DelegateCommand _showSettingsCommand;
        public DelegateCommand ShowSettingsCommand
        {
            get { return this._showSettingsCommand; }
        }

        public void InitializeCommands()
        {
            this._showSettingsCommand = new DelegateCommand(OnDisplaySettings, () => false);
        }

        #endregion Commands

        #region Constructors

        public MainWindowViewModel(IUnityContainer container, IMainWindowView view)
            : base(container)
        {
            this._view = view;

            this.InitializeViewModel();
            this.ActivateView();
        }

        #endregion Constructors

        #region Initialization Methods

        private void ActivateView()
        {
            this._view.DataContext = this;
            this._view.Show();
        }

        private void InitializeViewModel()
        {
            this._dispatcher = this.View.Dispatcher;
            this.ViewModel = this.Container.Resolve<ITorrentListViewModel>(new ParameterOverride("dispatcher", this._dispatcher));
            this.ViewModel.StatusMessages.CollectionChanged += (o, e) => this.RaisePropertyChanged(() => this.StatusMessage);

            this.InitializeCommands();
        }

        #endregion Initialization Methods

        #region ITorrentListViewModel

        private void OnRemoveCompletedTorrents()
        {
            this.ViewModel.RemoveCompletedTorrents();
        }

        #endregion ITorrentListViewModel

        #region ISettingsViewModel

        private void OnDisplaySettings()
        {
            using (var settingsDialog = this.Container.Resolve<ISettingsViewModel>(new ParameterOverride("model", this.ViewModel.SettingsModel)))
            {
                this.ViewModel.SettingsModel = settingsDialog.Model;
            }
        }

        #endregion ISettingsViewModel

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
