using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uTorrent.WebUI.Library.Objects
{
    public class Response<T>
    {
        public T Value { get; set; }
        public bool Success { get; set; }
        public List<string> DisplayMessages { get; set; }
        public List<Exception> Exceptions { get; set; }

        public Response(bool success)
        {
            this.Success = success;
            this.DisplayMessages = new List<string>();
            this.Exceptions = new List<Exception>();
        }

        public Response(bool success, T value)
        {
            this.Value = value;
            this.Success = success;
            this.DisplayMessages = new List<string>();
            this.Exceptions = new List<Exception>();
        }
    }
}
