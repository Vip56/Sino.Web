using System.Threading.Tasks;

namespace Sino.Event
{
    /// <summary>
    /// 空对象模式
    /// </summary>
    public sealed class NullEventBus : IEventBus
    {
        public static NullEventBus Instance { get { return SingletonInstance; } }
        private static readonly NullEventBus SingletonInstance = new NullEventBus();

        private NullEventBus() { }

        public Task PublishAsync<T>(T eventData) where T : class, IEventData
        {
            return Task.Delay(1);
        }
    }
}
