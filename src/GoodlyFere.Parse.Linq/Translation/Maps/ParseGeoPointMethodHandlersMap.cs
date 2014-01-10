#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GoodlyFere.Parse.Linq.Translation.Handlers;
using GoodlyFere.Parse.Linq.Translation.ParseQuery;

#endregion

namespace GoodlyFere.Parse.Linq.Translation.Maps
{
    internal class ParseGeoPointMethodHandlersMap :
        Map<ParseGeoPointMethodHandlersMap, string, MethodCallHandlerFactoryMethod>
    {
        #region Constructors and Destructors

        public ParseGeoPointMethodHandlersMap()
        {
            Add("NearSphere", ParseGeoPointMethodHandlers.HandleNearSphere);
        }

        #endregion
    }
}