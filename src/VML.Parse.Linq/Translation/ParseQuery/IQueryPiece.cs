// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IQueryPiece.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/11/2013 8:42 AM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;

#endregion

namespace VML.Parse.Linq.Translation.ParseQuery
{
    internal interface IQueryPiece
    {
        #region Public Properties

        string Key { get; }
        object Value { get; }

        #endregion
    }
}