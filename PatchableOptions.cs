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

    public class EvaluatePropertyEventArgs<TEntity> : EventArgs
    {
        private PatchablePropInfo _property;
        private TEntity _entity;

        internal EvaluatePropertyEventArgs(PatchablePropInfo property, TEntity entity)
        {
            _property = property;
            _entity = entity;
        }

        public bool IgnoreProperty { get; set; }
        public PropertyInfo PropertyInfo => _property.Property;
        public TEntity Entity => _entity;
    }
}