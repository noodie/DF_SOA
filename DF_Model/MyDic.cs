using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DF_DAL
{
    public class MyDic<K, V> : IDictionary<K, V>
    {
        Dictionary<string, V> dic = new Dictionary<string, V>();

        public V this[K key]
        {
            get
            {
                return dic[key.ToString().ToLower()];
            }

            set
            {
                dic[key.ToString().ToLower()] = value;
            }
        }

        public int Count
        {
            get
            {
                return this.dic.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false; 
            }
        }

        public ICollection<K> Keys
        {
            get
            {
                // ICollection<K> s = null;
                //  foreach (KeyValuePair<string, V> d in this.dic)
                //   s.Add((K)d.Key);
                // return s;
                return ((IDictionary<K, V>)this.dic).Keys;
              //  return (ICollection<K>)dic.Keys.ToArray().Cast<K>();

               // Dictionary<string, V>.KeyCollection keyCol = this.dic.Keys;
                //return (ICollection<K>)keyCol; 
            }
        }

        public ICollection<V> Values
        {
            get
            {
                return ((IDictionary<K, V>)this.dic).Values;
               // return (ICollection<V>)dic.Values.ToArray().Cast<V>();
            }
        }

        public void Add(KeyValuePair<K, V> item)
        {
            this.dic.Add(item.Key.ToString().ToLower(), item.Value);
        }

        public void Add(K key, V value)
        {
            this.dic.Add(key.ToString().ToLower(), value);
        }

        public void Clear()
        {
            this.dic.Clear();
        }

        public bool Contains(KeyValuePair<K, V> item)
        {
               return ((IDictionary<K,V>)this.dic).Contains(item);
        }

        public bool ContainsKey(K key)
        {
            return this.dic.ContainsKey(key.ToString().ToLower());
        }

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            return ((IDictionary<K, V>)this.dic).GetEnumerator();
          //  throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<K, V> item)
        {
            return ((IDictionary<K, V>)this.dic).Remove(item);
        }

        public bool Remove(K key)
        {
            return ((IDictionary<K, V>)this.dic).Remove(key);
        }
        public bool TryGetValue(K key, out V value)
        {
            return this.dic.TryGetValue(key.ToString().ToLower(), out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}