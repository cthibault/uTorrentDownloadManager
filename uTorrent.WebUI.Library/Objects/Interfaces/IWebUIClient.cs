using System;
using uTorrent.WebUI.Library.Constants;

namespace uTorrent.WebUI.Library.Objects.Interfaces
{
    public interface IWebUIClient : IDisposable
    {
        string SubmitRequest(WebUIActions action, params string[] hashes);
    }
}