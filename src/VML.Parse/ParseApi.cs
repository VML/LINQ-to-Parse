#region Usings

using System;
using System.Linq;
using VML.Parse.Defaults;
using VML.Parse.Interfaces;

#endregion

namespace VML.Parse
{
    public partial class ParseApi : IParseApi
    {
        #region Constants and Fields

        private readonly IRequestExecutor _executor;
        private readonly IParseApiSettingsProvider _settingsProvider;

        #endregion

        #region Constructors and Destructors

        public ParseApi()
            : this(null, null)
        {
        }

        public ParseApi(IRequestExecutor executor, IParseApiSettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider ?? new AppSettingsParseApiSettingsProvider();
            _executor = executor ?? new RequestExecutor(_settingsProvider);
        }

        #endregion
    }
}