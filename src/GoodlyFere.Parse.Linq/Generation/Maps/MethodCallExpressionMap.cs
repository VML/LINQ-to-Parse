#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GoodlyFere.Parse.Linq.Generation.Contraints;
using GoodlyFere.Parse.Linq.Generation.Handlers;

#endregion

namespace GoodlyFere.Parse.Linq.Generation.Maps
{
    internal delegate void MethodCallFactoryMethod(
        List<ConstraintSet> queryProperties, MethodCallExpression expression);

    internal class MethodCallExpressionMap : Dictionary<Type, MethodCallFactoryMethod>
    {
        #region Constructors and Destructors

        public MethodCallExpressionMap()
        {
            Add(typeof(String), MethodCallExpressionHandlers.String);
        }

        #endregion
    }
}