using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Commands;
using System.Collections.ObjectModel;
using Microsoft.Practices.Unity;
using uTorrent.WebUI.Library.Infrastructure.Extensions;
using uTorrent.WebUI.Library.Infrastructure.MVVM;
using uTorrent.WebUI.Library.Objects.Interfaces;
using uTorrent.WebUI.Shell.Data;
using uTorrent.WebUI.Library.Objects;
using uTorrent.WebUI.Shell.Infrastructure;
using uTorrent.WebUI.Shell.Models.Interfaces;
using uTorrent.WebUI.Shell.Utilities;
using uTorrent.WebUI.Shell.ViewModels.Interfaces;

namespace uTorrent.WebUI.Shell.ViewModels
{
    public class TorrentListViewModel : BaseModel, ITorrentListViewModel
    {
        #region Private Fields

        private readonly Dispatcher _dispatcher;
        private BackgroundWorker _worker;
        private Timer _timer;
        private IWebUIManager _webManager;
        private ITorrentRepository _repository;
        private IEventLog _eventLog;
        private ISettingsModel _settingsModel;

        #endregion Private Fields

        #region Private Delegates

        private delegate void ProcessCompletedDelegate(Response<IEnumerable<Torrent>> response);

        #endregion Private Delegates

        #region Commands

        private DelegateCommand _removeCompletedTorrentsCommand;
        public DelegateCommand RemoveCompletedTorrentsCommand
        {
            get { return this._removeCompletedTorrentsCommand; }
        }

        private void InitializeCommands()
        {
            this._removeCompletedTorrentsCommand = new DelegateCommand(this.RemoveCompletedTorrents, this.RemoveCompletedTorrentsCanExecute);
        }
        
        private bool RemoveCompletedTorrentsCanExecute()
        {
            return !this.IsBusy;
        }

        #endregion Commands

        #region Public Properties

        public ObservableCollection<string> StatusMessages { get; private set; }
        public ObservableCollection<Torrent> Torrents { get; private set; }
        public IEnumerable<Torrent> SortedTorrents
        {
            get
            {
                if (this.Torrents != null)
                    return this.Torrents.OrderByDescending(t => t.TimeCompleted).ThenBy(t => t.Name);
                return new LinkedList<Torrent>();
            }
        }

        public ISettingsModel SettingsModel
        {
            get
            {
                if (this._settingsModel == null)
                    this.SettingsModel = this.Container.Resolve<ISettingsModel>();
                return this._settingsModel;
            }
            set { this._settingsModel = value; }
        }

        public bool IsBusy
        {
            get
            {
                if (this._worker != null)
                    return this._worker.IsBusy;
                return false;
            }
        }

        #endregion Public Properties

        #region Constructors

        public TorrentListViewModel(IUnityContainer container, Dispatcher dispatcher)
            : base(container)
        {
            this._dispatcher = dispatcher;
            this.InitializeModel();
        }

        #endregion Constructors

        #region Initialization Methods

        private void InitializeModel()
        {
            this.InitializeCommands();

            this.LoadSettingsModel();

            this._repository = this.Container.Resolve<ITorrentRepository>(new ParameterOverride("filepath", this.SettingsModel.RepositoryFile));
            this._eventLog = this.Container.Resolve<IEventLog>(new ParameterOverride("filepath", this.SettingsModel.LogFile));

            this.LoadModel();
            this.SetupTimer();
            this.SetupBackgroundWorker();
        }

        private void LoadModel()
        {
            this.StatusMessages = new ObservableCollection<string>();
            this.RefreshTorrentsFromRepository();
            this.RaisePropertyChanged(() => this.SortedTorrents);
        }

        private void LoadSettingsModel()
        {
            string directory = @"C:\DownloadManager\";
            this.SettingsModel.RepositoryFile = directory + "repo.xml";
            this.SettingsModel.LogFile = directory + "error.log";
            this.SettingsModel.VerboseLogging = true;
            this.SettingsModel.SetConnectionInfo(ConnectionInfoUtility.GetConnectionInfo());
        }

