// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="StringMethodHandlers.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/10/2013 5:09 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VML.Parse.Linq.Translation.ExpressionVisitors;
using VML.Parse.Linq.Translation.ParseQuery;

#endregion

namespace VML.Parse.Linq.Translation.Handlers
{
    internal static class StringMethodHandlers
    {
        #region Methods

        internal static IList<IQueryPiece> HandleContains(QueryRoot query, MethodCallExpression expression)
        {
            object value = ConstantValueFinder.Find(expression);
            List<IQueryPiece> pieces = new List<IQueryPiece>
                {
                    new BasicQueryPiece("$regex", value),
                    new BasicQueryPiece("$options", "mi")
                };
            return pieces;
        }

        #endregion
    }
}