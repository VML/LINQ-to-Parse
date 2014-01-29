// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ResultOperatorHandlers.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/11/2013 2:57 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.ResultOperators;
using VML.Parse.Linq.Translation.ExpressionVisitors;

#endregion

namespace VML.Parse.Linq.Translation.Handlers
{
    internal class ResultOperatorHandlers
    {
        #region Public Methods

        public static void HandleCount(
            ResultOperatorBase resultOperator, Dictionary<string, string> parameters)
        {
            parameters.Add("limit", "0");
            parameters.Add("count", "1");
        }

        public static void HandleFirst(
            ResultOperatorBase resultOperator, Dictionary<string, string> parameters)
        {
            parameters.Add("limit", "1");
        }

        public static void HandleSkip(
            ResultOperatorBase resultOperator, Dictionary<string, string> parameters)
        {
            var skipRO = (SkipResultOperator)resultOperator;
            parameters.Add("skip", ConstantValueFinder.Find(skipRO.Count).ToString());
        }

        public static void HandleTake(
            ResultOperatorBase resultOperator, Dictionary<string, string> parameters)
        {
            var takeRO = (TakeResultOperator)resultOperator;
            parameters.Add("limit", ConstantValueFinder.Find(takeRO.Count).ToString());
        }

        #endregion
    }
}