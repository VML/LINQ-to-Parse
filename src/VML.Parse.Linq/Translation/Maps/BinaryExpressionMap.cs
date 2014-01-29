// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="BinaryExpressionMap.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/02/2013 4:12 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using System.Linq.Expressions;
using VML.Parse.Linq.Translation.Handlers;
using VML.Parse.Linq.Translation.ParseQuery;

#endregion

namespace VML.Parse.Linq.Translation.Maps
{
    internal delegate void BinaryExpressionFactoryMethod(
        QueryRoot query, BinaryExpression binExpr);

    internal class BinaryExpressionMap : Map<BinaryExpressionMap, ExpressionType, BinaryExpressionFactoryMethod>
    {
        #region Constructors and Destructors

        public BinaryExpressionMap()
        {
            // logical operators
            Add(ExpressionType.AndAlso, BinaryExpressionHandlers.HandleLogicalAnd);
            // logical operators
            Add(ExpressionType.OrElse, BinaryExpressionHandlers.HandleLogicalOr);

            // comparison operators
            Add(ExpressionType.Equal, BinaryExpressionHandlers.HandleEquals);
            Add(
                ExpressionType.NotEqual,
                (qp, es) => BinaryExpressionHandlers.HandleGeneralBinary(qp, es, ExpressionType.NotEqual));
            Add(
                ExpressionType.GreaterThan,
                (qp, es) => BinaryExpressionHandlers.HandleGeneralBinary(qp, es, ExpressionType.GreaterThan));
            Add(
                ExpressionType.GreaterThanOrEqual,
                (qp, es) => BinaryExpressionHandlers.HandleGeneralBinary(qp, es, ExpressionType.GreaterThanOrEqual));
            Add(
                ExpressionType.LessThan,
                (qp, es) => BinaryExpressionHandlers.HandleGeneralBinary(qp, es, ExpressionType.LessThan));
            Add(
                ExpressionType.LessThanOrEqual,
                (qp, es) => BinaryExpressionHandlers.HandleGeneralBinary(qp, es, ExpressionType.LessThanOrEqual));
        }

        #endregion
    }
}