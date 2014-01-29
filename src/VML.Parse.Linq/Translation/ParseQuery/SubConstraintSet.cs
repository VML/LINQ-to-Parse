// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="SubConstraintSet.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>01/10/2014 9:03 AM</created>
//  <updated>01/23/2014 2:33 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using Newtonsoft.Json;
using VML.Parse.Linq.Translation.ParseQuery.JsonConverters;

#endregion

namespace VML.Parse.Linq.Translation.ParseQuery
{
    [JsonConverter(typeof(SubConstraintSetJsonConverter))]
    internal class SubConstraintSet : ConstraintSet
    {
        #region Constructors and Destructors

        public SubConstraintSet(string key)
            : base(key)
        {
        }

        #endregion
    }
}