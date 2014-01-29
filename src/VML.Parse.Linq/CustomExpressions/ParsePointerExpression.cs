// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ParsePointerExpression.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/12/2013 1:10 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using System.Linq.Expressions;
using Remotion.Linq.Clauses.Expressions;
using Remotion.Linq.Parsing;

#endregion

namespace VML.Parse.Linq.CustomExpressions
{
    internal class ParsePointerExpression : ExtensionExpression
    {
        #region Constructors and Destructors

        public ParsePointerExpression(Type type, string propertyName, Expression operand)
            : base(type)
        {
            PropertyName = propertyName;
            Operand = operand;
        }

        public ParsePointerExpression(Type type, ExpressionType nodeType, string propertyName, Expression operand)
            : base(type, nodeType)
        {
            PropertyName = propertyName;
            Operand = operand;
        }

        #endregion

        #region Public Properties

        public Expression Operand { get; set; }
        public string PropertyName { get; set; }

        #endregion

        #region Methods

        protected override Expression VisitChildren(ExpressionTreeVisitor visitor)
        {
            return visitor.VisitExpression(Operand);
        }

        #endregion
    }
}