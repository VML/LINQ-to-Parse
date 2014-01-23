// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="BinaryOperatorMap.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/02/2013 4:18 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using System.Linq.Expressions;

#endregion

namespace VML.Parse.Linq.Translation.Maps
{
    internal class BinaryOperatorMap : Map<BinaryOperatorMap, ExpressionType, string>
    {
        #region Constructors and Destructors

        public BinaryOperatorMap()
        {
            Add(ExpressionType.NotEqual, "$ne");
            Add(ExpressionType.GreaterThan, "$gt");
            Add(ExpressionType.GreaterThanOrEqual, "$gte");
            Add(ExpressionType.LessThan, "$lt");
            Add(ExpressionType.LessThanOrEqual, "$lte");
        }

        #endregion
    }
}