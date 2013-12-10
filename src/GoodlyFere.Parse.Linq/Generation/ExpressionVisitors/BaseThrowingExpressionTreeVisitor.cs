#region Usings

using System;
using System.Linq;
using System.Linq.Expressions;
using Remotion.Linq.Clauses.ExpressionTreeVisitors;
using Remotion.Linq.Parsing;

#endregion

namespace GoodlyFere.Parse.Linq.Generation.ExpressionVisitors
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