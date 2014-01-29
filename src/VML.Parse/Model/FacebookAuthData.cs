// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="FacebookAuthData.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>01/23/2014 2:37 PM</created>
//  <updated>01/24/2014 8:17 AM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System.Linq;
using System;
using System.Runtime.Serialization;

#endregion

namespace VML.Parse.Model
{
    [DataContract]
    public class FacebookAuthData
    {
        #region Constructors and Destructors

        public FacebookAuthData()
        {
        }

        public FacebookAuthData(string id, string accessToken, DateTime expirationDate)
        {
            Id = id;
            AccessToken = accessToken;
            ExpirationDate = expirationDate;
        }

        #endregion

        #region Public Properties

        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name = "expiration_date")]
        public DateTime ExpirationDate { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        #endregion
    }
}