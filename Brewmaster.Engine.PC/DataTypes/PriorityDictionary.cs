using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System.Collections.Generic
{
    public class PriorityDictionary<TKey, TValue, TSortType> : IEnumerable<TValue> where TSortType : IComparable<TSortType>
    {
        private readonly Dictionary<TKey, TValue> dictionary;
        private readonly List<KeyValuePair<TSortType, TKey>> sortedList;
        private readonly Func<TValue, TSortType> getPriority;

        /// <summary>
        /// Creates an indexed and always sorted collection of data.
        /// </summary>
        /// <param name="GetPriority">Lambda expression to get the property to sort by.</param>
        public PriorityDictionary(Func<TValue, TSortType> GetPriority)
        {
            dictionary = new Dictionary<TKey, TValue>();
            sortedList = new List<KeyValuePair<TSortType, TKey>>();
            getPriority = GetPriority;
        }

        public TValue this[TKey key]
        {
            get { return dictionary[key]; }
            set { Add(key, value); }
        }

        public void Add(TKey key, TValue value)
        {
            sortedList.Add(new KeyValuePair<TSortType, TKey>(getPriority(value), key));
            sortedList.Sort((a, b) => a.Key.CompareTo(b.Key));
            dictionary.Add(key, value);
        }

        public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            sortedList.AddRange(items.Select(i => new KeyValuePair<TSortType, TKey>(getPriority(i.Value), i.Key)));
            sortedList.Sort();

            foreach (var item in items)
                dictionary.Add(item.Key, item.Value);
        }

        public void Remove(TKey key)
        {
            sortedList.Remove(new KeyValuePair<TSortType, TKey>(getPriority(dictionary[key]), key));
            dictionary.Remove(key);
        }

        public void Clear()
        {
            sortedList.Clear();
            dictionary.Clear();
        }

        public bool ContainsKey(TKey key)
        {
            return dictionary.ContainsKey(key);
        }

        public void ForEach(Func<TValue, bool> predicate, Action<TValue> action)
        {
            var keys = sortedList.Select(kvp => kvp.Value).ToList();

            if (keys.Any())
                foreach (var key in keys.Where(key => dictionary.ContainsKey(key)).Where(key => predicate == null || predicate(dictionary[key])))
                    action(dictionary[key]);
        }

        public void ForEach(Action<TValue> action)
        {
            ForEach(o => true, action);
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return sortedList.Select(key => dictionary[key.Value]).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Dictionary<TKey, TValue> ToDictionary()
        {
            return dictionary;
        }

        public TValue[] ToArray()
        {
            return dictionary.Select(d => d.Value).ToArray();
        }

        public List<TValue> ToList()
        {
            return dictionary.Select(d => d.Value).ToList();
        }
    }
}
