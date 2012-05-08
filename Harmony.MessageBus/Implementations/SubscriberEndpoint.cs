using System;
using Harmony.MessageBus.Core;

namespace Harmony.MessageBus.Implementations
{
    public abstract class SubscriberEndpoint<TEventMessage> : IEventSubscriber<TEventMessage> where TEventMessage : IEventMessage
    {

        private readonly Guid _subscriberId;

        protected SubscriberEndpoint()
        {
            _subscriberId = Guid.NewGuid();
        }

        internal SubscriberEndpoint(Guid subscriberId)
        {
            _subscriberId = subscriberId;
        }

        public Guid Id
        {
            get { return _subscriberId; }
        }

        public abstract void Handle(TEventMessage eventMessage);
    }

    public class SubscriberEndpoint
    {

    }
}
