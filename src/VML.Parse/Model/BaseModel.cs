// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="BaseModel.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>01/09/2014 5:08 PM</created>
//  <updated>01/24/2014 9:51 AM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using VML.Parse.Interfaces;

#endregion

namespace VML.Parse.Model
{
    [DataContract]
    public abstract class BaseModel : IBaseModel
    {
        #region Public Properties

        [DataMember(Name = "createdAt")]
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "objectId")]
        public string ObjectId { get; set; }

        [DataMember(Name = "updatedAt")]
        [JsonIgnore]
        public DateTime UpdatedAt { get; set; }

        #endregion
    }
}