using Newtonsoft.Json;

namespace Tbc.PhysicalPersonsDirectory.Application.Extensions
{
    public static class JsonExtensions
    {
        public static T FromJson<T>(this string strJson)
        {
            return JsonConvert.DeserializeObject<T>(strJson);
        }

        public static string ToJson<T>(this T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}