#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResultOperatorMap.cs">
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
using System.Collections.Generic;
using System.Linq;
using GoodlyFere.Parse.Linq.Generation.Handlers;
using GoodlyFere.Parse.Linq.Generation.ParseQuery;
using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.ResultOperators;

#endregion

namespace GoodlyFere.Parse.Linq.Generation.Maps
{
    internal delegate ConstraintSet ResultOperatorFactoryMethod(
        ResultOperatorBase resultOperator, IEnumerable values);

    internal class ResultOperatorMap : Dictionary<Type, ResultOperatorFactoryMethod>
    {
        #region Constants and Fields

        private static readonly ResultOperatorMap Instance;

        #endregion

        #region Constructors and Destructors

        static ResultOperatorMap()
        {
            Instance = new ResultOperatorMap();
        }

        protected ResultOperatorMap()
        {
            Add(typeof(ContainsResultOperator), ResultOperatorHandlers.HandleIEnumerableMethods);
        }

        #endregion

        #region Public Methods

        public static ResultOperatorFactoryMethod Get(Type type)
        {
            return Has(type) ? Instance[type] : null;
        }

        public static bool Has(Type type)
        {
            return Instance.ContainsKey(type);
        }

        #endregion
    }
}