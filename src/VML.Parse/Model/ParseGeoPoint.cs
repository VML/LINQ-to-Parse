// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ParseGeoPoint.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>01/09/2014 5:08 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;
using System.Runtime.Serialization;

#endregion

namespace VML.Parse.Model
{
    [DataContract]
    public class ParseGeoPoint
    {
        #region Constructors and Destructors

        public ParseGeoPoint()
        {
            ParseType = "GeoPoint";
        }

        #endregion

        #region Public Properties

        [DataMember(Name = "latitude")]
        public double Latitude { get; set; }

        [DataMember(Name = "longitude")]
        public double Longitude { get; set; }

        [DataMember(Name = "__type")]
        public string ParseType { get; set; }

        #endregion

        #region Public Methods

        public bool NearSphere(double latitude, double longitude)
        {
            throw new NotImplementedException("This method facilitates LINQ NearSphere queries only.");
        }

        #endregion
    }
}