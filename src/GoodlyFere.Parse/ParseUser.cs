#region Usings

using System;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using RestSharp;

#endregion

namespace GoodlyFere.Parse
{
    [DataContract]
    public class ParseUser : ParseObject
    {
        #region Public Properties

        [DataMember(Name = "email")]
        public string Email
        {
            get
            {
                return GetProperty<string>("email");
            }
            set
            {
                SetProperty("email", value);
            }
        }

        [DataMember(Name = "sessionToken")]
        public string SessionToken
        {
            get
            {
                return GetProperty<string>("sessionToken");
            }
            set
            {
                SetProperty("sessionToken", value);
            }
        }

        [DataMember(Name = "username")]
        public string Username
        {
            get
            {
                return GetProperty<string>("username");
            }
            set
            {
                SetProperty("username", value);
            }
        }

        #endregion

        #region Public Methods
        
        public static ParseUser GetByToken(string sessionToken)
        {
            RestRequest request = new RestRequest("users/me") { Method = Method.GET };
            request.AddHeader("X-Parse-Session-Token", sessionToken);

            return ExecuteUserRequest(request);
        }

        public static ParseUser SignIn(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException("username");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("password");
            }

            RestRequest request = new RestRequest("login") { Method = Method.GET };
            request.AddParameter("username", username);
            request.AddParameter("password", password);

            return ExecuteUserRequest(request);
        }

        public static ParseUser SignUp(ParseUser newUser)
        {
            if (newUser == null)
            {
                throw new ArgumentNullException("newUser");
            }

            RestRequest request = new RestRequest("users") { Method = Method.POST };
            request.AddBody(newUser);
            IRestResponse<ParseUser> response = ParseContext.API.ExecuteRequest<ParseUser>(request);

            CheckForResponseError(response);
            CheckForParseError(response);

            return response.Data;
        }

        #endregion

        #region Methods

        private static void CheckForParseError(IRestResponse<ParseUser> response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return;
            }

            "ParseUser".Log().Error(
                string.Format(
                    "Parse error: {0}, {1}",
                    response.StatusCode,
                    response.StatusDescription));

            throw new Exception(response.StatusDescription);
        }

        private static void CheckForResponseError(IRestResponse<ParseUser> response)
        {
            if (response.ResponseStatus == ResponseStatus.Completed)
            {
                return;
            }

            "ParseUser".Log().Error(
                string.Format("Response error: {0}", response.ResponseStatus),
                response.ErrorException);

            throw response.ErrorException;
        }

        private static ParseUser ExecuteUserRequest(RestRequest request)
        {
            IRestResponse<ParseUser> response = ParseContext.API.ExecuteRequest<ParseUser>(request);

            CheckForResponseError(response);
            CheckForParseError(response);

            return response.Data;
        }

        #endregion
    }
}