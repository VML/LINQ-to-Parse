// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ParseApiTests.theorydata.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/02/2013 2:15 PM</created>
//  <updated>01/23/2014 2:33 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using VML.Parse.Tests.Support;

#endregion

namespace VML.Parse.Tests.Unit
{
    public partial class ParseApiTests
    {
        #region Public Properties

        public static IEnumerable<object[]> WhereClauses
        {
            get
            {
                return new[]
                    {
                        new object[]
                            {
                                "where={\"firstName\": \"Ben\"}",
                                (Func<TestObject, bool>)(to => to.FirstName == "Ben")
                            },
                        new object[]
                            {
                                "where={\"isOld\": false}",
                                (Func<TestObject, bool>)(to => !to.IsOld)
                            },
                        new object[]
                            {
                                "where={\"isOld\": true}",
                                (Func<TestObject, bool>)(to => to.IsOld)
                            },
                        new object[]
                            {
                                "where={\"firstName\": \"Ben\", \"lastName\": \"Ramey\"}",
                                (Func<TestObject, bool>)(to => to.FirstName == "Ben" && to.LastName == "Ramey")
                            },
                        new object[]
                            {
                                "where={\"age\": {\"$lt\": 90}}",
                                (Func<TestObject, bool>)(to => to.Age < 90)
                            },
                        new object[]
                            {
                                "where={\"age\": {\"$gt\": 80}}",
                                (Func<TestObject, bool>)(to => to.Age > 80)
                            },
                        new object[]
                            {
                                "where={\"age\": {\"$lte\": 90}}",
                                (Func<TestObject, bool>)(to => to.Age <= 90)
                            },
                        new object[]
                            {
                                "where={\"age\": {\"$gte\": 31}}",
                                (Func<TestObject, bool>)(to => to.Age >= 31)
                            },
                        new object[]
                            {
                                "where={\"age\": {\"$ne\": 31}}",
                                (Func<TestObject, bool>)(to => to.Age != 31)
                            },
                        new object[]
                            {
                                "where={\"firstName\": {\"$exists\": true}}",
                                (Func<TestObject, bool>)(to => !string.IsNullOrEmpty(to.FirstName))
                            },
                        new object[]
                            {
                                "where={\"firstName\": {\"$exists\": false}}",
                                (Func<TestObject, bool>)(to => string.IsNullOrEmpty(to.FirstName))
                            },
                        new object[]
                            {
                                "where={\"age\": {\"$in\": [31,92,90,89]}}",
                                (Func<TestObject, bool>)(to => new[] { 31, 92, 90, 89 }.Contains(to.Age))
                            },
                        new object[]
                            {
                                "where={\"age\": {\"$nin\": [31,92,90,89]}}",
                                (Func<TestObject, bool>)(to => !(new[] { 31, 92, 90, 89 }.Contains(to.Age)))
                            },
                    };
            }
        }

        #endregion
    }
}