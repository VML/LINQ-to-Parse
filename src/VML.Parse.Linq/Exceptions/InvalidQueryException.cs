// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="InvalidQueryException.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/19/2013 12:22 PM</created>
//  <updated>01/23/2014 2:32 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Linq;

#endregion

namespace VML.Parse.Linq.Exceptions
{
    public class InvalidQueryException : Exception
    {
        #region Constants and Fields

        private readonly string _message;

        #endregion

        #region Constructors and Destructors

        public InvalidQueryException(string message)
        {
            _message = message + " ";
        }

        #endregion

        #region Public Properties

        public override string Message
        {
            get
            {
                return "The client query is invalid: " + _message;
            }
        }

        #endregion
    }
}