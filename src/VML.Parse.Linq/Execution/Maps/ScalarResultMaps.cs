// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ScalarResultMaps.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/19/2013 12:13 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Collections;
using System.Linq;

#endregion

namespace VML.Parse.Linq.Execution.Maps
{
    internal delegate T ScalarResultHandlerMethod<T>(string queryString, ParseApi api, Type objectType);

    internal class ScalarResultMaps : Map<ScalarResultMaps, Type, IDictionary>
    {
        #region Constructors and Destructors

        public ScalarResultMaps()
        {
            Add(typeof(Int32), new Int32ResultMap());
            Add(typeof(Int64), new Int64ResultMap());
            Add(typeof(bool), new BoolResultMap());
        }

        #endregion
    }
}