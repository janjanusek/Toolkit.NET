using System;

namespace Toolkit.NET.Extensions.Type
{
    public static class DefaultValue
    {
        /// <summary>
        /// Extension method for Type
        /// Method returns default value in runtime of specified type
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static object GetDefaultValue(this System.Type t)
        {
            return t.IsValueType ? Activator.CreateInstance(t) : null;
        }
    }
}
