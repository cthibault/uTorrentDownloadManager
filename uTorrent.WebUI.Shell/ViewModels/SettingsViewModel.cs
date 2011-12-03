using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using uTorrent.WebUI.Library.Infrastructure.MVVM;
using uTorrent.WebUI.Shell.Models;
using uTorrent.WebUI.Shell.Models.Interfaces;
using uTorrent.WebUI.Shell.Views;
using uTorrent.WebUI.Shell.Views.Interfaces;
using uTorrent.WebUI.Shell.ViewModels.Interfaces;

namespace uTorrent.WebUI.Shell.ViewModels
{
    public class SettingsViewModel : BaseModel, ISettingsViewModel
    {
        #region Private Fields

        private readonly ISettingsView _view;

        #endregion Private Fields

        #region Public Properties

        public ISettingsModel Model { get; set; }

        public int HoursMinValue { get; set; }
        public int HoursMaxValue { get; set; }
        public int MinutesMinValue { get; set; }
        public int MinutesMaxValue { get; set; }

        #endregion Public Properties

        #region Constructors

        public SettingsViewModel(IUnityContainer container, ISettingsView view, ISettingsModel model) : base(container)
        {
            this.Initialize();

            this.Model = model;

            _view = view;
            this._view.DataContext = this;
            this._view.ShowDialog();
        }

        #endregion Constructors

        #region Initialization Methods

        private void Initialize()
        {
            this.HoursMinValue = 0;
            this.MinutesMinValue = 0;
            
            this.HoursMaxValue = 12;
            this.MinutesMaxValue = 59;
        }

        private void LoadModel()
        {
            
        }

        #endregion Initialization Methods
    }
}
