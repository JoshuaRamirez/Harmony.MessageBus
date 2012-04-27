using System;
using Harmony.MessageBus.Core;

namespace Harmony.MessageBus
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
