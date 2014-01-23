// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ParseApi.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/09/2013 10:17 AM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
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
    public class ParseApi
    {
        #region Constants and Fields

        private readonly IParseApiSettingsProvider _settingsProvider;

        #endregion

        #region Constructors and Destructors

        public ParseApi(IParseApiSettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;
        }

        #endregion

        #region Public Methods

        public static void CheckForParseError<T>(
            IRestResponse<T> response, params HttpStatusCode[] acceptableStatusCodes)
        {
            if (response.StatusCode == HttpStatusCode.OK
                || acceptableStatusCodes.Contains(response.StatusCode))
            {
                return;
            }

            "ParseApi".Log().Error(
                String.Format(
                    "Parse error: {0}, {1}",
                    response.StatusCode,
                    response.StatusDescription));

            throw new Exception(response.StatusDescription);
        }

        public static void CheckForResponseError<T>(IRestResponse<T> response)
        {
            if (response.ResponseStatus == ResponseStatus.Completed)
            {
                return;
            }

            "ParseApi".Log().Error(
                String.Format("Response error: {0}", response.ResponseStatus),
                response.ErrorException);

            throw response.ErrorException;
        }

        public long Count(string queryString, Type objectType)
        {
            string uri = GetQueryRequestUri(objectType);
            var request = GetDefaultRequest(uri);
            SetParameters(queryString, request);

            IRestResponse<ParseCountResults> response = ExecuteRequest<ParseCountResults>(request);

            return response.Data.Count;
        }

        public T Create<T>(T modelToCreate) where T : IBaseModel, new()
        {
            string uri = GetQueryRequestUri<T>();
            uri += "/" + modelToCreate.ObjectId;
            RestRequest request = GetDefaultRequest(uri);
            request.Method = Method.POST;
            request.AddBody(modelToCreate);

            var response = ExecuteRequest<T>(request, HttpStatusCode.Created);
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
            RestRequest request = GetDefaultRequest(uri);
            request.Method = Method.DELETE;

            ExecuteRequest<T>(request);
            return true;
        }

        public IList<T> Query<T>(string queryString)
        {
            string uri = GetQueryRequestUri<T>();
            var request = GetDefaultRequest(uri);
            SetParameters(queryString, request);

            IRestResponse<ParseQueryResults<T>> response = ExecuteRequest<ParseQueryResults<T>>(request);
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
            RestRequest request = GetDefaultRequest(uri);
            request.Method = Method.PUT;
            request.AddBody(modelToSave);

            ExecuteRequest<T>(request);
            // response only contains updatedAt field, so we just return the same updated object
            return modelToSave;
        }

        #endregion

        #region Methods

        internal IRestResponse<T> ExecuteRequest<T>(IRestRequest request, params HttpStatusCode[] acceptableStatusCodes)
            where T : new()
        {
            RestClient client = new RestClient(_settingsProvider.ApiUrl);
            client.AddHandler("application/json", new ParseDeserializer());
            SetParseHeaders(request);

            IRestResponse<T> response = client.Execute<T>(request);
            CheckForResponseError(response);
            CheckForParseError(response, acceptableStatusCodes);

            return response;
        }

        internal RestRequest GetDefaultRequest(string uri)
        {
            RestRequest request = new RestRequest(uri)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new ParseSerializer()
                };
            return request;
        }

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

        private void SetParseHeaders(IRestRequest request)
        {
            request.AddHeader("X-Parse-Application-Id", _settingsProvider.ApplicationId);
            request.AddHeader("X-Parse-REST-API-Key", _settingsProvider.RestApiKey);
            request.AddHeader("Content-Type", "application/json");

            if (!string.IsNullOrWhiteSpace(_settingsProvider.CurrentUserSessionToken))
            {
                request.AddHeader("X-Parse-Session-Token", _settingsProvider.CurrentUserSessionToken);
            }
        }

        #endregion
    }
}