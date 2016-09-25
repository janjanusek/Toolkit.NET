using System;
using System.Linq;

namespace Toolkit.NET.Extensions.String
{
    public static class Format
    {
        /// <summary>
        /// Extension method. Use it as string.Format but choose first delimiting string so instead of {0} use for example [0]
        /// </summary>
        /// <param name="paStringInstance"></param>
        /// <param name="paFirstDelimitingString"></param>
        /// <param name="paValues"></param>
        /// <returns></returns>
        public static string FormatString(this string paStringInstance, string paFirstDelimitingString, params object[] paValues)
        {
            try
            {
                return FormatExtension(paStringInstance, paFirstDelimitingString, paValues);
            }
            catch (Exception exception)
            {
                throw ExtensionsException.DefaultMessage(exception);
            };
        }

        private static string FormatExtension(this string paStringInstance, string paFirstDelimitingString, params object[] paValues)
        {
            var delimiter = paFirstDelimitingString.Split('0');
            if (!delimiter.Any())
                throw new Exception($"{nameof(paStringInstance)} is in wrong format. {Extension.ReadDescription(typeof(Format))}");
            var leftSideString = delimiter.First();
            var rightSideString = delimiter.Last();
            var copyOfStringInstance = string.Copy(paStringInstance);
            for (int i = 0; i < paValues.Length; i++)
            {
                copyOfStringInstance = copyOfStringInstance.Replace($"{leftSideString}{i}{rightSideString}",
                    paValues[i].ToString());
            }
            return copyOfStringInstance; 
        }
    }
}
