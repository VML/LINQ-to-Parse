#region Usings

using System;
using System.Linq;

#endregion

namespace GoodlyFere.Parse
{
    public static class ModelExtensions
    {
        #region Public Methods

        public static T Update<T>(this T model) where T : BaseModel, new()
        {
            return ParseContext.API.Update(model);
        }

        #endregion
    }
}