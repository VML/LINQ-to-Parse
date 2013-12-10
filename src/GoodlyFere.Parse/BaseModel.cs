#region Usings

using System;
using System.Linq;
using System.Runtime.Serialization;

#endregion

namespace GoodlyFere.Parse
{
    [DataContract]
    public abstract class BaseModel
    {
        #region Public Properties

        [DataMember(Name="objectId")]
        public string ObjectId { get; set; }

        #endregion
    }
}