using System;

namespace Harmony.MessageBus.Core
{
    public interface IEventSubscriber<in TEventMessage> where TEventMessage : IEventMessage
    {
        Guid Id { get; }
        void Handle(TEventMessage eventMessage);
    }
}