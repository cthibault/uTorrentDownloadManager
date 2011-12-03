using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using uTorrent.WebUI.Shell.Models.Interfaces;

namespace uTorrent.WebUI.Shell.ViewModels.Interfaces
{
    interface ISettingsViewModel : IDisposable
    {
        ISettingsModel Model { get; }
    }
}
