// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ParseUser.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>01/09/2014 5:08 PM</created>
//  <updated>01/24/2014 10:50 AM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using RestSharp;
using VML.Parse.Attributes;
using VML.Parse.ResultSets;

#endregion

namespace VML.Parse.Model
{
    [DataContract]
    [ParseClassName("_User")]
    public class ParseUser : ParseObject
    {
        #region Public Properties

        [DataMember(Name = "authData")]
        public AuthData AuthData
        {
            get
            {
                return GetProperty<AuthData>("authData");
            }
            set
            {
                SetProperty("authData", value);
            }
        }

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

        public static ParseUser FacebookSignIn(
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

            RestRequest request = ParseContext.API.GetDefaultRequest("users");
            request.Method = Method.POST;
            request.AddBody(new { username, email, authData });

            return ExecuteUserRequest(request);
        }

        public static ParseUser GetCurrent()
        {
            RestRequest request = ParseContext.API.GetDefaultRequest("users/me");
            request.Method = Method.GET;
            return ExecuteUserRequest(request);
        }

        public static ParseUser LinkToFacebook(string userObjectId, FacebookAuthData facebookAuthData)
        {
            AuthData authData = new AuthData(facebookAuthData);

            RestRequest request = ParseContext.API.GetDefaultRequest("users/" + userObjectId);
            request.Method = Method.PUT;
            request.AddBody(new { authData });

            return ExecuteUserRequest(request);
        }

        public static bool ResetPassword(string email)
        {
            RestRequest request = ParseContext.API.GetDefaultRequest("requestPasswordReset");
            request.Method = Method.POST;
            request.AddBody(new { email });

            IRestResponse<ParseBasicResponse> response = ParseContext.API.ExecuteRequest<ParseBasicResponse>(request);

            return string.IsNullOrWhiteSpace(response.Data.Error) && response.Data.Code == 0;
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

            RestRequest request = ParseContext.API.GetDefaultRequest("login");
            request.Method = Method.GET;
            request.AddParameter("username", username);
            request.AddParameter("password", password);

            return ExecuteUserRequest(request);
        }

        public static ParseUser SignUp(ParseUser newUser, string password)
        {
            if (newUser == null)
            {
                throw new ArgumentNullException("newUser");
            }

            newUser["password"] = password;
            newUser.Remove("createdAt");
            newUser.Remove("updatedAt");
            RestRequest request = ParseContext.API.GetDefaultRequest("users");
            request.Method = Method.POST;
            request.AddBody(newUser);

            IRestResponse<ParseUser> response = ParseContext.API.ExecuteRequest<ParseUser>(
                request, HttpStatusCode.Created);
            return response.Data;
        }

        public static bool UnlinkFromFacebook(string userObjectId)
        {
            if (string.IsNullOrWhiteSpace(userObjectId))
            {
                throw new ArgumentNullException("userObjectId");
            }

            RestRequest request = ParseContext.API.GetDefaultRequest("users/" + userObjectId);
            request.Method = Method.PUT;
            request.AddBody(new { authData = new AuthData() });

            var response = ParseContext.API.ExecuteRequest<ParseBasicResponse>(request);
            return string.IsNullOrWhiteSpace(response.Data.Error);
        }

        public static bool ValidateSession()
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

        #region Methods

        private static ParseUser ExecuteUserRequest(RestRequest request)
        {
            IRestResponse<ParseUser> response = ParseContext.API.ExecuteRequest<ParseUser>(
                request, HttpStatusCode.OK, HttpStatusCode.Created);
            return response.Data;
        }

        #endregion
    }
}