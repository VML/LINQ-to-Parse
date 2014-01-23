// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="SubQueryResultOperatorHandlers.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/11/2013 2:57 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Collections;
using System.Linq;
using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.ResultOperators;
using VML.Parse.Linq.Translation.ExpressionVisitors;
using VML.Parse.Linq.Translation.ParseQuery;

#endregion

namespace VML.Parse.Linq.Translation.Handlers
{
    internal class SubQueryResultOperatorHandlers
    {
        #region Public Methods

        public static ConstraintSet HandleIEnumerableMethods(
            ResultOperatorBase resultOperator, IEnumerable values)
        {
            ContainsResultOperator containsOp = (ContainsResultOperator)resultOperator;
            string propName = MemberNameFinder.Find(containsOp.Item);

            ConstraintSet inSet = new ConstraintSet(propName);
            inSet.Operators.Add(new BasicQueryPiece("$in", values));

            return inSet;
        }

        #endregion
    }
}