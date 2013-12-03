#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WherePredicateVisitor.cs">
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

using System.Collections.Generic;
using System.Linq;
using System;
using System.Linq.Expressions;
using GoodlyFere.Parse.Linq.Generation.Maps;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Remotion.Linq.Clauses.ExpressionTreeVisitors;
using Remotion.Linq.Parsing;

#endregion

namespace GoodlyFere.Parse.Linq.Generation.ExpressionVisitors
{
    public class WherePredicateVisitor : ThrowingExpressionTreeVisitor
    {
        #region Constants and Fields

        private static readonly BinaryExpressionMap _binaryExpressionMap;
        private string _currentKey;
        private object _currentValue;

        #endregion

        #region Constructors and Destructors

        static WherePredicateVisitor()
        {
            _binaryExpressionMap = new BinaryExpressionMap();
        }

        public WherePredicateVisitor()
        {
            Query = new Dictionary<string, object>();
        }

        #endregion

        #region Properties

        protected Dictionary<string, object> Query { get; private set; }

        #endregion

        #region Public Methods

        public static string Translate(Expression predicate)
        {
            var visitor = new WherePredicateVisitor();
            visitor.VisitExpression(predicate);

            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return JsonConvert.SerializeObject(visitor.Query, settings);
        }

        #endregion

        #region Methods

        protected override Exception CreateUnhandledItemException<T>(T unhandledItem, string visitMethod)
        {
            var itemAsExpression = unhandledItem as Expression;
            string formatted = itemAsExpression == null
                                   ? unhandledItem.ToString()
                                   : FormattingExpressionTreeVisitor.Format(itemAsExpression);

            this.Log().Error(string.Format("Unhandled expression: {0}"), formatted);
            return new Exception("I can't handle it! Expression type: " + typeof(T).Name);
        }

        protected override Expression VisitBinaryExpression(BinaryExpression expression)
        {
            if (_binaryExpressionMap.ContainsKey(expression.NodeType))
            {
                VisitExpression(expression.Left);
                VisitExpression(expression.Right);

                _binaryExpressionMap[expression.NodeType](Query, _currentKey, _currentValue);
                return expression;
            }

            return base.VisitBinaryExpression(expression);
        }

        protected override Expression VisitConstantExpression(ConstantExpression expression)
        {
            _currentValue = expression.Value;
            return expression;
        }

        protected override Expression VisitMemberExpression(MemberExpression expression)
        {
            _currentKey = expression.Member.Name;

            if (!Query.ContainsKey(_currentKey))
            {
                Query.Add(_currentKey, null);
            }

            return expression;
        }

        #endregion
    }
}