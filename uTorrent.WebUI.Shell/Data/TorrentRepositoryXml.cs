using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using uTorrent.WebUI.Library.Infrastructure.Extensions;
using uTorrent.WebUI.Library.Objects;
using System.IO;

namespace uTorrent.WebUI.Shell.Data
{
    //TODO: Implement the Response pattern
    public class TorrentRepositoryXml : ITorrentRepository
    {
        private readonly string _filepath = string.Empty;

        private static class TorrentRepoElements
        {
            public const string DOWNLOAD_MANAGER = "download_manager";
            public const string TORRENTS = "torrents";
            public const string TORRENT = "torrent";
            public const string NAME = "name";
            public const string HASH = "hash";
            public const string TIME_COMPLETED = "time_completed";
        }

        public TorrentRepositoryXml(string filepath)
        {
            this._filepath = filepath;

            this.VerifyRepo();
        }

        private void VerifyRepo()
        {
            if (!File.Exists(this._filepath))
            {
                var root = new XElement(TorrentRepoElements.DOWNLOAD_MANAGER);
                root.Add(new XElement(TorrentRepoElements.TORRENTS));

                var repo = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
                repo.Save(this._filepath);
            }
        }

        #region Implementation of ITorrentRepository

        public IEnumerable<Torrent> GetAll()
        {
            var repo = XDocument.Load(this._filepath);

            //TODO: Implement try/catch block
            var torrents = from nodes in repo.Descendants(TorrentRepoElements.TORRENTS)
                           let tnodes = nodes.Descendants(TorrentRepoElements.TORRENT)
                           from tnode in tnodes
                           select new Torrent(tnode.Element(TorrentRepoElements.HASH).Value.ToString())
                                      {
                                          Name = tnode.Element(TorrentRepoElements.NAME).Value.ToString(),
                                          TimeCompleted = tnode.Element(TorrentRepoElements.TIME_COMPLETED).Value.ToString().ToNullableDT()
                                      };
            
            return torrents;
        }

        public bool Add(Torrent torrent)
        {
            return this.Add(new Torrent[] { torrent });
        }

        public bool Add(IEnumerable<Torrent> torrents)
        {
            var repo = XDocument.Load(this._filepath);

            var rootElement = repo.Element(TorrentRepoElements.DOWNLOAD_MANAGER);
            if (rootElement != null)
            {
                var torrentsElement = rootElement.Element(TorrentRepoElements.TORRENTS);
                if (torrentsElement != null)
                {
                    foreach (var torrent in torrents)
                    {
                        var dateElement = new XElement(TorrentRepoElements.TIME_COMPLETED);
                        if (torrent.TimeCompleted.HasValue)
                            dateElement.Value = torrent.TimeCompleted.ToString();
                        
                        var element = new XElement(TorrentRepoElements.TORRENT,
                                                   new XElement(TorrentRepoElements.NAME, torrent.Name),
                                                   new XElement(TorrentRepoElements.HASH, torrent.Hash),
                                                   dateElement);

                        torrentsElement.Add(element);
                    }

                    repo.Save(this._filepath);
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}