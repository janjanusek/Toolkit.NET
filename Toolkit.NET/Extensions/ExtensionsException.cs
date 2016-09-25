using System;

namespace Toolkit.NET.Extensions
{
    public class ExtensionsException : Exception
    {
        public ExtensionsException(string paMessace, Exception paException = null) : base(paMessace, paException)
        {
            
        }

        public static ExtensionsException DefaultMessage(Exception paException)
        {
            return new ExtensionsException("Ooops something wrong happend. Look into inner exception.", paException);
        }
    }
}
