#region Usings

using System;
using System.Linq;
using GoodlyFere.Parse.Linq.Translation.Handlers;

#endregion

namespace GoodlyFere.Parse.Linq.Translation.Maps
{
    internal class StringMethodHandlersMap : Map<StringMethodHandlersMap, string, MethodCallHandlerFactoryMethod>
    {
        #region Constructors and Destructors

        public StringMethodHandlersMap()
        {
            Add("Contains", StringMethodHandlers.HandleContains);
        }

        #endregion
    }
}