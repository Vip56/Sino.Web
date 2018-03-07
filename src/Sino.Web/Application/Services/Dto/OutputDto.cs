using Sino.Runtime;
using System.Collections.Generic;

namespace Sino.Application.Services.Dto
{
    public abstract class OutputDto : IOutputDto
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 是否请求成功
        /// </summary>
        public bool Success { get; set; } = true;
    }
}
