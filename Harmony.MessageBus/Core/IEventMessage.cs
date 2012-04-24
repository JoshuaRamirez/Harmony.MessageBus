using System;

namespace Tally.Bus.Core
{
    public interface IEventMessage
    {
        Guid MessageId { get; }
    }
}
