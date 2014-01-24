// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="AuthData.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>01/23/2014 2:38 PM</created>
//  <updated>01/24/2014 8:17 AM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using System.Runtime.Serialization;

#endregion

namespace VML.Parse.Model
{
    [DataContract]
    public class AuthData
    {
        #region Constructors and Destructors

        public AuthData()
        {
        }

        public AuthData(FacebookAuthData facebook)
        {
            Facebook = facebook;
        }

        #endregion

        #region Public Properties

        [DataMember(Name = "facebook")]
        public FacebookAuthData Facebook { get; set; }

        #endregion
    }
}