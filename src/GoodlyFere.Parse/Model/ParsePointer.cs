#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParsePointer.cs">
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
using System.Runtime.Serialization;
using GoodlyFere.Parse.Util;

#endregion

namespace GoodlyFere.Parse.Model
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