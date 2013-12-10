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
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GoodlyFere.Parse.Linq.Generation.ExpressionVisitors;
using GoodlyFere.Parse.Linq.Generation.Maps;

#endregion

namespace GoodlyFere.Parse.Linq.Generation.Handlers
{
    public static class BinaryExpressionHandlers
    {
        #region Constants and Fields

        private static readonly BinaryOperatorMap OperatorMap;

        #endregion

        #region Constructors and Destructors

        static BinaryExpressionHandlers()
        {
            OperatorMap = new BinaryOperatorMap();
        }

        #endregion

        #region Methods

        internal static void Equals(List<ParseQueryProperty> queryProperties, BinaryExpression binExpr)
        {
            object value = ConstantValueFinder.Find(binExpr);
            string propertyName = MemberNameFinder.Find(binExpr);

            var constraint = new ParseQueryConstraint(ExpressionType.Equal, value);
            AddContraintQueryProperty(queryProperties, propertyName, constraint);
        }

        internal static void LogicalAnd(List<ParseQueryProperty> queryProperties, BinaryExpression binExpr)
        {
            List<ParseQueryProperty> leftProperties = RootExpressionVisitor.Translate(binExpr.Left);
            List<ParseQueryProperty> rightProperties = RootExpressionVisitor.Translate(binExpr.Right);

            IEnumerable<ParseQueryProperty> combined = CombineQueryProperties(leftProperties, rightProperties);
            IEnumerable<ParseQueryProperty> newProperties = CombineQueryProperties(queryProperties, combined);

            queryProperties.Clear();
            queryProperties.AddRange(newProperties);
        }

        internal static void Other(
            List<ParseQueryProperty> queryProperties, BinaryExpression binExpr, ExpressionType exprType)
        {
            object value = ConstantValueFinder.Find(binExpr);
            string propertyName = MemberNameFinder.Find(binExpr);

            var constraint = new ParseQueryConstraint(exprType, value);
            AddContraintQueryProperty(queryProperties, propertyName, constraint);
        }

        private static void AddContraintQueryProperty(
            List<ParseQueryProperty> queryProperties, string propertyName, ParseQueryConstraint constraint)
        {
            var queryProperty = queryProperties.SingleOrDefault(qp => qp.PropertyName == propertyName);
            if (queryProperty != null)
            {
                queryProperty.Constraints.Push(constraint);
            }
            else
            {
                queryProperty = new ParseQueryProperty(propertyName);
                queryProperty.Constraints.Push(constraint);
                queryProperties.Add(queryProperty);
            }
        }

        private static IEnumerable<ParseQueryProperty> CombineQueryProperties(
            IEnumerable<ParseQueryProperty> leftProperties, IEnumerable<ParseQueryProperty> rightProperties)
        {
            return leftProperties
                .Union(rightProperties)
                .GroupBy(p => p.PropertyName)
                .Select(
                    x =>
                    new ParseQueryProperty(x.Key)
                        {
                            Constraints = new Stack<ParseQueryConstraint>(x.SelectMany(y => y.Constraints))
                        });
        }

        #endregion
    }
}