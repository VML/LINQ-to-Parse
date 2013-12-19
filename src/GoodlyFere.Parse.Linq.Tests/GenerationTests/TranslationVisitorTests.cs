#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TranslationVisitorTests.cs">
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
using System.Linq;
using System.Linq.Expressions;
using GoodlyFere.Parse.Linq.Tests.Support;
using GoodlyFere.Parse.Linq.Transformation;
using GoodlyFere.Parse.Linq.Translation;
using Remotion.Linq;
using Remotion.Linq.Parsing.Structure;
using Xunit;
using Xunit.Extensions;

#endregion

namespace GoodlyFere.Parse.Linq.Tests.GenerationTests
{
    public partial class TranslationVisitorTests
    {
        #region Public Methods

        [Fact]
        public void Count()
        {
            int count = ParseQueryFactory.Queryable<TestObject>().Count();
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