#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClassUtils.cs">
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
using System.Reflection;
using System.Runtime.Serialization;
using GoodlyFere.Parse.Attributes;

#endregion

namespace GoodlyFere.Parse
{
    public static class ClassUtils
    {
        #region Public Methods

        public static string GetParseClassName<T>()
        {
            Type type = typeof(T);
            CustomAttributeData attr =
                type.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(ParseClassNameAttribute));

            return attr != null ? (string)attr.ConstructorArguments[0].Value : type.Name;
        }

        public static string GetDataMemberPropertyName(MemberInfo member)
        {
            CustomAttributeData attr =
                member.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(DataMemberAttribute));

            return attr != null ? (string)attr.NamedArguments.First(na => na.MemberName == "Name").TypedValue.Value : member.Name;
        }

        #endregion
    }
}