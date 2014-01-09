#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MemberNameFinder.cs">
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
using GoodlyFere.Parse.Util;
using Remotion.Linq.Parsing;

#endregion

namespace GoodlyFere.Parse.Linq.Translation.ExpressionVisitors
{
    internal class MemberNameFinder : ExpressionTreeVisitor
    {
        #region Properties

        protected string MemberName { get; private set; }

        #endregion

        #region Methods

        internal static string Find(Expression expression)
        {
            var visitor = new MemberNameFinder();
            visitor.VisitExpression(expression);

            return visitor.MemberName;
        }

        protected override Expression VisitMemberExpression(MemberExpression expression)
        {
            MemberName = ClassUtils.GetDataMemberPropertyName(expression.Member);
            return expression;
        }

        #endregion
    }
}