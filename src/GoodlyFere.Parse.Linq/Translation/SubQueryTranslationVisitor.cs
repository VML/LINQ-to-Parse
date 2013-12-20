#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubQueryTranslationVisitor.cs">
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
using System.Linq.Expressions;
using GoodlyFere.Parse.Linq.Translation.Maps;
using GoodlyFere.Parse.Linq.Translation.ParseQuery;
using Remotion.Linq;
using Remotion.Linq.Clauses;

#endregion

namespace GoodlyFere.Parse.Linq.Translation
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
                ConstraintSet set = SubQueryResultOperatorMap.Get(resultOperator.GetType()).Invoke(resultOperator, _values);
                Query.AddConstraint(set);
            }

            base.VisitResultOperator(resultOperator, queryModel, index);
        }

        #endregion
    }
}