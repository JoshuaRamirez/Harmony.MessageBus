using System;

namespace Tally.Bus.Core
{
    public interface IEventSubscriber<in TEventMessage> where TEventMessage : IEventMessage
    {
        Guid Id { get; }
        void Handle(TEventMessage eventMessage);
    }
}