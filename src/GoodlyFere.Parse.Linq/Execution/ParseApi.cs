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
using System.Linq;
using System;
using GoodlyFere.Parse.Linq.Interfaces;
using RestSharp;

#endregion

namespace GoodlyFere.Parse.Linq.Execution
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

        public IList<T> Query<T>(params Parameter[] parameters)
        {
            var client = new RestClient(_settingsProvider.ApiUrl);
            var request = new RestRequest("classes/" + typeof(T).Name);

            SetParameters(parameters, request);
            SetParseHeaders(request);

            IRestResponse<ParseResults<T>> response = client.Execute<ParseResults<T>>(request);
            return GetResults(response);
        }

        #endregion

        #region Methods

        private static void SetParameters(IEnumerable<Parameter> parameters, RestRequest request)
        {
            if (parameters == null)
            {
                return;
            }

            foreach (var parameter in parameters)
            {
                request.AddParameter(parameter);
            }
        }

        private IList<T> GetResults<T>(IRestResponse<ParseResults<T>> response)
        {
            ParseResults<T> results = response.Data;

            if (results.Code > 0)
            {
                this.Log().Error("Parse query failed: {0}", results.Error);
                return new List<T>();
            }

            return results.Results;
        }

        private void SetParseHeaders(RestRequest request)
        {
            request.AddHeader("X-Parse-Application-Id", _settingsProvider.ApplicationId);
            request.AddHeader("X-Parse-REST-API-Key", _settingsProvider.RestApiKey);
        }

        #endregion
    }
}