        private void RefreshTorrentsFromRepository()
        {
            this.Torrents = this._repository.GetRecent().ToObservable();
        }

        #endregion Initialization Methods

        #region Timing & Background Worker

        private void SetupTimer()
        {
            // TODO: Get timespan values into the settings model
            var due = new TimeSpan(0, 0, 0, 0);
            var period = new TimeSpan(0, 0, 2, 0);

            this._timer = new Timer(this.RemoveCompletedTorrents, null, due, period);
        }

        private void SetupBackgroundWorker()
        {
            if (this._worker == null)
            {
                this._worker = new BackgroundWorker { WorkerReportsProgress = false, WorkerSupportsCancellation = false };
                this._worker.DoWork += Worker_OnDoWork;
                this._worker.RunWorkerCompleted += Worker_OnRunWorkerCompleted;
            }
        }

        private void Worker_OnDoWork(object sender, DoWorkEventArgs e)
        {
            Response<IEnumerable<Torrent>> response = this.WebUIManager.RemoveCompletedTorrents();
            e.Result = response;
        }

        private void Worker_OnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this._dispatcher.BeginInvoke(new ProcessCompletedDelegate(this.RemoveCompletedTorrentsCompleted), e.Result);
        }

        #endregion Timing & Background Worker

        #region IWebUIManager

        private IWebUIManager WebUIManager
        {
            get
            {
                if (this._webManager == null)
                    this._webManager = this.Container.Resolve<IWebUIManager>(new ParameterOverride("connectionInfo", this.SettingsModel.ConnectionInfo));
                else if (this.SettingsModel.ForceCredentialsRefresh)
                    this._webManager.SetConnectionInfo(this.SettingsModel.ConnectionInfo);

                return this._webManager;
            }
        }

        public void RemoveCompletedTorrents()
        {
            this.RemoveCompletedTorrents(null);
        }

        private void RemoveCompletedTorrents(object state)
        {
            if (this._worker != null)
                this._worker.RunWorkerAsync();

            this.RaisePropertyChanged(() => this.IsBusy);
            this.RemoveCompletedTorrentsCommand.RaiseCanExecuteChanged();
        }

        private void RemoveCompletedTorrentsCompleted(Response<IEnumerable<Torrent>> completedResult)
        {
            string message = string.Empty;
            if (completedResult.Success)
            {
                if (completedResult.Value != null)
                {
                    var nonDuplicateTorrents = completedResult.Value.Where(torrent => !this.Torrents.Contains(torrent)).ToList();
                    this._repository.Add(nonDuplicateTorrents);
                }
            }
            else
            {
                message = "Error";
                completedResult.DisplayMessages.Add(this.TimestampStatusMessage(message));
                _eventLog.WriteToLog(completedResult);
            }

            this.RefreshTorrentsFromRepository();

            this.UpdateStatusMessage(message);
            this.RaisePropertyChanged(() => this.SortedTorrents);
            this.RaisePropertyChanged(() => this.IsBusy);
            this.RemoveCompletedTorrentsCommand.RaiseCanExecuteChanged();
        }

        #endregion IWebUIManager

        #region Status Message Methods

        private void UpdateStatusMessage(string message = "")
        {
            message = this.TimestampStatusMessage(message);
            this.StatusMessages.Add(message);

            if (this.SettingsModel.VerboseLogging)
                this._eventLog.WriteToLog(message);
        }

        private string TimestampStatusMessage(string message)
        {
            return string.IsNullOrEmpty(message) ? DateTime.Now.ToString() : string.Format("{0} - {1}", DateTime.Now, message);
        }

        #endregion Status Message Methods

        #region IDisposable

        public override void Dispose()
        {
            base.Dispose();

            // Dispose objects
            if (this._timer != null)
                this._timer.Dispose();
            if (this._worker != null)
                this._worker.Dispose();

            // UnRegister Events
        }

        #endregion IDisposable
    }
}
