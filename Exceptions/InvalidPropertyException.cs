using System;

namespace Patchable
{
    public class InvalidPropertyException : Exception
    {
        internal InvalidPropertyException(Type type, string key)
            : base($"{type.Name} does not have a property {key}.")
        {
            EntityType = type;
            PropertyName = key;
        }

        public Type EntityType { get; }
        public string PropertyName { get; }
    }
}
