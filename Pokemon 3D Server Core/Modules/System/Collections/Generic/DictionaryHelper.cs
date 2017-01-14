using System.Collections;
using System.Collections.Generic;

namespace Pokemon_3D_Server_Core.Modules.System.Collections.Generic
{
    public class DictionaryHelper<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private Dictionary<TKey, TValue> Dictionary;
        protected ICollection Collection;

        public TValue this[TKey key]
        {
            get
            {
                lock (Collection.SyncRoot)
                    return Dictionary[key];
            }
            set
            {
                lock (Collection.SyncRoot)
                    Dictionary[key] = value;
            }
        }

        public TKey[] Keys
        {
            get
            {
                lock (Collection.SyncRoot)
                {
                    if (Dictionary.Keys.Count > 0)
                    {
                        TKey[] keys = new TKey[Dictionary.Keys.Count];
                        Dictionary.Keys.CopyTo(keys, 0);
                        return keys;
                    }
                    else
                        return new TKey[0];
                }
            }
        }

        public TValue[] Values
        {
            get
            {
                lock (Collection.SyncRoot)
                {
                    if (Dictionary.Values.Count > 0)
                    {
                        TValue[] values = new TValue[Dictionary.Values.Count];
                        Dictionary.Values.CopyTo(values, 0);
                        return values;
                    }
                    else
                        return new TValue[0];
                }
            }
        }

        public int Count { get { return Dictionary.Count; } }

        public DictionaryHelper()
        {
            Dictionary = new Dictionary<TKey, TValue>();
            Collection = Dictionary;
        }

        public DictionaryHelper(int capacity)
        {
            Dictionary = new Dictionary<TKey, TValue>(capacity);
            Collection = Dictionary;
        }

        public DictionaryHelper(IEqualityComparer<TKey> comparer)
        {
            Dictionary = new Dictionary<TKey, TValue>(comparer);
            Collection = Dictionary;
        }

        public DictionaryHelper(IDictionary<TKey, TValue> dictionary)
        {
            Dictionary = new Dictionary<TKey, TValue>(dictionary);
            Collection = Dictionary;
        }

        public DictionaryHelper(int capacity, IEqualityComparer<TKey> comparer)
        {
            Dictionary = new Dictionary<TKey, TValue>(capacity, comparer);
            Collection = Dictionary;
        }

        public DictionaryHelper(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
        {
            Dictionary = new Dictionary<TKey, TValue>(dictionary, comparer);
            Collection = Dictionary;
        }

        public virtual void Add(TKey obj, TValue obj2)
        {
            lock (Collection.SyncRoot)
                Dictionary.Add(obj, obj2);
        }

        public virtual void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> obj)
        {
            lock (Collection.SyncRoot)
            {
                foreach (KeyValuePair<TKey, TValue> item in obj)
                    Dictionary.Add(item.Key, item.Value);
            }
        }

        public virtual void Remove(TKey obj)
        {
            lock (Collection.SyncRoot)
                Dictionary.Remove(obj);

            Core.Logger.Debug($"Connection Disposed. Active connection left: " + Core.TcpClientCollection.Count);
        }

        public virtual void RemoveAll(IEnumerable<TKey> obj)
        {
            lock (Collection.SyncRoot)
            {
                foreach (TKey item in obj)
                    Dictionary.Remove(item);
            }
        }

        public virtual void RemoveAll(IEnumerable<KeyValuePair<TKey, TValue>> obj)
        {
            lock (Collection.SyncRoot)
            {
                foreach (KeyValuePair<TKey, TValue> item in obj)
                    Dictionary.Remove(item.Key);
            }
        }

        public virtual void Clear()
        {
            lock (Collection.SyncRoot)
                Dictionary.Clear();
        }

        public virtual bool ContainsKey(TKey key)
        {
            lock (Collection.SyncRoot)
                return Dictionary.ContainsKey(key);
        }

        public virtual bool ContainsValue(TValue value)
        {
            lock (Collection.SyncRoot)
                return Dictionary.ContainsValue(value);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            lock (Collection.SyncRoot)
                return Dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
