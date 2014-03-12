// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="LogExtensions.cs" company="VML">
//   Copyright VML 2014. All rights reserved.
//  </copyright>
//  <created>12/02/2013 9:47 AM</created>
//  <updated>01/23/2014 2:33 PM by Ben Ramey</updated>
// --------------------------------------------------------------------------------------------------------------------

#region Usings

using System;
using System.Collections.Concurrent;
using System.Linq;
using LoggingExtensions.Logging;

#endregion

namespace VML.Parse.Tests
{
    /// <summary>
    ///     Extensions to help make logging awesome - this should be installed into the root namespace of your application
    /// </summary>
    public static class LogExtensions
    {
        #region Constants and Fields

        /// <summary>
        ///     Concurrent dictionary that ensures only one instance of a logger for a type.
        /// </summary>
        private static readonly ConcurrentDictionary<string, ILog> _dictionary =
            new ConcurrentDictionary<string, ILog>();

        #endregion

        #region Public Methods

        /// <summary>
        ///     Gets the logger for <see cref="T" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">The type to get the logger for.</param>
        /// <returns>Instance of a logger for the object.</returns>
        public static ILog Log<T>(this T type)
        {
            string objectName = typeof(T).FullName;
            return Log(objectName);
        }

        /// <summary>
        ///     Gets the logger for the specified object name.
        /// </summary>
        /// <param name="objectName">Either use the fully qualified object name or the short. If used with Log&lt;T&gt;() you must use the fully qualified object name"/></param>
        /// <returns>Instance of a logger for the object.</returns>
        public static ILog Log(this string objectName)
        {
            return _dictionary.GetOrAdd(objectName, LoggingExtensions.Logging.Log.GetLoggerFor);
        }

        #endregion
    }
}