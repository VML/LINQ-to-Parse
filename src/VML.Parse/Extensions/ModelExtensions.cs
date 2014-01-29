// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ModelExtensions.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>01/09/2014 5:06 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using VML.Parse.Interfaces;

#endregion

namespace VML.Parse.Extensions
{
    public static class ModelExtensions
    {
        #region Public Methods

        public static T Create<T>(this T model) where T : IBaseModel, new()
        {
            return ParseContext.API.Create(model);
        }

        public static bool Delete<T>(this T model) where T : IBaseModel, new()
        {
            return ParseContext.API.Delete(model);
        }

        public static T Update<T>(this T model) where T : IBaseModel, new()
        {
            return ParseContext.API.Update(model);
        }

        #endregion
    }
}