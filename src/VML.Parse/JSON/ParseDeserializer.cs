// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ParseDeserializer.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>01/09/2014 5:08 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;
using RestSharp.Deserializers;

#endregion

namespace VML.Parse.JSON
{
    public class ParseDeserializer : IDeserializer
    {
        #region Public Properties

        public string DateFormat { get; set; }
        public string Namespace { get; set; }
        public string RootElement { get; set; }

        #endregion

        #region Public Methods

        public T Deserialize<T>(IRestResponse response)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            return JsonConvert.DeserializeObject<T>(response.Content, settings);
        }

        #endregion
    }
}