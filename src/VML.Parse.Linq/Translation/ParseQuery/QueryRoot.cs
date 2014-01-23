// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="QueryRoot.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/11/2013 10:08 AM</created>
//  <updated>01/23/2014 2:33 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using VML.Parse.Linq.Translation.ParseQuery.JsonConverters;

#endregion

namespace VML.Parse.Linq.Translation.ParseQuery
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