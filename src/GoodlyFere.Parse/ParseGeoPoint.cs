#region Usings

using System;
using System.Linq;
using System.Runtime.Serialization;

#endregion

namespace GoodlyFere.Parse
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
    }
}