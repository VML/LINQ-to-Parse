// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="TransformationVisitor.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/12/2013 1:09 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using Remotion.Linq;
using Remotion.Linq.Clauses;
using VML.Parse.Linq.Transformation.ExpressionVisitors;

#endregion

namespace VML.Parse.Linq.Transformation
{
    public class TransformationVisitor : QueryModelVisitorBase
    {
        #region Public Methods

        public static QueryModel Transform(QueryModel model)
        {
            var visitor = new TransformationVisitor();
            visitor.VisitQueryModel(model);

            return model;
        }

        public override void VisitWhereClause(WhereClause whereClause, QueryModel queryModel, int index)
        {
            whereClause.Predicate = RootExpressionVisitor.Transform(whereClause.Predicate);
            base.VisitWhereClause(whereClause, queryModel, index);
        }

        #endregion
    }
}