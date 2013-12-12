#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryRoot.cs">
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
using GoodlyFere.Parse.Linq.Translation.ParseQuery.JsonConverters;
using Newtonsoft.Json;

#endregion

namespace GoodlyFere.Parse.Linq.Translation.ParseQuery
{
    [JsonConverter(typeof(QueryRootJsonConverter))]
    internal class QueryRoot : List<IQueryPiece>
    {
        #region Public Methods

        public void AddConstraint(IQueryPiece constraint)
        {
            IQueryPiece existingForProperty = FindForProperty(constraint.Key);
            if (existingForProperty == null)
            {
                Add(constraint);
            }
            else if (existingForProperty is EqualsConstraint)
            {
                throw new Exception("Parse does not support multiple equals expressions ANDed together.");
            }
            else if (existingForProperty is ConstraintSet)
            {
                var set = existingForProperty as ConstraintSet;
                if (constraint is BasicQueryPiece)
                {
                    set.Operators.Add(constraint as BasicQueryPiece);
                }
                else if (constraint is ConstraintSet)
                {
                    var secondSet = constraint as ConstraintSet;
                    set.Merge(secondSet);
                }
            }
        }

        public void AddConstraintRange(IEnumerable<IQueryPiece> constraints)
        {
            foreach (var c in constraints)
            {
                AddConstraint(c);
            }
        }

        public IQueryPiece FindForProperty(string propertyName)
        {
            return this.SingleOrDefault(qp => qp.Key == propertyName);
        }

        #endregion
    }
}