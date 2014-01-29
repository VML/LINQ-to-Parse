// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="QueryRootJsonConverter.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/11/2013 11:51 AM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using Newtonsoft.Json;

#endregion

namespace VML.Parse.Linq.Translation.ParseQuery.JsonConverters
{
    internal class QueryRootJsonConverter : JsonConverter
    {
        #region Public Methods

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(QueryRoot);
        }

        public override object ReadJson(
            JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            QueryRoot query = (QueryRoot)value;
            writer.WriteStartObject();
            foreach (var piece in query)
            {
                serializer.Serialize(writer, piece);
            }
            writer.WriteEndObject();
        }

        #endregion
    }
}