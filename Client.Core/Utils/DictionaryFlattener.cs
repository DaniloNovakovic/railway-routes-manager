using System.Collections.Generic;

namespace Client.Core
{
    public static class DictionaryFlattener
    {
        public static ICollection<string> Flatten<TCollection>(IDictionary<string, TCollection> dictionary)
            where TCollection : IEnumerable<string>
        {
            var flattenedItems = new List<string>();
            foreach (string key in dictionary.Keys)
            {
                foreach (string item in dictionary[key])
                {
                    flattenedItems.Add(key + ": " + item);
                }
            }
            return flattenedItems;
        }
    }
}