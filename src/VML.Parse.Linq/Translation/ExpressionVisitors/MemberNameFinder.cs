// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MemberNameFinder.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/10/2013 3:15 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using System.Linq.Expressions;
using Remotion.Linq.Parsing;
using VML.Parse.Util;

#endregion

namespace VML.Parse.Linq.Translation.ExpressionVisitors
{
    internal class MemberNameFinder : ExpressionTreeVisitor
    {
        #region Properties

        protected string MemberName { get; private set; }

        #endregion

        #region Methods

        internal static string Find(Expression expression)
        {
            var visitor = new MemberNameFinder();
            visitor.VisitExpression(expression);

            return visitor.MemberName;
        }

        protected override Expression VisitMemberExpression(MemberExpression expression)
        {
            MemberName = ClassUtils.GetDataMemberPropertyName(expression.Member);
            return expression;
        }

        #endregion
    }
}