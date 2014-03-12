// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="TestExecutor.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/19/2013 9:42 AM</created>
//  <updated>01/23/2014 2:33 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Remotion.Linq;

#endregion

namespace VML.Parse.Tests.Support
{
    internal class TestExecutor : IQueryExecutor
    {
        #region Public Methods

        public IEnumerable<T> ExecuteCollection<T>(QueryModel queryModel)
        {
            return new List<T>();
        }

        public T ExecuteScalar<T>(QueryModel queryModel)
        {
            return default(T);
        }

        public T ExecuteSingle<T>(QueryModel queryModel, bool returnDefaultWhenEmpty)
        {
            return default(T);
        }

        #endregion
    }
}