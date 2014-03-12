// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="TranslationVisitorTests.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/02/2013 2:42 PM</created>
//  <updated>01/23/2014 2:33 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using NSubstitute;
using Remotion.Linq;
using Remotion.Linq.Parsing.Structure;
using VML.Parse.Linq;
using VML.Parse.Linq.Transformation;
using VML.Parse.Linq.Translation;
using VML.Parse.Tests.Support;
using Xunit;
using Xunit.Extensions;

#endregion

namespace VML.Parse.Tests.Integration
{
    public partial class QueryStringGenerationTests
    {
        #region Public Methods

        [Fact]
        public void Count()
        {
            var mockExecutor = Substitute.For<IQueryExecutor>();
            QueryModel queryModel = null;
            mockExecutor.When(me => me.ExecuteScalar<Int32>(Arg.Any<QueryModel>()))
                        .Do(ci => queryModel = (QueryModel)ci.Args()[0]);

            int count = ParseQueryFactory.Queryable<TestObject>(mockExecutor).Count();

            DoTranslation(queryModel, "limit=0&count=1");
        }

        [Theory]
        [PropertyData("ResultOperators")]
        public void ResultOperators_TranslatesProperly(
            IQueryable<TestObject> query, string expectedTranslation)
        {
            DoTranslation(query, expectedTranslation);
        }

        [Theory]
        [PropertyData("CompoundComparisons")]
        public void WhereClauses_CompoundComparisons_TranslatesProperly(
            IQueryable<TestObject> query, string expectedTranslation)
        {
            DoTranslation(query, expectedTranslation);
        }

        [Theory]
        [PropertyData("SimpleComparisons")]
        public void WhereClauses_SimpleComparisons_TranslatesProperly(
            IQueryable<TestObject> query, string expectedTranslation)
        {
            DoTranslation(query, expectedTranslation);
        }

        #endregion

        #region Methods

        private static void DoTranslation(IQueryable<TestObject> query, string expectedTranslation)
        {
            QueryModel queryModel = QueryParser.CreateDefault().GetParsedQuery(query.Expression);
            queryModel = TransformationVisitor.Transform(queryModel);
            string translation = TranslationVisitor.Translate(queryModel);

            Assert.Equal(expectedTranslation, translation);
        }

        private static void DoTranslation(QueryModel queryModel, string expectedTranslation)
        {
            queryModel = TransformationVisitor.Transform(queryModel);
            string translation = TranslationVisitor.Translate(queryModel);

            Assert.Equal(expectedTranslation, translation);
        }

        #endregion
    }
}