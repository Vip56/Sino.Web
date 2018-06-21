namespace Sino.ViewModels
{
    /// <summary>
    /// 视图输出根类
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// 错误消息
        /// </summary>
        public string errorMessage { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string errorCode { get; set; }

        /// <summary>
        /// 是否请求成功
        /// </summary>
        public bool success { get; set; }

        /// <summary>
        /// 数据对象
        /// </summary>
        public object data { get; set; }
    }
}
