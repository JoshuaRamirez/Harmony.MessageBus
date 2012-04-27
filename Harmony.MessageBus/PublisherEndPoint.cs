using Harmony.MessageBus.Core;

namespace Harmony.MessageBus
{
    public class PublisherEndpoint<TEventMessage> : IEventPublisher<TEventMessage> where TEventMessage : IEventMessage
    {
        public event HarmonyEvent<TEventMessage> Published;
        public void OnPublished(TEventMessage eventMessage)
        {
            if (Published != null) Published(eventMessage);
        }
    }
}
