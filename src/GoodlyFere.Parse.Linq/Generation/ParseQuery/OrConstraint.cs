#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrConstraint.cs">
// LINQ-to-Parse, a LINQ interface to the Parse.com REST API.
//  
// Copyright (C) 2013 Benjamin Ramey
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
// 
// http://www.gnu.org/licenses/lgpl-2.1-standalone.html
// 
// You can contact me at ben.ramey@gmail.com.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion

#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using GoodlyFere.Parse.Linq.Generation.ParseQuery.JsonConverters;
using Newtonsoft.Json;

#endregion

namespace GoodlyFere.Parse.Linq.Generation.ParseQuery
{
    [JsonConverter(typeof(QueryPieceJsonConverter))]
    internal class OrConstraint : IQueryPiece
    {
        #region Constants and Fields

        private IList<IQueryPiece> _operands;

        #endregion

        #region Constructors and Destructors

        public OrConstraint()
        {
            Key = "$or";
            Operands = new List<IQueryPiece>();
        }

        #endregion

        #region Public Properties

        public string Key { get; private set; }

        public IList<IQueryPiece> Operands
        {
            get
            {
                return _operands;
            }
            private set
            {
                _operands = value;
                Value = value;
            }
        }

        public object Value { get; private set; }

        #endregion
    }
}