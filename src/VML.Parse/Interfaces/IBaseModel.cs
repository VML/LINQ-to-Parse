// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IBaseModel.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>01/23/2014 2:23 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;

#endregion

namespace VML.Parse.Interfaces
{
    public interface IBaseModel
    {
        #region Public Properties

        DateTime CreatedAt { get; set; }
        string ObjectId { get; set; }
        DateTime UpdatedAt { get; set; }

        #endregion
    }
}