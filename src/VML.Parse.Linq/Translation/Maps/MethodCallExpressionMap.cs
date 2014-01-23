// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MethodCallExpressionMap.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/10/2013 4:26 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VML.Parse.Linq.Translation.Handlers;
using VML.Parse.Linq.Translation.ParseQuery;
using VML.Parse.Model;

#endregion

namespace VML.Parse.Linq.Translation.Maps
{
    internal delegate void MethodCallFactoryMethod(
        QueryRoot query, MethodCallExpression expression);

    internal delegate IList<IQueryPiece> MethodCallHandlerFactoryMethod(
        QueryRoot query, MethodCallExpression expression);

    internal class MethodCallExpressionMap : Map<MethodCallExpressionMap, Type, MethodCallFactoryMethod>
    {
        #region Constructors and Destructors

        public MethodCallExpressionMap()
        {
            Add(typeof(String), MethodCallExpressionHandlers.HandleStringMethods);
            Add(typeof(ParseGeoPoint), MethodCallExpressionHandlers.HandleParseGeoPointMethods);
        }

        #endregion
    }
}