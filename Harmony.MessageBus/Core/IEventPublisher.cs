namespace Harmony.MessageBus.Core
{
    public interface IEventPublisher<out TEventMessage> where TEventMessage : IEventMessage
    {
        event HarmonyEvent<TEventMessage> Published;
    }
}