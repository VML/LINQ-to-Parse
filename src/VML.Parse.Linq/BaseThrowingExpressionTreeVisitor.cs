// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="BaseThrowingExpressionTreeVisitor.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/12/2013 12:07 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using System.Linq.Expressions;
using Remotion.Linq.Clauses.ExpressionTreeVisitors;
using Remotion.Linq.Parsing;

#endregion

namespace VML.Parse.Linq
{
    internal abstract class BaseThrowingExpressionTreeVisitor : ThrowingExpressionTreeVisitor
    {
        #region Methods

        protected override Exception CreateUnhandledItemException<T>(T unhandledItem, string visitMethod)
        {
            var itemAsExpression = unhandledItem as Expression;
            string formatted = itemAsExpression == null
                                   ? unhandledItem.ToString()
                                   : FormattingExpressionTreeVisitor.Format(itemAsExpression);

            this.Log().Error(string.Format("Unhandled expression: {0}", formatted));
            return new Exception("I can't handle it! Expression type: " + typeof(T).Name);
        }

        #endregion
    }
}