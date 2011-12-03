using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uTorrent.WebUI.Library.Infrastructure.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime? ToNullableDT(this string datetime)
        {
            DateTime outdate;
            if (DateTime.TryParse(datetime, out outdate))
                return outdate;
            return null;
        }
    }
}
