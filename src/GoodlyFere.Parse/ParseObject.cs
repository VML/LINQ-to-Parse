#region Usings

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

#endregion

namespace GoodlyFere.Parse
{
    [DataContract]
    public class ParseObject : IDictionary<string, object>
    {
        #region Constants and Fields

        private readonly Dictionary<string, object> _data;

        #endregion

        #region Constructors and Destructors

        public ParseObject()
        {
            _data = new Dictionary<string, object>();
        }

        #endregion

        #region Public Properties

        public int Count
        {
            get
            {
                return _data.Count;
            }
        }

        [DataMember(Name = "createdAt")]
        public DateTime CreatedAt
        {
            get
            {
                return GetProperty<DateTime>("createdAt");
            }
            set
            {
                SetProperty("createdAt", value);
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public ICollection<string> Keys
        {
            get
            {
                return _data.Keys;
            }
        }

        [DataMember(Name = "objectId")]
        public string ObjectId
        {
            get
            {
                return GetProperty<string>("objectId");
            }
            set
            {
                SetProperty("objectId", value);
            }
        }

        [DataMember(Name = "updatedAt")]
        public DateTime UpdatedAt
        {
            get
            {
                return GetProperty<DateTime>("updatedAt");
            }
            set
            {
                SetProperty("updatedAt", value);
            }
        }

        public ICollection<object> Values
        {
            get
            {
                return _data.Values;
            }
        }

        #endregion

        #region Public Indexers

        public object this[string key]
        {
            get
            {
                return _data[key];
            }
            set
            {
                if (ContainsKey(key))
                {
                    _data[key] = value;
                }
                else
                {
                    Add(key, value);
                }
            }
        }

        #endregion

        #region Public Methods

        public void Add(KeyValuePair<string, object> item)
        {
            (_data as IDictionary<string, object>).Add(item);
        }

        public void Add(string key, object value)
        {
            _data.Add(key, value);
        }

        public void Clear()
        {
            _data.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return _data.Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return _data.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            (_data as IDictionary<string, object>).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            return (_data as IDictionary<string, object>).Remove(item);
        }

        public bool Remove(string key)
        {
            return _data.Remove(key);
        }

        public bool TryGetValue(string key, out object value)
        {
            return _data.TryGetValue(key, out value);
        }

        #endregion

        #region Explicit Interface Methods

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Methods

        protected T GetProperty<T>(string name)
        {
            if (ContainsKey(name))
            {
                return (T)this[name];
            }

            return default(T);
        }

        protected void SetProperty(string name, object value)
        {
            if (ContainsKey(name))
            {
                this[name] = value;
            }
            else
            {
                Add(name, value);
            }
        }

        #endregion
    }
}