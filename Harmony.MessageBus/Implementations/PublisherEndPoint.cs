using Harmony.MessageBus.Core;

namespace Harmony.MessageBus.Implementations
{
    public abstract class PublisherEndpoint<TEventMessage> : IEventPublisher<TEventMessage> where TEventMessage : IEventMessage
    {
        public event HarmonyEvent<TEventMessage> Published;
        protected void Publish(TEventMessage eventMessage)
        {
            if (Published != null) Published(eventMessage);
        }
    }
}
