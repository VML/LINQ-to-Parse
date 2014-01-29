// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IParseApiSettingsProvider.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>01/23/2014 2:23 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;

#endregion

namespace VML.Parse.Interfaces
{
    public interface IParseApiSettingsProvider
    {
        #region Public Properties

        string ApiUrl { get; }
        string ApplicationId { get; }
        string CurrentUserSessionToken { get; }
        string RestApiKey { get; }

        #endregion
    }
}