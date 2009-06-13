﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QI4N.Framework.Runtime
{
    using System.Reflection;

    public class PropertyHandler : InvocationHandler
    {
        private readonly AbstractProperty property;

        public PropertyHandler( AbstractProperty property )
        {
            this.property = property;
        }

        public object Invoke(object proxy, MethodInfo method, object[] args)
        {
            return method.Invoke(property, args);
        }
    }
}
