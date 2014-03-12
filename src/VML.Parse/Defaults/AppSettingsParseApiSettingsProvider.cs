// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="AppSettingsParseApiSettingsProvider.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/09/2013 10:08 AM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Configuration;
using System.Linq;
using VML.Parse.Interfaces;

#endregion

namespace VML.Parse.Defaults
{
    public class AppSettingsParseApiSettingsProvider : IParseApiSettingsProvider
    {
        #region Public Properties

        public virtual string ApiUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ParseApiUrl"];
            }
        }

        public virtual string ApplicationId
        {
            get
            {
                return ConfigurationManager.AppSettings["ParseApplicationId"];
            }
        }

        public virtual string CurrentUserSessionToken
        {
            get
            {
                return string.Empty;
            }
        }

        public virtual string RestApiKey
        {
            get
            {
                return ConfigurationManager.AppSettings["ParseRestApiKey"];
            }
        }

        #endregion
    }
}