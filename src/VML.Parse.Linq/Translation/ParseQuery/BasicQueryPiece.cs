// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="BasicQueryPiece.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/11/2013 8:49 AM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using Newtonsoft.Json;
using VML.Parse.Linq.Translation.ParseQuery.JsonConverters;

#endregion

namespace VML.Parse.Linq.Translation.ParseQuery
{
    [JsonConverter(typeof(QueryPieceJsonConverter))]
    internal class BasicQueryPiece : IQueryPiece
    {
        #region Constructors and Destructors

        public BasicQueryPiece()
        {
        }

        public BasicQueryPiece(string key, object value)
        {
            Key = key;
            Value = value;
        }

        #endregion

        #region Public Properties

        public string Key { get; set; }
        public object Value { get; set; }

        #endregion
    }
}