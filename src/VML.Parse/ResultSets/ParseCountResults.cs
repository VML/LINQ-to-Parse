// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ParseCountResults.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>01/09/2014 5:08 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System.Linq;
using System;

#endregion

namespace VML.Parse.ResultSets
{
    public class ParseCountResults : ParseBasicResponse
    {
        #region Public Properties

        public long Count { get; set; }

        #endregion
    }
}