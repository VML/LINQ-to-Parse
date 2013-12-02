#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParseApiTests.theories.cs">
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
using RestSharp;

#endregion

namespace GoodlyFere.Parse.Linq.Tests.ExecutionTests
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
                                new[]
                                    {
                                        new Parameter
                                            {
                                                Name = "where",
                                                Type = ParameterType.GetOrPost,
                                                Value = "{\"firstName\": \"Ben\"}"
                                            }
                                    },
                                (Func<TestObject, bool>)(to => to.FirstName == "Ben")
                            },
                        new object[]
                            {
                                new[]
                                    {
                                        new Parameter
                                            {
                                                Name = "where",
                                                Type = ParameterType.GetOrPost,
                                                Value = "{\"isOld\": false}"
                                            }
                                    },
                                (Func<TestObject, bool>)(to => !to.IsOld)
                            },
                        new object[]
                            {
                                new[]
                                    {
                                        new Parameter
                                            {
                                                Name = "where",
                                                Type = ParameterType.GetOrPost,
                                                Value = "{\"isOld\": true}"
                                            }
                                    },
                                (Func<TestObject, bool>)(to => to.IsOld)
                            },
                        new object[]
                            {
                                new[]
                                    {
                                        new Parameter
                                            {
                                                Name = "where",
                                                Type = ParameterType.GetOrPost,
                                                Value = "{\"firstName\": \"Ben\", \"lastName\": \"Ramey\"}"
                                            }
                                    },
                                (Func<TestObject, bool>)(to => to.FirstName == "Ben" && to.LastName == "Ramey")
                            },
                        new object[]
                            {
                                new[]
                                    {
                                        new Parameter
                                            {
                                                Name = "where",
                                                Type = ParameterType.GetOrPost,
                                                Value = "{\"age\": {\"$lt\": 90}}"
                                            }
                                    },
                                (Func<TestObject, bool>)(to => to.Age < 90)
                            },
                        new object[]
                            {
                                new[]
                                    {
                                        new Parameter
                                            {
                                                Name = "where",
                                                Type = ParameterType.GetOrPost,
                                                Value = "{\"age\": {\"$gt\": 80}}"
                                            }
                                    },
                                (Func<TestObject, bool>)(to => to.Age > 80)
                            },
                        new object[]
                            {
                                new[]
                                    {
                                        new Parameter
                                            {
                                                Name = "where",
                                                Type = ParameterType.GetOrPost,
                                                Value = "{\"age\": {\"$lte\": 90}}"
                                            }
                                    },
                                (Func<TestObject, bool>)(to => to.Age <= 90)
                            },
                        new object[]
                            {
                                new[]
                                    {
                                        new Parameter
                                            {
                                                Name = "where",
                                                Type = ParameterType.GetOrPost,
                                                Value = "{\"age\": {\"$gte\": 31}}"
                                            }
                                    },
                                (Func<TestObject, bool>)(to => to.Age >=31)
                            },
                        new object[]
                            {
                                new[]
                                    {
                                        new Parameter
                                            {
                                                Name = "where",
                                                Type = ParameterType.GetOrPost,
                                                Value = "{\"age\": {\"$ne\": 31}}"
                                            }
                                    },
                                (Func<TestObject, bool>)(to => to.Age != 31)
                            },
                        new object[]
                            {
                                new[]
                                    {
                                        new Parameter
                                            {
                                                Name = "where",
                                                Type = ParameterType.GetOrPost,
                                                Value = "{\"firstName\": {\"$exists\": true}}"
                                            }
                                    },
                                (Func<TestObject, bool>)(to => !string.IsNullOrEmpty(to.FirstName))
                            },
                        new object[]
                            {
                                new[]
                                    {
                                        new Parameter
                                            {
                                                Name = "where",
                                                Type = ParameterType.GetOrPost,
                                                Value = "{\"firstName\": {\"$exists\": false}}"
                                            }
                                    },
                                (Func<TestObject, bool>)(to => string.IsNullOrEmpty(to.FirstName))
                            },
                        new object[]
                            {
                                new[]
                                    {
                                        new Parameter
                                            {
                                                Name = "where",
                                                Type = ParameterType.GetOrPost,
                                                Value = "{\"age\": {\"$in\": [31,92,90,89]}}"
                                            }
                                    },
                                (Func<TestObject, bool>)(to => new[]{31,92,90,89}.Contains(to.Age))
                            },
                        new object[]
                            {
                                new[]
                                    {
                                        new Parameter
                                            {
                                                Name = "where",
                                                Type = ParameterType.GetOrPost,
                                                Value = "{\"age\": {\"$nin\": [31,92,90,89]}}"
                                            }
                                    },
                                (Func<TestObject, bool>)(to => !(new[]{31,92,90,89}.Contains(to.Age)))
                            },
                    };
            }
        }

        #endregion
    }
}