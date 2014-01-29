// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="QueryPieceJsonConverter.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/11/2013 11:51 AM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

#endregion

namespace VML.Parse.Linq.Translation.ParseQuery.JsonConverters
{
    internal class QueryPieceJsonConverter : JsonConverter
    {
        #region Constants and Fields

        private static readonly CamelCasePropertyNamesContractResolver PropNameResolver;

        #endregion

        #region Constructors and Destructors

        static QueryPieceJsonConverter()
        {
            PropNameResolver = new CamelCasePropertyNamesContractResolver();
        }

        #endregion

        #region Public Methods

        public override bool CanConvert(Type objectType)
        {
            return typeof(IQueryPiece).IsAssignableFrom(objectType);
        }

        public override object ReadJson(
            JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            IQueryPiece qp = (IQueryPiece)value;

            bool endObject = false;
            if (writer.WriteState == WriteState.Array)
            {
                endObject = true;
                writer.WriteStartObject();
            }

            writer.WritePropertyName(PropNameResolver.GetResolvedPropertyName(qp.Key));
            serializer.Serialize(writer, qp.Value);

            if (endObject)
            {
                writer.WriteEndObject();
            }
        }

        #endregion
    }
}