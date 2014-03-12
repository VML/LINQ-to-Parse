#region Usings

using System;
using System.Linq;
using RestSharp;
using VML.Parse.JSON;

#endregion

namespace VML.Parse.Util
{
    internal static class RequestBuilder
    {
        #region Methods

        internal static RestRequest BuildDefaultRequest(string uri)
        {
            RestRequest request = new RestRequest(uri)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new ParseSerializer()
                };
            return request;
        }

        #endregion
    }
}