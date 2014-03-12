// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="TranslationVisitorTests.theorydata.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/02/2013 2:42 PM</created>
//  <updated>01/23/2014 2:33 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using VML.Parse.Linq;
using VML.Parse.Tests.Support;

#endregion

namespace VML.Parse.Tests.Integration
{
    public partial class QueryStringGenerationTests
    {
        #region Public Properties

        public static IEnumerable<object[]> CompoundComparisons
        {
            get
            {
                return new[]
                    {
                        new object[]
                            {
                                (from to in ParseQueryFactory.Queryable<TestObject>()
                                 where to.Age > 31 && to.Age < 100
                                 select to),
                                "where={\"age\":{\"$gt\":31,\"$lt\":100}}"
                            },
                        new object[]
                            {
                                (from to in ParseQueryFactory.Queryable<TestObject>()
                                 where to.Age > 31 && to.Age < 100 && to.FirstName == "Ben"
                                 select to),
                                "where={\"age\":{\"$gt\":31,\"$lt\":100},\"firstName\":\"Ben\"}"
                            },
                        new object[]
                            {
                                (from to in ParseQueryFactory.Queryable<TestObject>()
                                 where to.Age < 100 && to.FirstName == "Ben"
                                 select to),
                                "where={\"age\":{\"$lt\":100},\"firstName\":\"Ben\"}"
                            },
                        new object[]
                            {
                                (from to in ParseQueryFactory.Queryable<TestObject>()
                                 where to.Age == 100 && to.FirstName == "Ben"
                                 select to),
                                "where={\"age\":100,\"firstName\":\"Ben\"}"
                            },
                        new object[]
                            {
                                (from to in ParseQueryFactory.Queryable<TestObject>()
                                 where to.Age == 100 || to.FirstName == "Ben"
                                 select to),
                                "where={\"$or\":[{\"age\":100},{\"firstName\":\"Ben\"}]}"
                            },
                        new object[]
                            {
                                (from to in ParseQueryFactory.Queryable<TestObject>()
                                 where to.FirstName.Contains("Ben")
                                 select to),
                                "where={\"firstName\":{\"$regex\":\"Ben\",\"$options\":\"mi\"}}"
                            },
                        new object[]
                            {
                                (from to in ParseQueryFactory.Queryable<TestObject>()
                                 where to.FirstName.Contains("Ben") || to.LastName.Contains("Ramey")
                                 select to),
                                "where={\"$or\":[{\"firstName\":{\"$regex\":\"Ben\",\"$options\":\"mi\"}},{\"lastName\":{\"$regex\":\"Ramey\",\"$options\":\"mi\"}}]}"
                            },
                        new object[]
                            {
                                (from to in ParseQueryFactory.Queryable<TestObject>()
                                 where
                                     to.FirstName.Contains("Ben") || to.LastName.Contains("Ramey")
                                     || to.MiddleName.Contains("Steven")
                                 select to),
                                "where={\"$or\":[{\"firstName\":{\"$regex\":\"Ben\",\"$options\":\"mi\"}},{\"lastName\":{\"$regex\":\"Ramey\",\"$options\":\"mi\"}},{\"middleName\":{\"$regex\":\"Steven\",\"$options\":\"mi\"}}]}"
                            },
                        new object[]
                            {
                                (from to in ParseQueryFactory.Queryable<TestObject>()
                                 where to.FirstName.Contains("Ben")
                                       || to.LastName.Contains("Ramey")
                                       || to.MiddleName.Contains("Steven")
                                       || to.AnotherName.Contains("Bones")
                                 select to),
                                "where={\"$or\":[{\"firstName\":{\"$regex\":\"Ben\",\"$options\":\"mi\"}},{\"lastName\":{\"$regex\":\"Ramey\",\"$options\":\"mi\"}},{\"middleName\":{\"$regex\":\"Steven\",\"$options\":\"mi\"}},{\"anotherName\":{\"$regex\":\"Bones\",\"$options\":\"mi\"}}]}"
                            },
                        new object[]
                            {
                                (from to in ParseQueryFactory.Queryable<TestObject>()
                                 where new[] { 1, 2, 92 }.Contains(to.Age)
                                 select to),
                                "where={\"age\":{\"$in\":[1,2,92]}}"
                            },
                        new object[]
                            {
                                (from to in ParseQueryFactory.Queryable<TestObject>()
                                 where to.Test2Pointer.ObjectId == "dkdfa923"
                                 select to),
                                "where={\"test2\":{\"__type\":\"Pointer\",\"className\":\"Test2Object\",\"objectId\":\"dkdfa923\"}}"
                            },
                        new object[]
                            {
                                (from to in ParseQueryFactory.Queryable<TestObject>()
                                 where to.Location.NearSphere(30.0, -20.0)
                                 select to),
                                "where={\"location\":{\"$nearSphere\":{\"__type\":\"GeoPoint\",\"latitude\":30.0,\"longitude\":-20.0}}}"
                            },
                    };
            }
        }

        public static IEnumerable<object[]> ResultOperators
        {
            get
            {
                return new[]
                    {
                        new object[]
                            {
                                ParseQueryFactory.Queryable<TestObject>().Skip(10),
                                "skip=10"
                            },
                        new object[]
                            {
                                ParseQueryFactory.Queryable<TestObject>().Take(10),
                                "limit=10"
                            },
                        new object[]
                            {
                                ParseQueryFactory.Queryable<TestObject>().Skip(10).Take(10),
                                "skip=10&limit=10"
                            },
                        new object[]
                            {
                                ParseQueryFactory.Queryable<TestObject>().OrderBy(t => t.FirstName),
                                "order=firstName"
                            },
                        new object[]
                            {
                                ParseQueryFactory.Queryable<TestObject>().OrderByDescending(t => t.FirstName),
                                "order=-firstName"
                            },
                    };
            }
        }

        public static IEnumerable<object[]> SimpleComparisons
        {
            get
            {
                return new[]
                    {
                        new object[]
                            {
                                (from to in ParseQueryFactory.Queryable<TestObject>()
                                 where to.Age == 31
                                 select to),
                                "where={\"age\":31}"
                            },
                        new object[]
                            {
                                (from to in ParseQueryFactory.Queryable<TestObject>()
                                 where 31 == to.Age
                                 select to),
                                "where={\"age\":31}"
                            },
                        new object[]
                            {
                                (from to in ParseQueryFactory.Queryable<TestObject>()
                                 where to.Age != 31
                                 select to),
                                "where={\"age\":{\"$ne\":31}}"
                            },
                        new object[]
                            {
                                (from to in ParseQueryFactory.Queryable<TestObject>()
                                 where to.Age > 31
                                 select to),
                                "where={\"age\":{\"$gt\":31}}"
                            },
                        new object[]
                            {
                                (from to in ParseQueryFactory.Queryable<TestObject>()
                                 where to.Age < 31
                                 select to),
                                "where={\"age\":{\"$lt\":31}}"
                            },
                        new object[]
                            {
                                (from to in ParseQueryFactory.Queryable<TestObject>()
                                 where to.Age <= 31
                                 select to),
                                "where={\"age\":{\"$lte\":31}}"
                            },
                        new object[]
                            {
                                (from to in ParseQueryFactory.Queryable<TestObject>()
                                 where to.Age >= 31
                                 select to),
                                "where={\"age\":{\"$gte\":31}}"
                            },
                    };
            }
        }

        #endregion
    }
}