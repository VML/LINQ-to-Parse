// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ParseQueryFactory.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/02/2013 9:37 AM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using Remotion.Linq;
using Remotion.Linq.Parsing.Structure;
using VML.Parse.Defaults;
using VML.Parse.Interfaces;

#endregion

namespace VML.Parse.Linq
{
    public class ParseQueryFactory
    {
        #region Public Methods

        public static ParseQueryable<T> Queryable<T>()
        {
            return Queryable<T>((IParseApiSettingsProvider)null);
        }

        public static ParseQueryable<T> Queryable<T>(IParseApiSettingsProvider settingsProvider)
        {
            settingsProvider = settingsProvider ?? new AppSettingsParseApiSettingsProvider();
            return Queryable<T>(CreateParser(), CreateExecutor(settingsProvider));
        }

        public static ParseQueryable<T> Queryable<T>(IQueryExecutor executor)
        {
            return Queryable<T>(CreateParser(), executor);
        }

        public static ParseQueryable<T> Queryable<T>(IQueryParser parser, IQueryExecutor executor)
        {
            return new ParseQueryable<T>(parser, executor);
        }

        #endregion

        #region Methods

        private static IQueryExecutor CreateExecutor(IParseApiSettingsProvider settingsProvider)
        {
            return new ParseQueryExecutor(settingsProvider);
        }

        private static IQueryParser CreateParser()
        {
            return QueryParser.CreateDefault();
        }

        #endregion
    }
}