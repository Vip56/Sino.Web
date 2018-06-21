using System.Collections.Generic;

namespace Sino.ViewModels
{
    public class ListResponse<T>
    {
        /// <summary>
        /// 列表
        /// </summary>
        public List<T> Data { get; set; }

        /// <summary>
        /// 列表总数
        /// </summary>
        public int Total { get; set; }
    }
}
