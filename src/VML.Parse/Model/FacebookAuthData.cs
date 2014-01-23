// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="FacebookAuthData.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>01/23/2014 2:37 PM</created>
//  <updated>01/23/2014 2:41 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System.Linq;
using System;

#endregion

namespace VML.Parse.Model
{
    public class FacebookAuthData
    {
        #region Constructors and Destructors

        public FacebookAuthData(string id, string accessToken, DateTime expirationDate)
        {
            Id = id;
            AccessToken = accessToken;
            ExpirationDate = expirationDate;
        }

        #endregion

        #region Public Properties

        public string AccessToken { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Id { get; set; }

        #endregion
    }
}