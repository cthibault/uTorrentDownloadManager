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
using uTorrent.WebUI.Shell.Models.Interfaces;

namespace uTorrent.WebUI.Shell.Models
{
    public class TorrentListModel : BaseModel, ITorrentListModel
    {
        #region Private Fields

        private readonly Dispatcher _dispatcher;
        private BackgroundWorker _worker;
        private Timer _timer;
        private IWebUIManager _webManager;
        private ITorrentRepository _repository;

        #endregion Private Fields

        #region Private Delegates

        private delegate void ProcessCompletedDelegate(IEnumerable<Torrent> e);

        #endregion Private Delegates

        #region Commands

        private ICommand _removeCompletedTorrentsCommand;
        public ICommand RemoveCompletedTorrentsCommand
        {
            get { return this._removeCompletedTorrentsCommand; }
        }

        private void InitializeCommands()
        {
            this._removeCompletedTorrentsCommand = new DelegateCommand(this.RemoveCompletedTorrents, () => !this.IsBusy);
        }

        #endregion Commands

        #region Public Properties

        public ObservableCollection<string> StatusMessages { get; private set; }
        public ObservableCollection<Torrent> Torrents { get; private set; }

        public ISettingsModel Settings { get; private set; }

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

        public TorrentListModel(IUnityContainer container, Dispatcher dispatcher)
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

            this._webManager = this.Container.Resolve<IWebUIManager>();
            this._repository = this.Container.Resolve<ITorrentRepository>();

            this.LoadModel();
            this.InitializeBackgroundWorker();
            this.InitializeTimingMechanism();
        }

        private void LoadModel()
        {
            this.StatusMessages = new ObservableCollection<string>();
            this.Torrents = this._repository.GetAll().ToObservable();
        }

        private void InitializeBackgroundWorker()
        {
            this._worker = new BackgroundWorker { WorkerReportsProgress = false };
            this._worker.DoWork += (sender, e) =>
                                   {
                                       var newTorrents = new List<Torrent>();
                                       if (this._webManager != null)
                                           newTorrents.AddRange(this._webManager.GetTorrentList());
                                       e.Result = newTorrents;

                                       //double x = 0;
                                       //for (double i = 1; i < 1000000000; i++)
                                       //    x += Math.Sqrt(i);

                                       //e.Result = new List<Torrent>()
                                       //           {
                                       //               new Torrent(DateTime.Now.GetHashCode().ToString()) { Name = x.ToString() }
                                       //           };
                                   };
            this._worker.RunWorkerCompleted += (sender, e) =>
                                               {
                                                   var processCompleted = new ProcessCompletedDelegate(this.RemoveCompletedTorrentsCompleted);
                                                   this._dispatcher.BeginInvoke(processCompleted, e.Result);
                                               };
        }

        private void InitializeTimingMechanism()
        {
            if (this._timer == null)
            {
                // Check the Torrent downloads:
                // - On application start
                // - Every 15 minutes after
                this._timer = new Timer(this.RemoveCompletedTorrents, null, 100, 54000000);
            }
        }

        #endregion Initialization Methods

        #region Status Message Methods

        private void UpdateStatusMessage(DateTime dt)
        {
            this.StatusMessages.Add(string.Format("Last Update: {0}", dt));
        }
       
        private void UpdateStatusMessage(string message)
        {
            this.StatusMessages.Add(message);
        }

        #endregion Status Message Methods

        #region IWebUIManager

        public void RemoveCompletedTorrents()
        {
            this.RemoveCompletedTorrents(null);
        }

        private void RemoveCompletedTorrents(object state)
        {
            if (this._worker != null)
                this._worker.RunWorkerAsync();

            this.RaisePropertyChanged(() => this.IsBusy);
        }

        private void RemoveCompletedTorrentsCompleted(IEnumerable<Torrent> newTorrents)
        {
            foreach (var torrent in newTorrents.Where(torrent => !this.Torrents.Contains(torrent)))
                this.Torrents.Add(torrent);

            this.UpdateStatusMessage(DateTime.Now);
            this.RaisePropertyChanged(() => this.IsBusy);
        }

        #endregion IWebUIManager

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
