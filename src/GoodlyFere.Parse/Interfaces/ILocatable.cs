#region Usings

using System;
using System.Linq;
using GoodlyFere.Parse.Model;

#endregion

namespace GoodlyFere.Parse.Interfaces
{
    public interface ILocatable<T>
        where T : ParseGeoPoint
    {
        #region Public Properties

        T Location { get; set; }

        #endregion
    }
}