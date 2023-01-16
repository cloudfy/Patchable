namespace Patchable
{
    public sealed class PatchableOptions
    {
        public PatchableOptions(bool ignoreInvalidProperties)
        {
            IgnoreInvalidProperties = ignoreInvalidProperties;
        }
        public bool IgnoreInvalidProperties { get; }
    }
}