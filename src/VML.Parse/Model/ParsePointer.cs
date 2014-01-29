// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ParsePointer.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>01/09/2014 5:08 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using System.Runtime.Serialization;
using VML.Parse.Util;

#endregion

namespace VML.Parse.Model
{
    [DataContract]
    public class ParsePointer<T> : BaseModel
    {
        #region Constructors and Destructors

        public ParsePointer()
            : this(ClassUtils.GetParseClassName<T>())
        {
            ParseType = "Pointer";
        }

        public ParsePointer(string className)
        {
            ClassName = className;
            ParseType = "Pointer";
        }

        #endregion

        #region Public Properties

        [DataMember(Name = "className")]
        public string ClassName { get; set; }

        [DataMember(Name = "__type")]
        public string ParseType { get; set; }

        #endregion
    }
}