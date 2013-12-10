#region Usings

using System;
using System.Linq;
using Newtonsoft.Json;
using RestSharp.Serializers;

#endregion

namespace GoodlyFere.Parse
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