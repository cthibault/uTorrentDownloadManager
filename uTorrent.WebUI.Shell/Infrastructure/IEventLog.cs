using System.Collections.Generic;
using System.Linq;
using System.Text;
using uTorrent.WebUI.Library.Objects;

namespace uTorrent.WebUI.Shell.Infrastructure
{
    interface IEventLog
    {
        void WriteToLog(string message);
        void WriteToLog(IEnumerable<string> messages);

        void WriteToLog<T>(Response<T> response);
    }
}
