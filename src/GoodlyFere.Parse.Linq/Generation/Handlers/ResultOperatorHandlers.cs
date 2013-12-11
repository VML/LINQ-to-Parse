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
using System.Collections;
using System.Linq;
using GoodlyFere.Parse.Linq.Generation.ExpressionVisitors;
using GoodlyFere.Parse.Linq.Generation.ParseQuery;
using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.ResultOperators;

#endregion

namespace GoodlyFere.Parse.Linq.Generation.Handlers
{
    internal class ResultOperatorHandlers
    {
        #region Public Methods

        public static ConstraintSet HandleIEnumerableMethods(ResultOperatorBase resultOperator, IEnumerable values)
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