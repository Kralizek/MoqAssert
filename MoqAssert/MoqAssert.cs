using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace MoqAssert
{
    public static class MoqAssert
    {
        private static readonly object[] EmptyObjectArray = new object[0];

        public static T That<T>(Func<T, object> action, IResolveConstraint constraint)
        {
            return That(action, constraint, null, EmptyObjectArray);
        }

        public static T That<T>(Func<T, object> action, IResolveConstraint constraint, string message)
        {
            return That(action, constraint, message, EmptyObjectArray);
        }

        public static T That<T>(Func<T, object> action, IResolveConstraint constraint, string message, params object[] args)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return It.Is<T>(item => ValidateItem(action(item), constraint, message, args));
        }

        private static bool ValidateItem<TProperty>(TProperty item, IResolveConstraint constraint, string message = null, params object[] args)
        {
            Assert.That(item, constraint, message, args);
            return true;
        }
    }
}
