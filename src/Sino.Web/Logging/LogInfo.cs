using Newtonsoft.Json;

namespace Sino.Web.Logging
{
    /// <summary>
    /// 标准日志规范格式
    /// </summary>
    public class LogInfo
    {
        /// <summary>
        /// 方法名
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        public object Argument { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string Description { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
