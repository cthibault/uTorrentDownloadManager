using System;
using System.Collections.Generic;
using System.IO;
using uTorrent.WebUI.Library.Objects;

namespace uTorrent.WebUI.Shell.Infrastructure
{
    public class EventLogFileBased : IEventLog
    {
        private readonly string _filepath;
        private StreamWriter GetLogWriter
        {
            get { return File.Exists(this._filepath) ? File.AppendText(this._filepath) : File.CreateText(this._filepath); }
        }

        public EventLogFileBased(string filepath)
        {
            this._filepath = filepath;
        }

        public void WriteToLog(string message)
        {
            using (var streamWriter = this.GetLogWriter)
            {
                streamWriter.WriteLine(message);
                streamWriter.Close();
            }
        }

        public void WriteToLog(IEnumerable<string> messages)
        {
            using (var streamWriter = this.GetLogWriter)
            {
                foreach (var message in messages)
                    streamWriter.WriteLine(message);
                streamWriter.Close();
            }
        }

        public void WriteToLog<T>(Response<T> response)
        {
            using (var streamWriter = this.GetLogWriter)
            {
                response.DisplayMessages.ForEach(streamWriter.WriteLine);
                if (response.Exceptions != null)
                    response.Exceptions.ForEach(ex =>
                                                {
                                                    streamWriter.WriteLine("--Message: {0}", ex.Message);
                                                    streamWriter.WriteLine("--Stack Trace--\n{0}", ex.StackTrace ?? string.Empty);
                                                });
                streamWriter.Close();
            }
        }
    }
}