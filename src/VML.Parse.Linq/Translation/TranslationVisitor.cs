// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="TranslationVisitor.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/10/2013 5:24 PM</created>
//  <updated>01/23/2014 2:33 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System.Collections.Generic;
using System.Linq;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Remotion.Linq;
using Remotion.Linq.Clauses;
using VML.Parse.Linq.Exceptions;
using VML.Parse.Linq.Translation.ExpressionVisitors;
using VML.Parse.Linq.Translation.Maps;

#endregion

namespace VML.Parse.Linq.Translation
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