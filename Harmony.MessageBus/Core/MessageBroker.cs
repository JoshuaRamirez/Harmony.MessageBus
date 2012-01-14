using System.Collections.Generic;
using Tally.Bus.Contracts;

namespace Tally.Bus.Core
{
    internal class MessageBroker : IMessageBroker
    {

        private readonly IList<dynamic> _messageHandlers;

        public MessageBroker()
        {
            _messageHandlers = new List<dynamic>();
        }

        public void Raise<TMessage>(TMessage message) where TMessage : IMessage
        {
            foreach (var messageHandler in _messageHandlers)
            {
                messageHandler.Handle(message);
            }
        }
        public void AddHandler<TMessage>(IMessageHandler<TMessage> handler) where TMessage : IMessage
        {
            _messageHandlers.Add(handler);
        }
        public void RemoveHandler<TMessage>(IMessageHandler<TMessage> handler) where TMessage : IMessage
        {
            _messageHandlers.Remove(handler);
        }

        public void ClearHandlers<TMessage>() where TMessage : IMessage
        {
            _messageHandlers.Clear();
        }
    }
}
