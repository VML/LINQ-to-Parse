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
using GoodlyFere.Parse.Linq.Translation.ExpressionVisitors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Remotion.Linq;
using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.ResultOperators;

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

        public override void VisitResultOperator(ResultOperatorBase resultOperator, QueryModel queryModel, int index)
        {
            if (resultOperator is SkipResultOperator)
            {
                var skipRO = (SkipResultOperator)resultOperator;
                Parameters.Add("skip", ConstantValueFinder.Find(skipRO.Count).ToString());
            }
            else if (resultOperator is TakeResultOperator)
            {
                var takeRO = (TakeResultOperator)resultOperator;
                Parameters.Add("limit", ConstantValueFinder.Find(takeRO.Count).ToString());
            }
            else if (resultOperator is FirstResultOperator)
            {
                Parameters.Add("limit", "1");
            }
            else if (resultOperator is CountResultOperator)
            {
                Parameters.Add("limit", "0");
                Parameters.Add("count", "1");
            }

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