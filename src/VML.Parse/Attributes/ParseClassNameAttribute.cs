// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ParseClassNameAttribute.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/09/2013 2:17 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;

#endregion

namespace VML.Parse.Attributes
{
    public class ParseClassNameAttribute : Attribute
    {
        #region Constructors and Destructors

        public ParseClassNameAttribute(string name)
        {
            Name = name;
        }

        #endregion

        #region Properties

        protected string Name { get; set; }

        #endregion
    }
}