#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RootExpressionVisitor.cs">
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

using System.Linq;
using System;
using System.Linq.Expressions;
using GoodlyFere.Parse.Linq.CustomExpressions;
using GoodlyFere.Parse.Linq.Translation.Maps;
using GoodlyFere.Parse.Linq.Translation.ParseQuery;
using Remotion.Linq.Clauses.Expressions;

#endregion

namespace GoodlyFere.Parse.Linq.Translation.ExpressionVisitors
{
    internal class RootExpressionVisitor : BaseThrowingExpressionTreeVisitor
    {
        #region Constructors and Destructors

        public RootExpressionVisitor()
        {
            Query = new QueryRoot();
        }

        #endregion

        #region Properties

        protected QueryRoot Query { get; private set; }

        #endregion

        #region Public Methods

        public static QueryRoot Translate(Expression predicate)
        {
            var visitor = new RootExpressionVisitor();
            visitor.VisitExpression(predicate);
            return visitor.Query;
        }

        #endregion

        #region Methods

        protected override Expression VisitExtensionExpression(ExtensionExpression expression)
        {
            if (expression is ParsePointerExpression)
            {
                ParsePointerExpression ppe = (ParsePointerExpression)expression;
                ConstraintSet set = new ConstraintSet(ppe.PropertyName);
                QueryRoot root = Translate(ppe.Operand);
                foreach (var c in root)
                {
                    set.Operators.Add(c);
                }

                Query.AddConstraint(set);

                return expression;
            }

            return base.VisitExtensionExpression(expression);
        }

        protected override Expression VisitBinaryExpression(BinaryExpression expression)
        {
            if (BinaryExpressionMap.Has(expression.NodeType))
            {
                BinaryExpressionMap.Get(expression.NodeType)(Query, expression);
                return expression;
            }

            return base.VisitBinaryExpression(expression);
        }

        protected override Expression VisitMethodCallExpression(MethodCallExpression expression)
        {
            Type declaringType = expression.Method.DeclaringType;

            if (MethodCallExpressionMap.Has(declaringType))
            {
                MethodCallExpressionMap.Get(declaringType)(Query, expression);
                return expression;
            }

            return base.VisitMethodCallExpression(expression);
        }

        protected override Expression VisitSubQueryExpression(SubQueryExpression expression)
        {
            QueryRoot subQueryRoot = SubQueryTranslationVisitor.Translate(expression.QueryModel);
            Query.AddConstraintRange(subQueryRoot);
            return expression;
        }

        #endregion
    }
}