using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Data;
using System.Xml.Linq;
using uTorrent.WebUI.Library.Objects;

namespace uTorrent.WebUI.Communication
{
    class Program
    {
        static void Main(string[] args)
        {
            //var connectionInfo = new WebUIConnectionInfo();
            //var manager = new WebUIManager(connectionInfo);
            //var result = manager.RemoveCompletedTorrents();

            ////var torrents = manager.GetTorrentList();
            ////torrents.ToList().ForEach(t => Console.WriteLine(t.ToString()));

            Console.WriteLine(new DisplayNameConverter().Convert(
                new object[] { "First", null, "", "JR" }, null, null, null));
            Console.Read();
        }
    }

    public class DisplayNameConverter : IMultiValueConverter
    {
        #region Implementation of IMultiValueConverter

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string displayName = string.Empty;
            var names = new string[4];

            if (values != null)
            {
                for (int i = 0; i < values.Length; i++)
                    names[i] = values[i] != null ? values[i].ToString() : string.Empty;

                if (names.Count(name => !string.IsNullOrEmpty(name)) >= 2)
                    displayName = DisplayNameConverter.Convert(names[0], names[1], names[2], names[3]);
            }

            return displayName;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

        public static string Convert(string firstName, string lastName, string middleName = "", string suffix = "")
        {
            string name = string.Format("{0}, {1} {2}", lastName, firstName, middleName);
            return name;
        }
    }
}