#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParseQueryConstraint.cs">
// LINQ-to-Parse, a LINQ interface to the Parse.com REST API.
//  
// Copyright (C) 2013 Benjamin Ramey
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
// 
// http://www.gnu.org/licenses/lgpl-2.1-standalone.html
// 
// You can contact me at ben.ramey@gmail.com.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion

#region Usings

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace GoodlyFere.Parse.Linq.Generation
{
    internal class ParseQueryConstraint : IDictionary<string, object>
    {
        #region Constants and Fields

        private readonly Dictionary<string, object> _dictionary;

        #endregion

        #region Constructors and Destructors

        public ParseQueryConstraint()
        {
            _dictionary = new Dictionary<string, object>();
        }

        #endregion

        #region Public Properties

        public int Count
        {
            get
            {
                return _dictionary.Count;
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
                return _dictionary.Keys;
            }
        }

        public ICollection<object> Values
        {
            get
            {
                return _dictionary.Values;
            }
        }

        #endregion

        #region Public Indexers

        public object this[string key]
        {
            get
            {
                return _dictionary[key];
            }
            set
            {
                _dictionary[key] = value;
            }
        }

        #endregion

        #region Public Methods

        public void Add(KeyValuePair<string, object> item)
        {
            (_dictionary as IDictionary<string, object>).Add(item);
        }

        public void Add(string key, object value)
        {
            if (_dictionary.ContainsKey(key))
            {
                if (_dictionary[key] is ParseQueryConstraint)
                {
                    var pqc = (_dictionary[key] as ParseQueryConstraint);
                    pqc.Add(key, value);
                }
                else
                {
                    var pqc = new ParseQueryConstraint();
                    //pqc.Add();
                }
            }
            _dictionary.Add(key, value);
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return _dictionary.Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return _dictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            (_dictionary as IDictionary<string, object>).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            return (_dictionary as IDictionary<string, object>).Remove(item);
        }

        public bool Remove(string key)
        {
            return _dictionary.Remove(key);
        }

        public bool TryGetValue(string key, out object value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        #endregion

        #region Explicit Interface Methods

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}