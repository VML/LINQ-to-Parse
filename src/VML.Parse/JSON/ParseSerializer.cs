// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ParseSerializer.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>01/09/2014 5:08 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using Newtonsoft.Json;
using RestSharp.Serializers;

#endregion

namespace VML.Parse.JSON
{
    public class ParseSerializer : ISerializer
    {
        #region Constructors and Destructors

        public ParseSerializer()
        {
            ContentType = "application/json";
        }

        #endregion

        #region Public Properties

        public string ContentType { get; set; }
        public string DateFormat { get; set; }
        public string Namespace { get; set; }
        public string RootElement { get; set; }

        #endregion

        #region Public Methods

        public string Serialize(object obj)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            return JsonConvert.SerializeObject(obj, settings);
        }

        #endregion
    }
}