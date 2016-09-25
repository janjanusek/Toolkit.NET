using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.NET.Mapper
{
    public class MapperException : Exception
    {
        public MapperException(string paMessage, Exception paInnerException):base(paMessage, paInnerException)
        {
            
        }

        public MapperException(string paMessage) : base(paMessage)
        {

        }
    }
}
