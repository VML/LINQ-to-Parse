// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="RootExpressionVisitor.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/12/2013 1:09 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System.Linq;
using System;
using System.Linq.Expressions;
using System.Reflection;
using Remotion.Linq.Parsing;
using VML.Parse.Linq.CustomExpressions;
using VML.Parse.Model;
using VML.Parse.Util;

#endregion

namespace VML.Parse.Linq.Transformation.ExpressionVisitors
{
    internal class RootExpressionVisitor : ExpressionTreeVisitor
    {
        #region Constants and Fields

        private BinaryExpression _additionalBinaries;
        private string _propertyName;

        #endregion

        #region Public Methods

        public static Expression Transform(Expression predicate)
        {
            var visitor = new RootExpressionVisitor();
            return visitor.VisitExpression(predicate);
        }

        #endregion

        #region Methods

        protected override Expression VisitBinaryExpression(BinaryExpression expression)
        {
            var expr = base.VisitBinaryExpression(expression);

            if (_additionalBinaries != null)
            {
                Expression transformedExpr = Expression.AndAlso(_additionalBinaries, expr);
                return new ParsePointerExpression(
                    transformedExpr.Type, transformedExpr.NodeType, _propertyName, transformedExpr);
            }

            return expr;
        }

        protected override Expression VisitMemberExpression(MemberExpression expression)
        {
            Expression parentExpr = expression.Expression;
            if (parentExpr != null
                && parentExpr.NodeType == ExpressionType.MemberAccess
                && parentExpr.Type.IsGenericType
                && parentExpr.Type.GetGenericTypeDefinition() == typeof(ParsePointer<>))
            {
                MemberExpression parentMemExpr = (MemberExpression)parentExpr;
                MemberInfo classNameMemberInfo = parentExpr.Type.GetMember("ClassName").FirstOrDefault();
                MemberInfo parseTypeMemberInfo = parentExpr.Type.GetMember("ParseType").FirstOrDefault();

                object dummyObject = Activator.CreateInstance(parentExpr.Type);
                string className = (string)parentExpr.Type.GetProperty("ClassName").GetValue(dummyObject);
                string parseType = (string)parentExpr.Type.GetProperty("ParseType").GetValue(dummyObject);

                Expression classNameBinary = Expression.MakeBinary(
                    ExpressionType.Equal,
                    Expression.MakeMemberAccess(parentExpr, classNameMemberInfo),
                    Expression.Constant(className));
                Expression parseTypeBinary = Expression.MakeBinary(
                    ExpressionType.Equal,
                    Expression.MakeMemberAccess(parentExpr, parseTypeMemberInfo),
                    Expression.Constant(parseType));

                _additionalBinaries = Expression.AndAlso(parseTypeBinary, classNameBinary);
                _propertyName = ClassUtils.GetDataMemberPropertyName(parentMemExpr.Member);
            }

            return base.VisitMemberExpression(expression);
        }

        #endregion
    }
}