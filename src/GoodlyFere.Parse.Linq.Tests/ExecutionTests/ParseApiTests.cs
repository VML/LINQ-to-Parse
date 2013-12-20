#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParseApiTests.cs">
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
using GoodlyFere.Parse.DefaultImplementations;
using GoodlyFere.Parse.Linq.Tests.Support;
using Xunit;
using Xunit.Extensions;

#endregion

namespace GoodlyFere.Parse.Linq.Tests.ExecutionTests
{
    public partial class ParseApiTests : BaseTest
    {
        #region Constants and Fields

        private readonly List<Test2Object> _test2Objects;
        private readonly List<TestObject> _testObjects;

        #endregion

        #region Constructors and Destructors

        public ParseApiTests()
        {
            _testObjects = TestObject.GetAll<TestObject>();
            _test2Objects = TestObject.GetAll<Test2Object>();
        }

        #endregion

        #region Public Methods

        [Fact]
        public void Query_InvalidQuery_ThrowsException()
        {
            var parseApi = new ParseApi(new AppSettingsParseApiSettingsProvider());
            IList<TestObject> results = null;

            Assert.Throws<Exception>(() => results = parseApi.Query<TestObject>("where=dkdkdkdkdkd"));
            Assert.Null(results);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(14)]
        [InlineData(0)]
        public void Query_Limit(int amount)
        {
            var parseApi = new ParseApi(new AppSettingsParseApiSettingsProvider());
            var results =
                parseApi.Query<TestObject>("limit=" + amount);
            var expectedResults = _testObjects.Take(amount).ToList();

            Assert.NotNull(results);
            CompareResultSets(expectedResults, results);
        }

        [Fact]
        public void Query_NullParams_DoesNotThrow()
        {
            var parseApi = new ParseApi(new AppSettingsParseApiSettingsProvider());
            Assert.DoesNotThrow(() => parseApi.Query<TestObject>(null));
        }

        [Fact]
        public void Query_OrderAscending()
        {
            var parseApi = new ParseApi(new AppSettingsParseApiSettingsProvider());
            var results =
                parseApi.Query<TestObject>("order=age");
            var expectedResults = _testObjects.OrderBy(to => to.Age).ToList();

            Assert.NotNull(results);
            CompareResultSets(expectedResults, results);
        }

        [Fact]
        public void Query_OrderDescending()
        {
            var parseApi = new ParseApi(new AppSettingsParseApiSettingsProvider());
            var results =
                parseApi.Query<TestObject>("order=-age");
            var expectedResults = _testObjects.OrderByDescending(to => to.Age).ToList();

            Assert.NotNull(results);
            CompareResultSets(expectedResults, results);
        }

        [Fact]
        public void Query_SingleQuotesFails()
        {
            var parseApi = new ParseApi(new AppSettingsParseApiSettingsProvider());
            IList<TestObject> results = null;

            Assert.Throws<Exception>(() => results = parseApi.Query<TestObject>("where={'firstName': 'Ben'}"));
            Assert.Null(results);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(14)]
        [InlineData(0)]
        public void Query_Skip(int amount)
        {
            var parseApi = new ParseApi(new AppSettingsParseApiSettingsProvider());
            var results =
                parseApi.Query<TestObject>("skip=" + amount);
            var expectedResults = _testObjects.Skip(amount).ToList();

            Assert.NotNull(results);
            CompareResultSets(expectedResults, results);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(14, 1)]
        [InlineData(0, 4)]
        public void Query_SkipAndLimit(int skip, int limit)
        {
            var parseApi = new ParseApi(new AppSettingsParseApiSettingsProvider());
            var results =
                parseApi.Query<TestObject>("skip=" + skip + "&limit=" + limit);
            var expectedResults = _testObjects.Skip(skip).Take(limit).ToList();

            Assert.NotNull(results);
            CompareResultSets(expectedResults, results);
        }

        [Fact]
        public void Query_Test2Object_ExecutesQuery()
        {
            TestQuerying(null, _test2Objects, true);
        }

        [Fact]
        public void Query_TestObject_ExecutesQuery()
        {
            TestQuerying(null, _testObjects, true);
        }

        [Theory]
        [PropertyData("WhereClauses")]
        public void Query_WhereClauses_ExecutesQuery(string queryString, Func<TestObject, bool> filter)
        {
            TestQuerying(queryString, _testObjects, filter);
        }

        [Fact]
        public void Update()
        {
            var parseApi = new ParseApi(new AppSettingsParseApiSettingsProvider());
            var obj = _testObjects.First();
            string oldName = obj.FirstName;
            string newName = oldName + oldName;

            obj.FirstName = newName;

            Assert.DoesNotThrow(() => obj = parseApi.Update(obj));
            Assert.Equal(newName, obj.FirstName);
        }

        [Fact]
        public void Update_WithPointer()
        {
            var parseApi = new ParseApi(new AppSettingsParseApiSettingsProvider());
            var obj = _testObjects.First();

            obj.Test2Pointer = new ParsePointer<Test2Object> { ObjectId = _test2Objects.First().ObjectId };

            Assert.DoesNotThrow(() => obj = parseApi.Update(obj));
            Assert.True(obj.CreatedAt > DateTime.MinValue);
            Assert.True(obj.UpdatedAt > DateTime.MinValue);
        }

        #endregion

        #region Methods

        private static void CompareResultSets<T>(IList<T> expectedResults, IList<T> results) where T : TestObject
        {
            for (int i = 0; i < expectedResults.Count; i++)
            {
                Assert.False(string.IsNullOrEmpty(expectedResults[i].ObjectId));
                Assert.False(string.IsNullOrEmpty(results[i].ObjectId));

                Assert.Equal(expectedResults[i].ObjectId, results[i].ObjectId);
            }
        }

        private void TestQuerying<T>(
            string queryString, List<T> expectedResults, bool expectMoreThanZero)
            where T : TestObject
        {
            var parseApi = new ParseApi(new AppSettingsParseApiSettingsProvider());
            IList<T> results = null;

            Assert.DoesNotThrow(() => results = parseApi.Query<T>(queryString));
            Assert.NotNull(results);

            if (expectMoreThanZero)
            {
                Assert.True(results.Count > 0, "Returned no items.");
                Assert.True(expectedResults.Count > 0, "Expected results has no items."); // sanity check
            }

            Assert.Equal(expectedResults.Count, results.Count);

            results = results.OrderBy(er => er.ObjectId).ToList();
            expectedResults = expectedResults.OrderBy(er => er.ObjectId).ToList();
            CompareResultSets(expectedResults, results);
        }

        private void TestQuerying<T>(string queryString, IEnumerable<T> baseSet, Func<T, bool> filter)
            where T : TestObject
        {
            List<T> expectedResults = baseSet.Where(filter).ToList();
            TestQuerying(queryString, expectedResults, false);
        }

        #endregion
    }
}