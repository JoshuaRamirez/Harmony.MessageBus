namespace Tally.Bus.Core
{
    public interface IEventPublisher<out TEventMessage> where TEventMessage : IEventMessage
    {
        event HarmonyEvent<TEventMessage> Published;
    }
}