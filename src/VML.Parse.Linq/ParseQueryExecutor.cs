// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ParseQueryExecutor.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/02/2013 9:43 AM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Remotion.Linq;
using VML.Parse.Interfaces;
using VML.Parse.Linq.Exceptions;
using VML.Parse.Linq.Execution.Maps;
using VML.Parse.Linq.Transformation;
using VML.Parse.Linq.Translation;

#endregion

namespace VML.Parse.Linq
{
    public class ParseQueryExecutor : IQueryExecutor
    {
        #region Constants and Fields

        private readonly IParseApi _parseApi;

        #endregion

        #region Constructors and Destructors

        public ParseQueryExecutor(IParseApi parseApi)
        {
            _parseApi = parseApi;
        }

        #endregion

        #region Public Methods

        public IEnumerable<T> ExecuteCollection<T>(QueryModel queryModel)
        {
            string queryString = TranslateToQueryString(queryModel);
            IList<T> query = _parseApi.Query<T>(queryString);

            return query.ToList();
        }

        public T ExecuteScalar<T>(QueryModel queryModel)
        {
            if (!ScalarResultMaps.Has(typeof(T)))
            {
                throw new InvalidQueryException(
                    string.Format("'{0}' result operators are not supported.", typeof(T).Name));
            }

            string queryString = TranslateToQueryString(queryModel);
            IDictionary handlerMap = ScalarResultMaps.Get(typeof(T));
            T returnValue = default(T);

            foreach (var resultOperator in queryModel.ResultOperators)
            {
                if (!handlerMap.Contains(resultOperator.GetType()))
                {
                    throw new InvalidQueryException(
                        string.Format("'{0}' result operator is not supported.", resultOperator.GetType().Name));
                }

                var handler = (ScalarResultHandlerMethod<T>)handlerMap[resultOperator.GetType()];
                returnValue = handler.Invoke(queryString, _parseApi, queryModel.MainFromClause.ItemType);
            }

            return returnValue;
        }

        public T ExecuteSingle<T>(QueryModel queryModel, bool returnDefaultWhenEmpty)
        {
            string queryString = TranslateToQueryString(queryModel);
            IList<T> query = _parseApi.Query<T>(queryString);

            if (returnDefaultWhenEmpty)
            {
                return query.FirstOrDefault();
            }

            return query.First();
        }

        #endregion

        #region Methods

        private static string TranslateToQueryString(QueryModel queryModel)
        {
            queryModel = TransformationVisitor.Transform(queryModel);
            string queryString = TranslationVisitor.Translate(queryModel);
            return queryString;
        }

        #endregion
    }
}