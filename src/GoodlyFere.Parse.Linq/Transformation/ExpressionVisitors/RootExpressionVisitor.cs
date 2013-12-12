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
using System.Reflection;
using GoodlyFere.Parse.Linq.CustomExpressions;
using Remotion.Linq.Parsing;

#endregion

namespace GoodlyFere.Parse.Linq.Transformation.ExpressionVisitors
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
                return new ParsePointerExpression(transformedExpr.Type, transformedExpr.NodeType, _propertyName, transformedExpr);
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
                _propertyName = parentMemExpr.Member.Name;
            }

            return base.VisitMemberExpression(expression);
        }

        #endregion
    }
}