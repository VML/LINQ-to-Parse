#region Usings

using System.Collections.Generic;
using System.Linq;
using System;
using System.Linq.Expressions;

#endregion

namespace GoodlyFere.Parse.Linq.Generation.Contraints
{
    internal class ConstraintSet
    {
        #region Constructors and Destructors

        public ConstraintSet(string propertyName)
            : this()
        {
            PropertyName = propertyName;
        }

        public ConstraintSet()
        {
            Constraints = new Queue<Constraint>();
        }

        #endregion

        #region Public Properties

        public Queue<Constraint> Constraints { get; set; }
        public string PropertyName { get; set; }
        public ExpressionType Type { get; set; }

        #endregion
    }
}