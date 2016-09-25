using System;

namespace Toolkit.NET.Extensions
{
    public static class Extension
    {
        public static string ReadDescription(Type paExtensionClassType)
        {
            return $"Please read description of {paExtensionClassType.Name} extension.";
        }
    }
}
