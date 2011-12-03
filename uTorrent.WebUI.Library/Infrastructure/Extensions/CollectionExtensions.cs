using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace uTorrent.WebUI.Library.Infrastructure.Extensions
{
    public static class CollectionExtensions
    {
        public static ObservableCollection<T> ToObservable<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
                return new ObservableCollection<T>();
            return new ObservableCollection<T>(collection);
        }
    }
}