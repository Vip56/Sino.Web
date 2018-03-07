using System;

namespace Sino.Event
{
    /// <summary>
    /// 事件数据基础类
    /// </summary>
    public abstract class EventData : IEventData
    {
        public DateTime EventTime { get; set; }

        public EventData()
        {
            EventTime = DateTime.Now;
        }
    }
}