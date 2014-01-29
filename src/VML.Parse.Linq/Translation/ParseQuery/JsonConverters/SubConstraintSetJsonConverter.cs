// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="SubConstraintSetJsonConverter.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>01/10/2014 9:04 AM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using Newtonsoft.Json;

#endregion

namespace VML.Parse.Linq.Translation.ParseQuery.JsonConverters
{
    internal class SubConstraintSetJsonConverter : ConstraintSetJsonConverter
    {
        #region Public Methods

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ConstraintSetJsonConverter);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            SubConstraintSet set = (SubConstraintSet)value;

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
                writer.WritePropertyName(c.Key);
                serializer.Serialize(writer, c.Value);
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