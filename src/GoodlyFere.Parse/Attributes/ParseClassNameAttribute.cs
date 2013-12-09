#region Usings

using System;
using System.Linq;

#endregion

namespace GoodlyFere.Parse.Attributes
{
    public class ParseClassNameAttribute : Attribute
    {
        #region Constructors and Destructors

        public ParseClassNameAttribute(string name)
        {
            Name = name;
        }

        #endregion

        #region Properties

        protected string Name { get; set; }

        #endregion
    }
}