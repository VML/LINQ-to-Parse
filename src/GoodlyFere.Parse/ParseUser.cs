#region Usings

using System;
using System.Linq;
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

            RestRequest request = new RestRequest("users") { Method = Method.POST, RequestFormat = DataFormat.Json };
            request.AddBody(newUser);

            IRestResponse<ParseUser> response = ParseContext.API.ExecuteRequest<ParseUser>(request);
            return response.Data;
        }

        public static bool ValidateSession(string sessionToken)
        {
            try
            {
                return GetByToken(sessionToken) != null;
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
            IRestResponse<ParseUser> response = ParseContext.API.ExecuteRequest<ParseUser>(request);
            return response.Data;
        }

        #endregion
    }
}