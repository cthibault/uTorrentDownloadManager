using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using uTorrent.WebUI.Shell.Views.Interfaces;

namespace uTorrent.WebUI.Shell.ViewModels.Interfaces
{
    interface IMainWindowViewModel
    {
        IMainWindowView View { get; }

    }
}
