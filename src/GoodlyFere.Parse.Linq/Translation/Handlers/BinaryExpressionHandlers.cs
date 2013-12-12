#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BinaryExpressionHandlers.cs">
// LINQ-to-Parse, a LINQ interface to the Parse.com REST API.
//  
// Copyright (C) 2013 Benjamin Ramey
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
// 
// http://www.gnu.org/licenses/lgpl-2.1-standalone.html
// 
// You can contact me at ben.ramey@gmail.com.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion

#region Usings

using System;
using System.Linq;
using System.Linq.Expressions;
using GoodlyFere.Parse.Linq.Translation.ExpressionVisitors;
using GoodlyFere.Parse.Linq.Translation.Maps;
using GoodlyFere.Parse.Linq.Translation.ParseQuery;

#endregion

namespace GoodlyFere.Parse.Linq.Translation.Handlers
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