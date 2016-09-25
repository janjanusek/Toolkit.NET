using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.NET.Mapper
{
    public class MapperKey
    {
        public Type MapFrom { get; private set; }
        public Type MapTo { get; private set; }

        public MapperKey(Type paMapFrom, Type paMapTo)
        {
            MapFrom = paMapFrom;
            MapTo = paMapTo;
        }
    }
}
