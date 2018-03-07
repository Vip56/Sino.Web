using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Sino.Json
{
    public static class JsonExtensions
    {
        /// <summary>
        /// 将实体转换为Json字符串
        /// </summary>
        public static string ToJsonString(this object obj, bool camelCase = false, bool indented = false)
        {
            var options = new JsonSerializerSettings();

            if (camelCase)
            {
                options.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }

            if (indented)
            {
                options.Formatting = Formatting.Indented;
            }

            return JsonConvert.SerializeObject(obj, options)=="null"?"[]":JsonConvert.SerializeObject(obj, options);
        }
    }
}
