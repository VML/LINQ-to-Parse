// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ParseBasicResponse.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>01/13/2014 10:10 AM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;

#endregion

namespace VML.Parse.ResultSets
{
    public class ParseBasicResponse
    {
        #region Public Properties

        public int Code { get; set; }
        public string Error { get; set; }

        #endregion
    }
}