#region Usings

using System;
using System.Linq;
using System.Net;
using RestSharp;
using VML.Parse.Model;
using VML.Parse.ResultSets;
using VML.Parse.Util;

#endregion

namespace VML.Parse
{
    public partial class ParseApi
    {
        #region Public Methods

        public ParseUser FacebookSignIn(
            string username, string email, string id, string accessToken, DateTime expirationDate)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("id");
            }
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                throw new ArgumentNullException("accessToken");
            }

            FacebookAuthData facebookAuthData = new FacebookAuthData(id, accessToken, expirationDate);
            AuthData authData = new AuthData(facebookAuthData);

            RestRequest request = RequestBuilder.BuildDefaultRequest("users");
            request.Method = Method.POST;
            request.AddBody(new { username, email, authData });

            return _executor.ExecuteUserRequest(request);
        }

        public ParseUser GetCurrent()
        {
            RestRequest request = RequestBuilder.BuildDefaultRequest("users/me");
            request.Method = Method.GET;
            return _executor.ExecuteUserRequest(request);
        }

        public ParseUser LinkToFacebook(string userObjectId, FacebookAuthData facebookAuthData)
        {
            AuthData authData = new AuthData(facebookAuthData);

            RestRequest request = RequestBuilder.BuildDefaultRequest("users/" + userObjectId);
            request.Method = Method.PUT;
            request.AddBody(new { authData });

            return _executor.ExecuteUserRequest(request);
        }

        public bool ResetPassword(string email)
        {
            RestRequest request = RequestBuilder.BuildDefaultRequest("requestPasswordReset");
            request.Method = Method.POST;
            request.AddBody(new { email });

            IRestResponse<ParseBasicResponse> response = _executor.ExecuteRequest<ParseBasicResponse>(request);

            return string.IsNullOrWhiteSpace(response.Data.Error) && response.Data.Code == 0;
        }

        public ParseUser SignIn(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException("username");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("password");
            }

            RestRequest request = RequestBuilder.BuildDefaultRequest("login");
            request.Method = Method.GET;
            request.AddParameter("username", username);
            request.AddParameter("password", password);

            return _executor.ExecuteUserRequest(request);
        }

        public ParseUser SignUp(ParseUser newUser, string password)
        {
            if (newUser == null)
            {
                throw new ArgumentNullException("newUser");
            }

            newUser["password"] = password;
            newUser.Remove("createdAt");
            newUser.Remove("updatedAt");
            newUser.Remove("authData");
            RestRequest request = RequestBuilder.BuildDefaultRequest("users");
            request.Method = Method.POST;
            request.AddBody(newUser);

            IRestResponse<ParseUser> response = _executor.ExecuteRequest<ParseUser>(
                request, HttpStatusCode.Created);
            return response.Data;
        }

        public bool UnlinkFromFacebook(string userObjectId)
        {
            if (string.IsNullOrWhiteSpace(userObjectId))
            {
                throw new ArgumentNullException("userObjectId");
            }

            RestRequest request = RequestBuilder.BuildDefaultRequest("users/" + userObjectId);
            request.Method = Method.PUT;
            request.AddBody(new { authData = new AuthData() });

            var response = _executor.ExecuteRequest<ParseBasicResponse>(request);
            return string.IsNullOrWhiteSpace(response.Data.Error);
        }

        public bool ValidateSession()
        {
            try
            {
                return GetCurrent() != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}