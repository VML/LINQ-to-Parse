#region Usings

using System;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using RestSharp;
using VML.Parse.Extensions;
using VML.Parse.Interfaces;
using VML.Parse.JSON;
using VML.Parse.Model;
using VML.Parse.ResultSets;

#endregion

namespace VML.Parse.Defaults
{
    public class RequestExecutor : IRequestExecutor
    {
        #region Constants and Fields

        private readonly RestClient _restClient;
        private readonly IParseApiSettingsProvider _settingsProvider;

        #endregion

        #region Constructors and Destructors

        public RequestExecutor()
            : this(null, null)
        {
        }

        public RequestExecutor(IParseApiSettingsProvider settingsProvider)
            : this(null, settingsProvider)
        {
        }

        public RequestExecutor(RestClient restClient, IParseApiSettingsProvider settingsProvider)
        {
            _restClient = restClient ?? new RestClient();
            _settingsProvider = settingsProvider ?? new AppSettingsParseApiSettingsProvider();

            _restClient.BaseUrl = _settingsProvider.ApiUrl;
            _restClient.AddHandler("application/json", new ParseDeserializer());
        }

        #endregion

        #region Public Methods

        public void CheckForParseError<T>(
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

            ParseBasicResponse basicResponse = JsonConvert.DeserializeObject<ParseBasicResponse>(response.Content);
            throw new Exception(basicResponse.Error);
        }

        public void CheckForResponseError<T>(IRestResponse<T> response)
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

        public IRestResponse<T> ExecuteRequest<T>(IRestRequest request, params HttpStatusCode[] acceptableStatusCodes)
            where T : new()
        {
            AddParseHeaders(request);

            IRestResponse<T> response = _restClient.Execute<T>(request);

            CheckForResponseError(response);
            CheckForParseError(response, acceptableStatusCodes);

            return response;
        }

        public ParseUser ExecuteUserRequest(IRestRequest request)
        {
            IRestResponse<ParseUser> response = ExecuteRequest<ParseUser>(
                request, HttpStatusCode.OK, HttpStatusCode.Created);
            return response.Data;
        }

        #endregion

        #region Methods

        private void AddParseHeaders(IRestRequest request)
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