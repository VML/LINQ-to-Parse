// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="SubQueryTranslationVisitor.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/11/2013 1:33 PM</created>
//  <updated>01/23/2014 2:33 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using Remotion.Linq;
using Remotion.Linq.Clauses;
using VML.Parse.Linq.Translation.Maps;
using VML.Parse.Linq.Translation.ParseQuery;

#endregion

namespace VML.Parse.Linq.Translation
{
    internal class SubQueryTranslationVisitor : QueryModelVisitorBase
    {
        #region Constants and Fields

        private IEnumerable _values;

        #endregion

        #region Constructors and Destructors

        public SubQueryTranslationVisitor()
        {
            Query = new QueryRoot();
        }

        #endregion

        #region Properties

        protected QueryRoot Query { get; set; }

        #endregion

        #region Public Methods

        public static QueryRoot Translate(QueryModel model)
        {
            var visitor = new SubQueryTranslationVisitor();
            visitor.VisitQueryModel(model);

            return visitor.Query;
        }

        public override void VisitMainFromClause(MainFromClause fromClause, QueryModel queryModel)
        {
            if (fromClause.FromExpression is ConstantExpression)
            {
                _values = (fromClause.FromExpression as ConstantExpression).Value as IEnumerable;
            }

            base.VisitMainFromClause(fromClause, queryModel);
        }

        public override void VisitResultOperator(ResultOperatorBase resultOperator, QueryModel queryModel, int index)
        {
            if (SubQueryResultOperatorMap.Has(resultOperator.GetType()))
            {
                ConstraintSet set = SubQueryResultOperatorMap.Get(resultOperator.GetType())
                                                             .Invoke(resultOperator, _values);
                Query.AddConstraint(set);
            }

            base.VisitResultOperator(resultOperator, queryModel, index);
        }

        #endregion
    }
}