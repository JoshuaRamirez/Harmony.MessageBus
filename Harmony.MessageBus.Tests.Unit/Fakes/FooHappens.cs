using System;
using Tally.Bus.Core;


namespace Tally.Bus.Tests.Unit.Fakes
{
    public class FooHappens : IEventMessage
    {
        private readonly Guid _messageId;
        public FooHappens(Guid messageId)
        {
            _messageId = messageId;
        }

        public Guid MessageId
        {
            get { return _messageId; }
        }
    }
}