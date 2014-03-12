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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using VML.Parse.Attributes;
using VML.Parse.ResultSets;
using VML.Parse.Util;

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


        #endregion

        #region Methods


        #endregion
    }
}