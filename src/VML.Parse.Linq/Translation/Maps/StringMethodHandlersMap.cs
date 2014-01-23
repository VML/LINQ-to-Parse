// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="StringMethodHandlersMap.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>01/09/2014 6:14 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using VML.Parse.Linq.Translation.Handlers;

#endregion

namespace VML.Parse.Linq.Translation.Maps
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