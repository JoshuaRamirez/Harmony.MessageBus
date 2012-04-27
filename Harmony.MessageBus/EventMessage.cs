using System;
using Tally.Bus.Core;

namespace Tally.Bus
{
    public abstract class EventMessage : IEventMessage
    {
        private readonly Guid _messageId;

        protected EventMessage(Guid messageId)
        {
            _messageId = messageId;
        }

        public Guid MessageId
        {
            get { return _messageId; }
        }
    }
}
