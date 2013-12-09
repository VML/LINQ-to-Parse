#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParseApi.cs">
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
using System.Collections.Specialized;
using System.Linq;
using System;
using System.Reflection;
using GoodlyFere.Parse.Attributes;
using GoodlyFere.Parse.Interfaces;
using RestSharp;
using RestSharp.Contrib;

#endregion

namespace GoodlyFere.Parse
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

        public IList<T> Query<T>(string queryString)
        {
            string uri = GetQueryRequestUri<T>();
            var request = new RestRequest(uri);
            SetParameters<T>(queryString, request);

            IRestResponse<ParseQueryResults<T>> response = ExecuteRequest<ParseQueryResults<T>>(request);
            return GetResults(response);
        }

        #endregion

        #region Methods

        internal IRestResponse<T> ExecuteRequest<T>(IRestRequest request) where T : new()
        {
            RestClient client = new RestClient(_settingsProvider.ApiUrl);
            SetParseHeaders(request);
            IRestResponse<T> response = client.Execute<T>(request);

            return response;
        }

        private static void SetParameters<T>(string queryString, RestRequest request)
        {
            NameValueCollection parameters = HttpUtility.ParseQueryString(queryString);

            foreach (string key in parameters.AllKeys)
            {
                request.AddParameter(key, parameters[key]);
            }
        }

        private string GetQueryRequestUri<T>()
        {
            Type type = typeof(T);
            CustomAttributeData attr =
                type.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(ParseClassNameAttribute));

            if (attr == null)
            {
                return "classes/" + type.Name;
            }
            else
            {
                return (string)attr.ConstructorArguments[0].Value;
            }
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
        }

        #endregion
    }
}