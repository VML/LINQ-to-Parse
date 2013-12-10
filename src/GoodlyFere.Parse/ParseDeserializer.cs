#region Usings

using System;
using System.Linq;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;

#endregion

namespace GoodlyFere.Parse
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
            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        #endregion
    }
}