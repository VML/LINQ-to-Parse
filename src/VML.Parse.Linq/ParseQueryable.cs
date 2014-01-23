// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ParseQueryable.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/02/2013 9:28 AM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using System.Linq.Expressions;
using Remotion.Linq;
using Remotion.Linq.Parsing.Structure;

#endregion

namespace VML.Parse.Linq
{
    public class ParseQueryable<T> : QueryableBase<T>
    {
        #region Constructors and Destructors

        public ParseQueryable(IQueryParser queryParser, IQueryExecutor executor)
            : base(queryParser, executor)
        {
        }

        public ParseQueryable(IQueryProvider provider, Expression expression)
            : base(provider, expression)
        {
        }

        #endregion
    }
}