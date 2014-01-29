// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="BinaryExpressionHandlers.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/02/2013 4:20 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using System.Linq.Expressions;
using VML.Parse.Linq.Translation.ExpressionVisitors;
using VML.Parse.Linq.Translation.Maps;
using VML.Parse.Linq.Translation.ParseQuery;

#endregion

namespace VML.Parse.Linq.Translation.Handlers
{
    public static class BinaryExpressionHandlers
    {
        #region Methods

        internal static void HandleEquals(QueryRoot query, BinaryExpression binExpr)
        {
            object value = ConstantValueFinder.Find(binExpr);
            string propertyName = MemberNameFinder.Find(binExpr);

            var constraint = new EqualsConstraint(propertyName, value);
            query.AddConstraint(constraint);
        }

        internal static void HandleGeneralBinary(
            QueryRoot query, BinaryExpression binExpr, ExpressionType exprType)
        {
            object value = ConstantValueFinder.Find(binExpr);
            string propertyName = MemberNameFinder.Find(binExpr);

            var set = new ConstraintSet(propertyName);
            set.Operators.Add(new BasicQueryPiece(BinaryOperatorMap.Get(exprType), value));
            query.AddConstraint(set);
        }

        internal static void HandleLogicalAnd(QueryRoot query, BinaryExpression binExpr)
        {
            QueryRoot leftQuery = RootExpressionVisitor.Translate(binExpr.Left);
            QueryRoot rightQuery = RootExpressionVisitor.Translate(binExpr.Right);

            query.AddConstraintRange(leftQuery);
            query.AddConstraintRange(rightQuery);
        }

        internal static void HandleLogicalOr(QueryRoot query, BinaryExpression binExpr)
        {
            QueryRoot leftQuery = RootExpressionVisitor.Translate(binExpr.Left);
            QueryRoot rightQuery = RootExpressionVisitor.Translate(binExpr.Right);

            OrConstraint or = ((OrConstraint)query.FindForProperty("$or")) ?? new OrConstraint();
            foreach (var c in leftQuery.Union(rightQuery))
            {
                if (c is OrConstraint)
                {
                    or.Merge((OrConstraint)c);
                }
                else
                {
                    or.Operands.Add(c);
                }
            }

            query.AddConstraint(or);
        }

        #endregion
    }
}