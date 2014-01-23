// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="BoolResultMap.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/19/2013 12:13 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;

#endregion

namespace VML.Parse.Linq.Execution.Maps
{
    internal class BoolResultMap : Map<BoolResultMap, Type, ScalarResultHandlerMethod<bool>>
    {
        #region Constructors and Destructors

        public BoolResultMap()
        {
        }

        #endregion
    }
}