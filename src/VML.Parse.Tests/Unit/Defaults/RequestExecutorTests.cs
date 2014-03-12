#region Usings

using System;
using System.Linq;
using NSubstitute;
using RestSharp;
using VML.Parse.Defaults;
using VML.Parse.Model;
using Xunit;

#endregion

namespace VML.Parse.Tests.Unit.Defaults
{
    public class RequestExecutorTests
    {
        #region Constants and Fields

        private readonly RequestExecutor _executor;
        private readonly RestClient _restClientMock;

        #endregion

        #region Constructors and Destructors

        public RequestExecutorTests()
        {
            _restClientMock = Substitute.For<RestClient>();
            _executor = new RequestExecutor(_restClientMock, null);
        }

        #endregion

        #region Public Methods

        [Fact]
        public void ExecuteRequest_CallsClientExecute()
        {
            var request = new RestRequest();

            _executor.ExecuteUserRequest(request);

            _restClientMock.Received(1).Execute<ParseUser>(request);
        }

        #endregion
    }
}