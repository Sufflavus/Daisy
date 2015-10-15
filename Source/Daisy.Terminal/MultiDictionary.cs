using System;
using System.Collections.Generic;


namespace Daisy.Terminal
{
    public class MultiDictionary<T, K> : Dictionary<T, List<K>>
    {
        public void AddValue(T key, K newItem)
        {
            InitKey(key);
            this[key].Add(newItem);
        }


        public void AddValues(T key, IEnumerable<K> newItems)
        {
            InitKey(key);
            this[key].AddRange(newItems);
        }


        public bool RemoveValue(T key, K value)
        {
            if (!ContainsKey(key))
            {
                return false;
            }

            this[key].Remove(value);

            if (this[key].Count == 0)
            {
                Remove(key);
            }

            return true;
        }


        public bool RemoveValues(T key, Predicate<K> match)
        {
            if (!ContainsKey(key))
            {
                return false;
            }

            this[key].RemoveAll(match);

            if (this[key].Count == 0)
            {
                Remove(key);
            }

            return true;
        }


        private void InitKey(T key)
        {
            if (!ContainsKey(key))
            {
                this[key] = new List<K>();
            }
        }
    }
}
