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
using FluentAssertions;
using VML.Parse.Defaults;
using Xunit;

#endregion

namespace VML.Parse.Tests.Unit.Defaults
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
            appId.Should().NotBeNull();

            _provider.ApplicationId.Should().Be(appId);
        }

        [Fact]
        public void CurrentUserSessionToken_IsEmptyString()
        {
            _provider.CurrentUserSessionToken.Should().BeNullOrEmpty();
        }

        [Fact]
        public void ParseApiUrl_MatchesAppSettings()
        {
            string apiUrl = ConfigurationManager.AppSettings["ParseApiUrl"];
            apiUrl.Should().NotBeNull();

            _provider.ApiUrl.Should().Be(apiUrl);
        }

        [Fact]
        public void RestApiKey_MatchesAppSettings()
        {
            string restKey = ConfigurationManager.AppSettings["ParseRestApiKey"];
            restKey.Should().NotBeNull();

            _provider.RestApiKey.Should().Be(restKey);
        }

        #endregion
    }
}