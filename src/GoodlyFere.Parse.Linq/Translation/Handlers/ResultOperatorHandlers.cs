#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResultOperatorHandlers.cs">
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

using System;
using System.Collections.Generic;
using System.Linq;
using GoodlyFere.Parse.Linq.Translation.ExpressionVisitors;
using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.ResultOperators;

#endregion

namespace GoodlyFere.Parse.Linq.Translation.Handlers
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