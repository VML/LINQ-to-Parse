#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TranslationVisitor.cs">
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

using System.Collections.Generic;
using System.Linq;
using System;
using GoodlyFere.Parse.Linq.Exceptions;
using GoodlyFere.Parse.Linq.Translation.ExpressionVisitors;
using GoodlyFere.Parse.Linq.Translation.Maps;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Remotion.Linq;
using Remotion.Linq.Clauses;

#endregion

namespace GoodlyFere.Parse.Linq.Translation
{
    public class TranslationVisitor : QueryModelVisitorBase
    {
        #region Constructors and Destructors

        protected TranslationVisitor()
        {
            Parameters = new Dictionary<string, string>();
        }

        #endregion

        #region Properties

        protected Dictionary<string, string> Parameters { get; private set; }

        #endregion

        #region Public Methods

        public static string Translate(QueryModel model)
        {
            var visitor = new TranslationVisitor();
            visitor.VisitQueryModel(model);

            return string.Join(
                "&",
                visitor.Parameters
                       .Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value)));
        }

        public override void VisitOrderByClause(OrderByClause orderByClause, QueryModel queryModel, int index)
        {
            List<string> orderingProperties = new List<string>();
            foreach (var ordering in orderByClause.Orderings)
            {
                string propertyName = MemberNameFinder.Find(ordering.Expression);
                propertyName = ordering.OrderingDirection == OrderingDirection.Desc ? "-" + propertyName : propertyName;
                orderingProperties.Add(propertyName);
            }

            Parameters.Add("order", string.Join(",", orderingProperties));

            base.VisitOrderByClause(orderByClause, queryModel, index);
        }

        public override void VisitResultOperator(ResultOperatorBase resultOperator, QueryModel queryModel, int index)
        {
            if (!ResultOperatorMap.Has(resultOperator.GetType()))
            {
                throw new InvalidQueryException(
                    string.Format("{0} result operator cannot be handled.", resultOperator.GetType().Name));
            }

            var handler = ResultOperatorMap.Get(resultOperator.GetType());
            handler.Invoke(resultOperator, Parameters);

            base.VisitResultOperator(resultOperator, queryModel, index);
        }

        public override void VisitWhereClause(WhereClause whereClause, QueryModel queryModel, int index)
        {
            var query = RootExpressionVisitor.Translate(whereClause.Predicate);
            var settings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

            Parameters.Add("where", JsonConvert.SerializeObject(query, settings));

            base.VisitWhereClause(whereClause, queryModel, index);
        }

        #endregion
    }
}