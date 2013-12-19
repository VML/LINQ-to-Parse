#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParseUser.cs">
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

using System;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using GoodlyFere.Parse.Attributes;
using RestSharp;

#endregion

namespace GoodlyFere.Parse
{
    [DataContract]
    [ParseClassName("_User")]
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

        public static ParseUser SignUp(ParseUser newUser, string password)
        {
            if (newUser == null)
            {
                throw new ArgumentNullException("newUser");
            }

            newUser["password"] = password;
            RestRequest request = new RestRequest("users") { Method = Method.POST, RequestFormat = DataFormat.Json };
            request.AddBody(newUser);

            IRestResponse<ParseUser> response = ParseContext.API.ExecuteRequest<ParseUser>(request, HttpStatusCode.Created);
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