// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="AuthData.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>01/23/2014 2:38 PM</created>
//  <updated>01/23/2014 2:39 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;

#endregion

namespace VML.Parse.Model
{
    public class AuthData
    {
        #region Constructors and Destructors

        public AuthData(FacebookAuthData facebook)
        {
            Facebook = facebook;
        }

        #endregion

        #region Public Properties

        public FacebookAuthData Facebook { get; set; }

        #endregion
    }
}