using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.NET.Mapper
{
    public class MapperSpectialContract<TMapFrom, TMapTo>
    {
        public Action<TMapFrom, TMapTo> Contract { get; private set; }
        public string MapFromProperty { get; private set; }
        public string MapToProperty { get; private set; }

        public MapperSpectialContract(string paMapFromProperty, string paMapToProperty, Action<TMapFrom, TMapTo> paContract)
        {
            if (string.IsNullOrEmpty(paMapFromProperty) ||
                string.IsNullOrEmpty(paMapToProperty) ||
                paContract == null)
                throw new MapperException("SpecialContract failed because one or more parameters are null or empty.");
            MapFromProperty = paMapFromProperty;
            MapToProperty = paMapToProperty;
            Contract = paContract;
        }
    }
}
