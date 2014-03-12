// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ParseApi.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/09/2013 10:17 AM</created>
//  <updated>01/24/2014 10:28 AM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System;
using System.Net;
using RestSharp;
using RestSharp.Contrib;
using VML.Parse.Extensions;
using VML.Parse.Interfaces;
using VML.Parse.JSON;
using VML.Parse.ResultSets;
using VML.Parse.Util;

#endregion

namespace VML.Parse
{
    public partial class ParseApi
    {
        #region Public Methods

        public long Count(string queryString, Type objectType)
        {
            string uri = GetQueryRequestUri(objectType);
            var request = RequestBuilder.BuildDefaultRequest(uri);
            SetParameters(queryString, request);

            IRestResponse<ParseCountResults> response = _executor.ExecuteRequest<ParseCountResults>(request);

            return response.Data.Count;
        }

        public T Create<T>(T modelToCreate) where T : IBaseModel, new()
        {
            string uri = GetQueryRequestUri<T>();
            uri += "/" + modelToCreate.ObjectId;
            RestRequest request = RequestBuilder.BuildDefaultRequest(uri);
            request.Method = Method.POST;
            request.AddBody(modelToCreate);

            var response = _executor.ExecuteRequest<T>(request, HttpStatusCode.Created);
            Parameter locationHeader = response.Headers.FirstOrDefault(h => h.Name == "Location");

            if (locationHeader != null)
            {
                string id = locationHeader.Value.ToString().Split('/').Last();
                modelToCreate.ObjectId = id;
            }

            return modelToCreate;
        }

        public bool Delete<T>(T modelToDelete) where T : IBaseModel, new()
        {
            string uri = GetQueryRequestUri<T>();
            uri += "/" + modelToDelete.ObjectId;
            RestRequest request = RequestBuilder.BuildDefaultRequest(uri);
            request.Method = Method.DELETE;

            _executor.ExecuteRequest<T>(request);
            return true;
        }

        public IList<T> Query<T>(string queryString)
        {
            string uri = GetQueryRequestUri<T>();
            var request = RequestBuilder.BuildDefaultRequest(uri);
            SetParameters(queryString, request);

            IRestResponse<ParseQueryResults<T>> response = _executor.ExecuteRequest<ParseQueryResults<T>>(request);
            return GetResults(response);
        }

        public T Update<T>(T modelToSave) where T : IBaseModel, new()
        {
            if (String.IsNullOrWhiteSpace(modelToSave.ObjectId))
            {
                throw new ArgumentException("ObjectId must be set to save.");
            }

            string uri = GetQueryRequestUri<T>();
            uri += "/" + modelToSave.ObjectId;
            RestRequest request = RequestBuilder.BuildDefaultRequest(uri);
            request.Method = Method.PUT;
            request.AddBody(modelToSave);

            _executor.ExecuteRequest<T>(request);
            // response only contains updatedAt field, so we just return the same updated object
            return modelToSave;
        }

        #endregion

        #region Methods


        private static void SetParameters(string queryString, RestRequest request)
        {
            if (string.IsNullOrWhiteSpace(queryString))
            {
                return;
            }

            NameValueCollection parameters = HttpUtility.ParseQueryString(queryString);

            foreach (string key in parameters.AllKeys)
            {
                request.AddParameter(key, parameters[key]);
            }
        }

        private string GetQueryRequestUri(Type objectType)
        {
            string name = ClassUtils.GetParseClassName(objectType);

            if (name.Equals("users"))
            {
                return name;
            }

            return "classes/" + name;
        }

        private string GetQueryRequestUri<T>()
        {
            return GetQueryRequestUri(typeof(T));
        }

        private IList<T> GetResults<T>(IRestResponse<ParseQueryResults<T>> response)
        {
            ParseQueryResults<T> results = response.Data;

            if (results.Code > 0)
            {
                this.Log().Error("Parse query failed: {0}", results.Error);
                return new List<T>();
            }

            return results.Results;
        }

        #endregion
    }
}