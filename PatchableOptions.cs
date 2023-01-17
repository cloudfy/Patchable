using System;
using System.Reflection;

namespace Patchable
{
    public delegate void EvaluatePropertyEventHandler(PropertyInfo propertyInfo, object entity, Type entityType);

    public sealed class PatchableOptions
    {
        public PatchableOptions(bool ignoreInvalidProperties)
        {
            IgnoreInvalidProperties = ignoreInvalidProperties;
        }

        public bool IgnoreInvalidProperties { get; }

        public event EvaluatePropertyEventHandler EvaluateProperty;
        internal void OnEvaluateProperty<TEntity>(EvaluatePropertyEventArgs<TEntity> e)
        {
            EvaluateProperty?.Invoke(e.PropertyInfo, e.Entity, typeof(TEntity));
        }
    }
}