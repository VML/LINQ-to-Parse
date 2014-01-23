// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Map.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/19/2013 12:17 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace VML.Parse.Linq
{
    internal class Map<TInstance, TKey, TValue> : Dictionary<TKey, TValue>
        where TValue : class
        where TInstance : Map<TInstance, TKey, TValue>, new()
    {
        #region Constants and Fields

        private static TInstance _instance;

        #endregion

        #region Public Properties

        public static TInstance Instance
        {
            get
            {
                return _instance ?? (_instance = new TInstance());
            }
        }

        #endregion

        #region Public Methods

        public static TValue Get(TKey type)
        {
            return Instance.GetValue(type);
        }

        public static bool Has(TKey type)
        {
            return Instance.HasValue(type);
        }

        public TValue GetValue(TKey type)
        {
            return HasValue(type) ? Instance[type] : null;
        }

        public bool HasValue(TKey type)
        {
            return Instance.ContainsKey(type);
        }

        #endregion
    }
}