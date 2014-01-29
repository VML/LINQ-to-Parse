// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ILocatable.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>01/23/2014 2:23 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using VML.Parse.Model;

#endregion

namespace VML.Parse.Interfaces
{
    public interface ILocatable<T>
        where T : ParseGeoPoint
    {
        #region Public Properties

        T Location { get; set; }

        #endregion
    }
}