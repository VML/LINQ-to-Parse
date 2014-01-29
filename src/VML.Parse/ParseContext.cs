// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ParseContext.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/09/2013 10:28 AM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using VML.Parse.Interfaces;

#endregion

namespace VML.Parse
{
    public class ParseContext
    {
        #region Constants and Fields

        private readonly ParseApi _api;
        private static ParseContext _instance;
        private IParseApiSettingsProvider _settingsProvider;

        #endregion

        #region Constructors and Destructors

        private ParseContext(IParseApiSettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;
            _api = new ParseApi(settingsProvider);
        }

        #endregion

        #region Public Properties

        public static ParseApi API
        {
            get
            {
                return _instance._api;
            }
        }

        #endregion

        #region Public Methods

        public static void Initialize(IParseApiSettingsProvider settingsProvider)
        {
            _instance = new ParseContext(settingsProvider);
        }

        #endregion
    }
}