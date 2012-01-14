using System;


namespace Tally.Bus.Contracts
{
    public interface IMessage
    {
        Guid MessageId { get; }
    }
}
