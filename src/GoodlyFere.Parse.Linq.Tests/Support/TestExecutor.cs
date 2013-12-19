#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Remotion.Linq;

#endregion

namespace GoodlyFere.Parse.Linq.Tests.Support
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