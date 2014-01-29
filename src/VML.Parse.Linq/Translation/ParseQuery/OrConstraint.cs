// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="OrConstraint.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/11/2013 8:48 AM</created>
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
    [JsonConverter(typeof(QueryPieceJsonConverter))]
    internal class OrConstraint : IQueryPiece
    {
        #region Constants and Fields

        private IList<IQueryPiece> _operands;

        #endregion

        #region Constructors and Destructors

        public OrConstraint()
        {
            Key = "$or";
            Operands = new List<IQueryPiece>();
        }

        #endregion

        #region Public Properties

        public string Key { get; private set; }

        public IList<IQueryPiece> Operands
        {
            get
            {
                return _operands;
            }
            private set
            {
                _operands = value;
                Value = value;
            }
        }

        public object Value { get; private set; }

        #endregion

        #region Public Methods

        public void Merge(OrConstraint anotherOr)
        {
            foreach (var operand in anotherOr.Operands)
            {
                Operands.Add(operand);
            }
        }

        #endregion
    }
}