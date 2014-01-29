// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ConstantValueFinder.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/10/2013 3:17 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using System.Linq.Expressions;
using Remotion.Linq.Parsing;

#endregion

namespace VML.Parse.Linq.Translation.ExpressionVisitors
{
    internal class ConstantValueFinder : ExpressionTreeVisitor
    {
        #region Constructors and Destructors

        protected ConstantValueFinder()
        {
        }

        #endregion

        #region Properties

        protected object Value { get; private set; }

        #endregion

        #region Methods

        internal static object Find(Expression expression)
        {
            var visitor = new ConstantValueFinder();
            visitor.VisitExpression(expression);

            return visitor.Value;
        }

        protected override Expression VisitConstantExpression(ConstantExpression expression)
        {
            Value = expression.Value;
            return expression;
        }

        #endregion
    }
}