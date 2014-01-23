// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ResultOperatorMap.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/11/2013 2:56 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.ResultOperators;
using VML.Parse.Linq.Translation.Handlers;

#endregion

namespace VML.Parse.Linq.Translation.Maps
{
    internal delegate void ResultOperatorFactoryMethod(
        ResultOperatorBase resultOperator, Dictionary<string, string> parameters);

    internal class ResultOperatorMap : Map<ResultOperatorMap, Type, ResultOperatorFactoryMethod>
    {
        #region Constructors and Destructors

        public ResultOperatorMap()
        {
            Add(typeof(SkipResultOperator), ResultOperatorHandlers.HandleSkip);
            Add(typeof(TakeResultOperator), ResultOperatorHandlers.HandleTake);
            Add(typeof(FirstResultOperator), ResultOperatorHandlers.HandleFirst);
            Add(typeof(CountResultOperator), ResultOperatorHandlers.HandleCount);
        }

        #endregion
    }
}