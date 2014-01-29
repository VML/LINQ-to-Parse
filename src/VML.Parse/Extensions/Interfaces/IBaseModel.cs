using System.Linq;
using System;

namespace GoodlyFere.Parse.Interfaces
{
    public interface IBaseModel
    {
        #region Public Properties

        DateTime CreatedAt { get; set; }
        string ObjectId { get; set; }
        DateTime UpdatedAt { get; set; }

        #endregion
    }
}