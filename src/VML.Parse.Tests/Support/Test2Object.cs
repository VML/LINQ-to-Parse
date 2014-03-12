// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Test2Object.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/02/2013 12:54 PM</created>
//  <updated>01/23/2014 2:33 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using System.Runtime.Serialization;

#endregion

namespace VML.Parse.Tests.Support
{
    [DataContract]
    public class Test2Object : TestObject
    {
        #region Public Properties

        [DataMember(Name = "alias")]
        public string Alias { get; set; }

        #endregion
    }
}