#region Usings

using System;
using System.Linq;
using System.Linq.Expressions;
using Remotion.Linq.Parsing;

#endregion

namespace GoodlyFere.Parse.Linq.Generation.ExpressionVisitors
{
    internal class MemberNameFinder : ExpressionTreeVisitor
    {
        #region Constructors and Destructors

        protected MemberNameFinder()
        {
        }

        #endregion

        #region Properties

        protected string MemberName { get; private set; }

        #endregion

        #region Methods

        internal static string Find(BinaryExpression expression)
        {
            var visitor = new MemberNameFinder();
            visitor.VisitExpression(expression);

            return visitor.MemberName;
        }

        protected override Expression VisitMemberExpression(MemberExpression expression)
        {
            MemberName = expression.Member.Name;
            return expression;
        }

        #endregion
    }
}