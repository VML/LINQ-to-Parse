#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BinaryExpressionHandlers.cs">
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
using System.Linq.Expressions;
using GoodlyFere.Parse.Linq.Generation.Maps;

#endregion

namespace GoodlyFere.Parse.Linq.Generation.Handlers
{
    public static class BinaryExpressionHandlers
    {
        #region Constants and Fields

        private static readonly BinaryOperatorMap OperatorMap;

        #endregion

        #region Constructors and Destructors

        static BinaryExpressionHandlers()
        {
            OperatorMap = new BinaryOperatorMap();
        }

        #endregion

        #region Public Methods

        public static void Equals(Dictionary<string, object> query, string currentKey, object currentValue)
        {
            query[currentKey] = currentValue;
        }

        public static void Other(Dictionary<string, object> query, string currentKey, object currentValue, ExpressionType exprType)
        {
            query[currentKey] = new Dictionary<string, object>
                {
                    { OperatorMap[exprType], currentValue }
                };
        }

        #endregion
    }
}