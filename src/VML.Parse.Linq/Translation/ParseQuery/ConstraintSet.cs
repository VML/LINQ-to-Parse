// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ConstraintSet.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/10/2013 4:04 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using VML.Parse.Linq.Translation.ParseQuery.JsonConverters;

#endregion

namespace VML.Parse.Linq.Translation.ParseQuery
{
    [JsonConverter(typeof(ConstraintSetJsonConverter))]
    internal class ConstraintSet : IQueryPiece
    {
        #region Constants and Fields

        private IList<IQueryPiece> _operators;

        #endregion

        #region Constructors and Destructors

        public ConstraintSet(string key)
        {
            Key = key;
            Operators = new List<IQueryPiece>();
        }

        #endregion

        #region Public Properties

        public string Key { get; private set; }

        public IList<IQueryPiece> Operators
        {
            get
            {
                return _operators;
            }
            private set
            {
                _operators = value;
                Value = value;
            }
        }

        public object Value { get; set; }

        #endregion

        #region Public Methods

        public void Merge(ConstraintSet anotherSet)
        {
            foreach (var c in anotherSet.Operators)
            {
                Operators.Add(c);
            }
        }

        #endregion
    }
}