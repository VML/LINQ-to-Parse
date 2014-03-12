#region Usings

using System.Linq;
using System;
using System.Net;
using RestSharp;
using VML.Parse.Model;

#endregion

namespace VML.Parse.Interfaces
{
    public interface IRequestExecutor
    {
        #region Public Methods

        IRestResponse<T> ExecuteRequest<T>(IRestRequest request, params HttpStatusCode[] acceptableStatusCodes)
            where T : new();

        ParseUser ExecuteUserRequest(IRestRequest request);

        #endregion
    }
}