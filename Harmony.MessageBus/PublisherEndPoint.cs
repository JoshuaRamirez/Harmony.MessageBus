using Tally.Bus.Core;

namespace Tally.Bus
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
