using System;

namespace Toolkit.NET.Extensions
{
    public static class Extension
    {
        public static string ReadDescription(System.Type paExtensionClassType)
        {
            return $"Please read description of {paExtensionClassType.Name} extension.";
        }
    }
}
