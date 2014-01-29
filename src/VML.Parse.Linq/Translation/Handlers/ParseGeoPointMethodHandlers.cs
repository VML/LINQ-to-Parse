// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ParseGeoPointMethodHandlers.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>01/09/2014 5:57 PM</created>
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
    internal static class ParseGeoPointMethodHandlers
    {
        #region Methods

        internal static IList<IQueryPiece> HandleNearSphere(QueryRoot query, MethodCallExpression expression)
        {
            object latitude = ConstantValueFinder.Find(expression.Arguments[0]);
            object longitude = ConstantValueFinder.Find(expression.Arguments[1]);

            SubConstraintSet set = new SubConstraintSet("$nearSphere");
            set.Operators.Add(new BasicQueryPiece("__type", "GeoPoint"));
            set.Operators.Add(new BasicQueryPiece("latitude", latitude));
            set.Operators.Add(new BasicQueryPiece("longitude", longitude));

            return new List<IQueryPiece> { set };
        }

        #endregion
    }
}