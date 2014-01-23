// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="BaseModel.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>01/09/2014 5:08 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;

#endregion

namespace VML.Parse.Model
{
    [DataContract]
    public abstract class BaseModel
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