// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MethodCallExpressionHandlers.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/10/2013 4:59 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VML.Parse.Linq.Translation.ExpressionVisitors;
using VML.Parse.Linq.Translation.Maps;
using VML.Parse.Linq.Translation.ParseQuery;

#endregion

namespace VML.Parse.Linq.Translation.Handlers
{
    internal static class MethodCallExpressionHandlers
    {
        #region Methods

        internal static void HandleParseGeoPointMethods(QueryRoot query, MethodCallExpression expression)
        {
            Handle(query, expression, ParseGeoPointMethodHandlersMap.Instance);
        }

        internal static void HandleStringMethods(QueryRoot query, MethodCallExpression expression)
        {
            Handle(query, expression, StringMethodHandlersMap.Instance);
        }

        private static void AddConstraintToQuery(QueryRoot query, IEnumerable<IQueryPiece> operands, ConstraintSet set)
        {
            foreach (var op in operands)
            {
                set.Operators.Add(op);
            }
            query.AddConstraint(set);
        }

        private static void Handle<T>(
            QueryRoot query, MethodCallExpression expression, T map)
            where T : Map<T, string, MethodCallHandlerFactoryMethod>, new()
        {
            string propertyName = MemberNameFinder.Find(expression.Object);
            ConstraintSet set = new ConstraintSet(propertyName);

            if (map.HasValue(expression.Method.Name))
            {
                var method = map.GetValue(expression.Method.Name);
                IList<IQueryPiece> operands = method(query, expression);
                AddConstraintToQuery(query, operands, set);
            }
            else
            {
                throw new Exception(string.Format("Method call not handled! {0}", expression.Method.Name));
            }
        }

        #endregion
    }
}