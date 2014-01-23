// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ParseQueryResults.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>01/09/2014 5:08 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System.Collections.Generic;
using System.Linq;
using System;

#endregion

namespace VML.Parse.ResultSets
{
    public class ParseQueryResults<T> : ParseBasicResponse
    {
        #region Public Properties

        public List<T> Results { get; set; }

        #endregion
    }
}