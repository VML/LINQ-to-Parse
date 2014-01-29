// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="TestObject.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/02/2013 11:31 AM</created>
//  <updated>01/23/2014 2:33 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Faker;
using Newtonsoft.Json;
using Plant.Core;
using RestSharp;
using VML.Parse.DefaultImplementations;
using VML.Parse.Interfaces;
using VML.Parse.Model;
using VML.Parse.ResultSets;

#endregion

namespace VML.Parse.Linq.Tests.Support
{
    [DataContract]
    public class TestObject : BaseModel, ILocatable<ParseGeoPoint>, IBaseModel
    {
        #region Public Properties

        [DataMember(Name = "age")]
        public int Age { get; set; }

        [DataMember(Name = "anotherName")]
        public string AnotherName { get; set; }

        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "isOld")]
        public bool IsOld { get; set; }

        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        public ParseGeoPoint Location { get; set; }

        [DataMember(Name = "middleName")]
        public string MiddleName { get; set; }

        [DataMember(Name = "test2")]
        public ParsePointer<Test2Object> Test2Pointer { get; set; }

        #endregion

        #region Public Methods

        public static List<T> GetAll<T>() where T : TestObject
        {
            var settings = new AppSettingsParseApiSettingsProvider();
            var client = new RestClient(settings.ApiUrl);
            var request = new RestRequest("classes/" + typeof(T).Name);
            request.AddHeader("X-Parse-Application-Id", settings.ApplicationId);
            request.AddHeader("X-Parse-REST-API-Key", settings.RestApiKey);

            IRestResponse<ParseQueryResults<T>> response = client.Execute<ParseQueryResults<T>>(request);
            return response.Data.Results;
        }

        public string Json()
        {
            return JsonConvert.SerializeObject(this);
        }

        #endregion
    }

    internal class TestObjectBlueprint : IBlueprint
    {
        #region Public Methods

        public void SetupPlant(BasePlant p)
        {
            p.DefinePropertiesOf(
                new TestObject
                    {
                        FirstName = Name.First(),
                        LastName = Name.Last(),
                        Age = new Random().Next(100)
                    });
        }

        #endregion
    }
}