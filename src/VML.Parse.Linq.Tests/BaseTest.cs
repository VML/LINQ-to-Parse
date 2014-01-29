// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="BaseTest.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/02/2013 12:19 PM</created>
//  <updated>01/23/2014 2:33 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using LoggingExtensions.Logging;
using LoggingExtensions.log4net;
using Plant.Core;

#endregion

namespace VML.Parse.Linq.Tests
{
    public class BaseTest
    {
        #region Constructors and Destructors

        static BaseTest()
        {
            Log.InitializeWith<Log4NetLog>();
        }

        public BaseTest()
        {
            ObjectPlant = new BasePlant().WithBlueprintsFromAssemblyOf<BaseTest>();
        }

        #endregion

        #region Properties

        protected BasePlant ObjectPlant { get; set; }

        #endregion
    }
}