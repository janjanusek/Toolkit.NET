using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.NET.Mapper
{
    /// <summary>
    /// This class makes you able to create contract between objects
    /// and map their values to each other
    /// </summary>
    public class Mapper
    {
        private readonly Dictionary<MapperKey, object> _contracts;

        private static Mapper _instance;
        public static Mapper Instance => _instance ?? (_instance = new Mapper());

        private Mapper()
        {
            _contracts = new Dictionary<MapperKey, object>();
        }

        /// <summary>
        /// This method creates contract between types TMapFrom and TMapTo
        /// </summary>
        /// <typeparam name="TMapFrom"></typeparam>
        /// <typeparam name="TMapTo"></typeparam>
        /// <param name="paOwnContracts">This is special contract between two properties and it's override starndart automapping rule</param>
        public void CreateContract<TMapFrom, TMapTo>(params MapperSpectialContract<TMapFrom, TMapTo>[] paOwnContracts)
        {
            if (_contracts.Keys.FirstOrDefault(
                k => k.MapFrom == typeof(TMapFrom) &&
                k.MapTo == typeof(TMapTo)) != null)
                throw new MapperException("This conversion is already defined.");
            var mapperContractor = new MapperContractor<TMapFrom, TMapTo>(paOwnContracts);
            _contracts.Add(new MapperKey(typeof(TMapFrom), typeof(TMapTo)), mapperContractor);
        }

        /// <summary>
        /// This method maps properties from TMapFrom instance to TMapTo instance
        /// before this operation make sure that you created contract between this types
        /// </summary>
        /// <typeparam name="TMapFrom"></typeparam>
        /// <typeparam name="TMapTo"></typeparam>
        /// <param name="paMapFrom"></param>
        /// <param name="paMapTo"></param>
        public void Map<TMapFrom, TMapTo>(ref TMapFrom paMapFrom, ref TMapTo paMapTo)
        {
            MapperKey key;
            var from = paMapFrom;
            var to = paMapTo;
            if ((key = _contracts.Keys.FirstOrDefault(k => k.MapFrom == from.GetType() && k.MapTo == to.GetType())) != null)
            {
                var contractor = _contracts[key] as MapperContractor<TMapFrom, TMapTo>;
                if (contractor != null)
                {
                    contractor.MapTypes(ref paMapFrom, ref paMapTo);
                    return;
                }
            }
            throw new MapperException("Conversion not possible: Make sure that you created this conversion.");
        }
    }
}
