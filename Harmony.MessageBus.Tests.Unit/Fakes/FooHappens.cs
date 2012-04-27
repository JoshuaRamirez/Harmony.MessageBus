using System;
using Harmony.MessageBus.Core;

namespace Harmony.MessageBus.Tests.Unit.Fakes
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