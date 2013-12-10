#region Usings

using System;
using System.Linq;
using System.Runtime.Serialization;

#endregion

namespace GoodlyFere.Parse
{
    [DataContract]
    public class ParseUserPointer : BaseModel
    {
        #region Constructors and Destructors

        public ParseUserPointer()
        {
            ParseType = "Pointer";
            ClassName = "_User";
        }

        #endregion

        #region Public Properties

        [DataMember(Name = "className")]
        public string ClassName { get; set; }

        [DataMember(Name = "__type")]
        public string ParseType { get; set; }

        #endregion
    }
}