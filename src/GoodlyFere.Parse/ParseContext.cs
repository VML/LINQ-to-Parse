#region Usings

using System;
using System.Linq;
using GoodlyFere.Parse.Interfaces;

#endregion

namespace GoodlyFere.Parse
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