using System.Reflection;

namespace Patchable
{
    internal sealed class PatchablePropInfo
    {
        private readonly PropertyInfo _propertyInfo;
        private readonly string _name;

        private PatchablePropInfo(string name, PropertyInfo p)
        {
            _propertyInfo = p;
            _name = name;
        }
        internal static PatchablePropInfo FromPropertyInfo(PropertyInfo p)
        {
            return new PatchablePropInfo(p.Name, p);
        }

        internal bool IgnoreProperty => false;
        internal PropertyInfo Property => _propertyInfo;
        internal string Name => _name;
    }
}