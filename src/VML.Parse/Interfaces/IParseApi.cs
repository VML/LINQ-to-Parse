#region Usings

using System.Collections.Generic;
using System.Linq;
using System;
using VML.Parse.Model;

#endregion

namespace VML.Parse.Interfaces
{
    public interface IParseApi
    {
        #region Public Methods

        long Count(string queryString, Type objectType);

        T Create<T>(T modelToCreate) where T : IBaseModel, new();

        bool Delete<T>(T modelToDelete) where T : IBaseModel, new();

        ParseUser FacebookSignIn(
            string username, string email, string id, string accessToken, DateTime expirationDate);

        ParseUser GetCurrent();

        ParseUser LinkToFacebook(string userObjectId, FacebookAuthData facebookAuthData);

        IList<T> Query<T>(string queryString);

        bool ResetPassword(string email);

        ParseUser SignIn(string username, string password);

        ParseUser SignUp(ParseUser newUser, string password);

        bool UnlinkFromFacebook(string userObjectId);

        T Update<T>(T modelToSave) where T : IBaseModel, new();

        bool ValidateSession();

        #endregion
    }
}