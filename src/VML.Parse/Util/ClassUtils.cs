// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ClassUtils.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>01/09/2014 5:08 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using VML.Parse.Attributes;

#endregion

namespace VML.Parse.Util
{
    public static class ClassUtils
    {
        #region Public Methods

        public static string GetDataMemberPropertyName(MemberInfo member)
        {
            CustomAttributeData attr =
                member.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(DataMemberAttribute));

            return attr != null
                       ? (string)attr.NamedArguments.First(na => na.MemberName == "Name").TypedValue.Value
                       : member.Name;
        }

        public static string GetParseClassName<T>()
        {
            return GetParseClassName(typeof(T));
        }

        public static string GetParseClassName(Type objectType)
        {
            CustomAttributeData attr =
                objectType.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(ParseClassNameAttribute));

            return attr != null ? (string)attr.ConstructorArguments[0].Value : objectType.Name;
        }

        #endregion
    }
}