// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ConstraintSetJsonConverter.cs" company="VML">
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
    internal class ConstraintSetJsonConverter : JsonConverter
    {
        #region Constructors and Destructors

        static ConstraintSetJsonConverter()
        {
            PropNameResolver = new CamelCasePropertyNamesContractResolver();
        }

        #endregion

        #region Properties

        protected static CamelCasePropertyNamesContractResolver PropNameResolver { get; private set; }

        #endregion

        #region Public Methods

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ConstraintSet);
        }

        public override object ReadJson(
            JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            ConstraintSet set = (ConstraintSet)value;

            bool endObject = false;
            if (writer.WriteState == WriteState.Array)
            {
                endObject = true;
                writer.WriteStartObject();
            }

            writer.WritePropertyName(PropNameResolver.GetResolvedPropertyName(set.Key));
            writer.WriteStartObject();
            foreach (var c in set.Operators)
            {
                serializer.Serialize(writer, c);
            }
            writer.WriteEndObject();

            if (endObject)
            {
                writer.WriteEndObject();
            }
        }

        #endregion
    }
}