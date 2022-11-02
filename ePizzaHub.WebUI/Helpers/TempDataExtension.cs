using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json;

namespace ePizzaHub.WebUI.Helpers
{
    public static class TempDataExtension
    {
        public static void Set<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonSerializer.Serialize(value);
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            tempData.TryGetValue(key, out object o);
            return o == null ? null : JsonSerializer.Deserialize<T>((string)o);
        }

        public static T Peek<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object o = tempData.Peek(key);
            return o == null ? null : JsonSerializer.Deserialize<T>((string)o);
        }
    }
}