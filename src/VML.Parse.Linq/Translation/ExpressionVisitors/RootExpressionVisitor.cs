// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="RootExpressionVisitor.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/02/2013 4:13 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System.Linq;
using System;
using System.Linq.Expressions;
using Remotion.Linq.Clauses.Expressions;
using VML.Parse.Linq.CustomExpressions;
using VML.Parse.Linq.Translation.Maps;
using VML.Parse.Linq.Translation.ParseQuery;

#endregion

namespace VML.Parse.Linq.Translation.ExpressionVisitors
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

        protected override Expression VisitBinaryExpression(BinaryExpression expression)
        {
            if (BinaryExpressionMap.Has(expression.NodeType))
            {
                BinaryExpressionMap.Get(expression.NodeType)(Query, expression);
                return expression;
            }

            return base.VisitBinaryExpression(expression);
        }

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