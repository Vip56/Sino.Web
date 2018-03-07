using System;

namespace Sino.Event
{
    /// <summary>
    /// 事件数据基础接口
    /// </summary>
    public interface IEventData
    {
        DateTime EventTime { get; set; }
    }
}
