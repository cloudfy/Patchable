using System;
using System.Text.Json;

namespace Patchable.Helpers
{
    internal class JsonHelper
    {
        internal static T ToObject<T>(JsonElement element)
        {
            var json = element.GetRawText();
            return (T)JsonSerializer.Deserialize<T>(json);
        }
        internal static object ToObject(JsonElement element, Type returnType)
        {
            var json = element.GetRawText();
            return JsonSerializer.Deserialize(json, returnType);
        }
    }
}