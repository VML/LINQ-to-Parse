// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Int64ResultHandlers.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/19/2013 12:14 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using VML.Parse.Interfaces;

#endregion

namespace VML.Parse.Linq.Execution.Handlers
{
    internal class Int64ResultHandlers
    {
        #region Methods

        internal static Int64 HandleLongCount(string queryString, IParseApi api, Type objectType)
        {
            return api.Count(queryString, objectType);
        }

        #endregion
    }
}