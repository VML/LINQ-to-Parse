#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GoodlyFere.Parse.Linq.Generation.Maps;

#endregion

namespace GoodlyFere.Parse.Linq.Generation.ExpressionVisitors
{
    internal class BinaryExpressionVisitor : BaseThrowingExpressionTreeVisitor
    {
        #region Constants and Fields

        private static readonly BinaryExpressionMap BinaryExpressionMap;

        #endregion

        #region Constructors and Destructors

        static BinaryExpressionVisitor()
        {
            BinaryExpressionMap = new BinaryExpressionMap();
        }

        protected BinaryExpressionVisitor(List<ParseQueryProperty> queryProperties)
        {
            QueryProperties = queryProperties;
        }

        #endregion

        #region Properties

        protected List<ParseQueryProperty> QueryProperties { get; set; }

        #endregion

        #region Methods

        internal static void Parse(BinaryExpression binExpr, List<ParseQueryProperty> queryProperties)
        {
            var visitor = new BinaryExpressionVisitor(queryProperties);
            visitor.VisitExpression(binExpr);
        }

        protected override Expression VisitBinaryExpression(BinaryExpression expression)
        {
            if (BinaryExpressionMap.ContainsKey(expression.NodeType))
            {
                BinaryExpressionMap[expression.NodeType](QueryProperties, expression);
                return expression;
            }

            return base.VisitBinaryExpression(expression);
        }

        #endregion
    }
}