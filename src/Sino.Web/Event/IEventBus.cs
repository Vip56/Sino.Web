using System.Threading.Tasks;

namespace Sino.Event
{
    /// <summary>
    /// 事件总线
    /// </summary>
    public interface IEventBus
    {
        Task PublishAsync<T>(T eventData) where T : class, IEventData;
    }
}
