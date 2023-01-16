using System.Collections.Concurrent;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Patchable 
{
    internal sealed class PatchableCache
    {
        private static PatchableCache _instance;
        private readonly ConcurrentDictionary<Type, IEnumerable<PropertyInfo>> _entityProperties = new ConcurrentDictionary<Type, IEnumerable<PropertyInfo>>();

        private PatchableCache() { }

        internal static PatchableCache GetOrCreateCache()
        {
            if (_instance is null)
                _instance = new PatchableCache();

            return _instance;
        }

        internal List<PatchablePropInfo> GetEntityProperties<T>() where T : class
        {
            if (_entityProperties.ContainsKey(typeof(T)) == false)
            {
                _entityProperties.TryAdd(typeof(T), typeof(T).GetProperties());
            }

            return _entityProperties[typeof(T)]
                .Select(p => PatchablePropInfo.FromPropertyInfo(p))
                .ToList();
        }
    }
}