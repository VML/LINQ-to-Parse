// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="SubQueryResultOperatorMap.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/11/2013 2:56 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Collections;
using System.Linq;
using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.ResultOperators;
using VML.Parse.Linq.Translation.Handlers;
using VML.Parse.Linq.Translation.ParseQuery;

#endregion

namespace VML.Parse.Linq.Translation.Maps
{
    internal delegate ConstraintSet SubQueryResultOperatorFactoryMethod(
        ResultOperatorBase resultOperator, IEnumerable values);

    internal class SubQueryResultOperatorMap : Map<SubQueryResultOperatorMap, Type, SubQueryResultOperatorFactoryMethod>
    {
        #region Constructors and Destructors

        public SubQueryResultOperatorMap()
        {
            Add(typeof(ContainsResultOperator), SubQueryResultOperatorHandlers.HandleIEnumerableMethods);
        }

        #endregion
    }
}