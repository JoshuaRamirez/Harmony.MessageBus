using System;

namespace Harmony.MessageBus.Core
{
    public interface IEventMessage
    {
        Guid MessageId { get; }
    }
}
