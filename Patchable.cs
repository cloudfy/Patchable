﻿using Patchable.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;

namespace Patchable
{
    public sealed class Patchable<TEntity> 
        : IDictionary<string, object> where TEntity : class
    {
        private readonly Dictionary<string, object> _valueDictionary = new Dictionary<string, object>();
        private readonly List<PatchablePropInfo> _entityProperties = new List<PatchablePropInfo>();

        public Patchable()
        {
            var cache = PatchableCache.GetOrCreateCache();
            _entityProperties = cache.GetEntityProperties<TEntity>();
        }

        public void Patch(TEntity entity, PatchableOptions options = null)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            SetPropertiesValue(entity, options);
        }

        private void SetPropertiesValue(TEntity entity, PatchableOptions options = null)
        {
            var patchOptions = options ?? new PatchableOptions(false);

            foreach (var key in _valueDictionary.Keys)
            {
                var property = _entityProperties
                    .FirstOrDefault(p => p.Name.Equals(key, StringComparison.CurrentCultureIgnoreCase));
                if (property is null &&
                    patchOptions.IgnoreInvalidProperties == false)
                {
                    throw new ArgumentException($"{typeof(TEntity).Name} does not have a property {key}.", key);
                }
                else if (property is null && 
                    patchOptions.IgnoreInvalidProperties == true)
                {
                    continue; // ignored
                }

                var value = _valueDictionary[key];

                if (TypeHelper.IsNullable(property.Property) == false &&
                    value is null)
                {
                    throw new ArgumentNullException($"{key} value is null, however property does not allow null.");
                }

                try
                {
                    var jsonElement = (JsonElement)_valueDictionary[key];
                    var valueValue = JsonHelper.ToObject(jsonElement, property.Property.PropertyType);
                    property.Property.SetValue(entity, valueValue);
                }
                catch (JsonException e)
                {
                    throw new FormatException($"Invalid format patching property {key}.", e);
                }
                catch
                {
                    throw;
                }
            }
        }

        #region IDictionary Interface
        public object this[string key] {
            get {
                return _valueDictionary[key];
            }
            set {
                _valueDictionary[key] = value;
            }
        }

        public ICollection<string> Keys => _valueDictionary.Keys;

        public ICollection<object> Values => _valueDictionary.Values;

        public int Count => _valueDictionary.Count;

        public bool IsReadOnly => ((IDictionary<string, object>)_valueDictionary).IsReadOnly;

        public void Add(string key, object value)
            => _valueDictionary.Add(key, value);

        public void Add(KeyValuePair<string, object> item)
            => _valueDictionary.Add(item.Key, item.Value);

        public void Clear()
            => _valueDictionary.Clear();

        public bool Contains(KeyValuePair<string, object> item)
            => _valueDictionary.Contains(item);

        public bool ContainsKey(string key)
            => _valueDictionary.ContainsKey(key);

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            ((IDictionary<string, object>)_valueDictionary).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
            => _valueDictionary.GetEnumerator();

        public bool Remove(string key)
            => _valueDictionary.Remove(key);

        public bool Remove(KeyValuePair<string, object> item)
            => _valueDictionary.Remove(item.Key);

        public bool TryGetValue(string key, [MaybeNullWhen(false)] out object value)
            => _valueDictionary.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator()
            => _valueDictionary.GetEnumerator();
        #endregion
    }
}