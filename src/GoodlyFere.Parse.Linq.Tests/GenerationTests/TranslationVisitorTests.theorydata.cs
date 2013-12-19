#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TranslationVisitorTests.theorydata.cs">
// LINQ-to-Parse, a LINQ interface to the Parse.com REST API.
//  
// Copyright (C) 2013 Benjamin Ramey
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
// 
// http://www.gnu.org/licenses/lgpl-2.1-standalone.html
// 
// You can contact me at ben.ramey@gmail.com.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion

#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using GoodlyFere.Parse.Linq.Tests.Support;

#endregion

namespace GoodlyFere.Parse.Linq.Tests.GenerationTests
{
    public partial class TranslationVisitorTests
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