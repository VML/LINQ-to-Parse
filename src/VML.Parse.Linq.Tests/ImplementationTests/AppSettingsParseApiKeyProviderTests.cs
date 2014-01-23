// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="AppSettingsParseApiKeyProviderTests.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/02/2013 11:23 AM</created>
//  <updated>01/23/2014 2:33 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Configuration;
using System.Linq;
using VML.Parse.DefaultImplementations;
using Xunit;

#endregion

namespace VML.Parse.Linq.Tests.ImplementationTests
{
    public class AppSettingsParseApiKeyProviderTests
    {
        #region Constants and Fields

        private readonly AppSettingsParseApiSettingsProvider _provider;

        #endregion

        #region Constructors and Destructors

        public AppSettingsParseApiKeyProviderTests()
        {
            _provider = new AppSettingsParseApiSettingsProvider();
        }

        #endregion

        #region Public Methods

        [Fact]
        public void ApplicationId_MatchesAppSettings()
        {
            string appId = ConfigurationManager.AppSettings["ParseApplicationId"];
            Assert.NotNull(appId);
            Assert.Equal(appId, _provider.ApplicationId);
        }

        [Fact]
        public void ParseApiUrl_MatchesAppSettings()
        {
            string apiUrl = ConfigurationManager.AppSettings["ParseApiUrl"];
            Assert.NotNull(apiUrl);
            Assert.Equal(apiUrl, _provider.ApiUrl);
        }

        [Fact]
        public void RestApiKey_MatchesAppSettings()
        {
            string restKey = ConfigurationManager.AppSettings["ParseRestApiKey"];
            Assert.NotNull(restKey);
            Assert.Equal(restKey, _provider.RestApiKey);
        }

        #endregion
    }
}