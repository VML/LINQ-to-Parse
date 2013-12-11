#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestObject.cs">
// LINQ-to-Parse, a LINQ interface to the Parse.com REST API.
//  
// Copyright (C) 2013 Benjamin Ramey
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
// 
// http://www.gnu.org/licenses/lgpl-2.1-standalone.html
// 
// You can contact me at ben.ramey@gmail.com.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion

#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Faker;
using GoodlyFere.Parse.DefaultImplementations;
using Newtonsoft.Json;
using Plant.Core;
using RestSharp;

#endregion

namespace GoodlyFere.Parse.Linq.Tests.Support
{
    [DataContract]
    public class TestObject
    {
        #region Public Properties

        [DataMember(Name = "age")]
        public int Age { get; set; }

        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "isOld")]
        public bool IsOld { get; set; }

        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        [DataMember(Name = "middleName")]
        public string MiddleName { get; set; }

        [DataMember(Name = "objectId")]
        public string ObjectId { get; set; }

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