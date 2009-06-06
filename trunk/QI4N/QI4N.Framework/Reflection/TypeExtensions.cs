﻿namespace QI4N.Framework.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;

    public static class TypeExtensions
    {
        public static IEnumerable<Type> GetAllInterfaces(this Type targetType)
        {

            if (targetType.IsInterface)
                yield return targetType;

            foreach (Type type in targetType.GetInterfaces())
            {
                yield return type;
            }
        }

        public static PropertyInfo GetInterfaceProperty(this Type interfaceType, string propertyName)
        {
            PropertyInfo propertyInfo = (
                                                from i in interfaceType.GetAllInterfaces()
                                                select i.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Public)
                                        ).FirstOrDefault();

            return propertyInfo;
        }

        public static string GetTypeName(this Type type)
        {
            if (type.IsGenericType)
            {

                var argNames = (from argType in type.GetGenericArguments()
                                select GetTypeName(argType)).ToArray();

                string args = string.Join(",", argNames);

                string typeName = type.Name;
                int index = typeName.IndexOf("`");
                typeName = typeName.Substring(0, index);

                return string.Format("{0}[of {1}]", typeName, args);
            }
            return type.Name;
        }

        public static IEnumerable<MethodInfo> GetAllMethods(this Type type)
        {
            const BindingFlags flags = BindingFlags.Instance |
                                       BindingFlags.Public |
                                       BindingFlags.NonPublic;

            MethodInfo[] ownMethods = type.GetMethods(flags)
                    .ToArray();

            foreach (MethodInfo method in ownMethods)
            {
                yield return method;
            }
        }

        public static MethodBuilder GetMethodOverrideBuilder(this TypeBuilder typeBuilder, MethodInfo method)
        {
            const MethodAttributes methodAttributes = MethodAttributes.NewSlot |
                                                      MethodAttributes.Private |
                                                      MethodAttributes.Virtual |
                                                      MethodAttributes.Final |
                                                      MethodAttributes.HideBySig;

            string methodName = string.Format("{1} in {0}", method.DeclaringType.Name, method.Name);
            Type[] parameters = method
                    .GetParameters()
                    .Select(p => p.ParameterType)
                    .ToArray();

            MethodBuilder methodBuilder = typeBuilder
                    .DefineMethod(methodName, methodAttributes, CallingConventions.Standard, method.ReturnType, parameters);

            typeBuilder.DefineMethodOverride(methodBuilder, method);
            return methodBuilder;
        }
    }
}