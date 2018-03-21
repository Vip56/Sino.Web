namespace Sino.ViewModels
{
    /// <summary>
    /// 视图输出根类
    /// </summary>
    public class BaseResponse<T>
    {
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// 是否请求成功
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// 数据对象
        /// </summary>
        public T Data { get; set; }
    }
}
