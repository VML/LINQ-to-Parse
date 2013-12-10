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
using GoodlyFere.Parse.Linq.Generation.Contraints;
using GoodlyFere.Parse.Linq.Generation.ExpressionVisitors;

#endregion

namespace GoodlyFere.Parse.Linq.Generation.Handlers
{
    public static class BinaryExpressionHandlers
    {
        #region Methods

        internal static void Equals(List<ConstraintSet> queryProperties, BinaryExpression binExpr)
        {
            object value = ConstantValueFinder.Find(binExpr);
            string propertyName = MemberNameFinder.Find(binExpr);

            var constraint = new Constraint(ExpressionType.Equal, value);
            AddContraintQueryProperty(queryProperties, propertyName, constraint);
        }

        internal static void LogicalAnd(List<ConstraintSet> queryProperties, BinaryExpression binExpr)
        {
            List<ConstraintSet> leftProperties = RootExpressionVisitor.Translate(binExpr.Left);
            List<ConstraintSet> rightProperties = RootExpressionVisitor.Translate(binExpr.Right);

            IEnumerable<ConstraintSet> combined = CombineQueryProperties(leftProperties, rightProperties);
            IEnumerable<ConstraintSet> newProperties = CombineQueryProperties(queryProperties, combined);

            queryProperties.Clear();
            queryProperties.AddRange(newProperties);
        }

        internal static void LogicalOr(List<ConstraintSet> queryProperties, BinaryExpression binExpr)
        {
            List<ConstraintSet> leftProperties = RootExpressionVisitor.Translate(binExpr.Left);
            List<ConstraintSet> rightProperties = RootExpressionVisitor.Translate(binExpr.Right);

            IEnumerable<ConstraintSet> combined = CombineQueryProperties(leftProperties, rightProperties);
            IEnumerable<ConstraintSet> newProperties = CombineQueryProperties(queryProperties, combined);

            queryProperties.Clear();
            queryProperties.AddRange(newProperties);
        }

        internal static void Other(
            List<ConstraintSet> queryProperties, BinaryExpression binExpr, ExpressionType exprType)
        {
            object value = ConstantValueFinder.Find(binExpr);
            string propertyName = MemberNameFinder.Find(binExpr);

            var constraint = new Constraint(exprType, value);
            AddContraintQueryProperty(queryProperties, propertyName, constraint);
        }

        private static void AddContraintQueryProperty(
            List<ConstraintSet> queryProperties, string propertyName, Constraint constraint)
        {
            var queryProperty = queryProperties.SingleOrDefault(qp => qp.PropertyName == propertyName);
            if (queryProperty != null)
            {
                queryProperty.Constraints.Enqueue(constraint);
            }
            else
            {
                queryProperty = new ConstraintSet(propertyName);
                queryProperty.Constraints.Enqueue(constraint);
                queryProperties.Add(queryProperty);
            }
        }

        private static IEnumerable<ConstraintSet> CombineQueryProperties(
            IEnumerable<ConstraintSet> leftProperties, IEnumerable<ConstraintSet> rightProperties)
        {
            return leftProperties
                .Union(rightProperties)
                .GroupBy(p => p.PropertyName)
                .Select(
                    x =>
                    new ConstraintSet(x.Key)
                        {
                            Constraints = new Queue<Constraint>(x.SelectMany(y => y.Constraints))
                        });
        }

        #endregion
    }
}