#region Usings

using System;
using System.Linq;
using System.Linq.Expressions;
using Remotion.Linq.Parsing;

#endregion

namespace GoodlyFere.Parse.Linq.Generation.ExpressionVisitors
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

        internal static object Find(BinaryExpression expression)
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