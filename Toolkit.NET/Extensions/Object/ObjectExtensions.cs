using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.NET.Extensions.Object
{
    public static class ObjectExtensions
    {
        public static TType Cast<TType>(this object paObject) => (TType) paObject;
        public static TType CastSafe<TType>(this object paObject) where TType : class => paObject as TType;
    }
}
