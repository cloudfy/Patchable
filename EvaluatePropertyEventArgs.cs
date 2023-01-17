using System;
using System.Reflection;
using Patchable.Internals;

namespace Patchable
{
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