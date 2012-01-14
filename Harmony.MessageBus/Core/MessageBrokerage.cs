using System.Linq;
using System;
using System.Collections.Generic;
using Tally.Bus.Contracts;

namespace Tally.Bus.Core
{
    internal class MessageBrokerage : IMessageBrokerage
    {
        private readonly Dictionary<Type, IMessageBroker> _brokersByEachType;

        public MessageBrokerage()
        {
            _brokersByEachType = new Dictionary<Type, IMessageBroker>();
        }
        
        public void Raise<TMessage>(TMessage message) where TMessage : IMessage
        {
            var messageType = message.GetType();

            var brokers = findBrokers(messageType);
            foreach (var broker in brokers)
            {
                dynamic convertedMessage = Convert.ChangeType(message, messageType);
                broker.Raise(convertedMessage);
            }
        }

        public void AddHandler<TMessage>(IMessageHandler<TMessage> handler) where TMessage : IMessage
        {
            var containsType = _brokersByEachType.ContainsKey(typeof(TMessage));
            if (!containsType)
            {
                _brokersByEachType[typeof(TMessage)] = new MessageBroker();
            }
            var msgHandlerRegistry = _brokersByEachType[typeof (TMessage)];
            msgHandlerRegistry.AddHandler(handler);
        }

        public void RemoveHandler<TMessage>(IMessageHandler<TMessage> handler) where TMessage : IMessage
        {

            if (_brokersByEachType.ContainsKey(typeof(TMessage)))
            {
                var msgBroker = _brokersByEachType[typeof(TMessage)];
                msgBroker.RemoveHandler(handler);
            }
        }

        public void ClearHandlers<TMessage>() where TMessage : IMessage
        {
            var containsType = _brokersByEachType.ContainsKey(typeof(TMessage));
            if (containsType)
            {
                var msgBroker = _brokersByEachType[typeof(TMessage)];
                msgBroker.ClearHandlers<TMessage>();
            }
        }

        public Boolean HandlesType(Type type)
        {
            return findBrokers(type).Any();
        }

        private IEnumerable<IMessageBroker> findBrokers(Type messageType)
        {
            if (messageType == null) throw new ArgumentNullException("messageType");
            while (messageType != typeof(object))
            {
                if (messageType == null) throw new InvalidOperationException("Encountered a null object as the result of the BaseType property.");
                if (_brokersByEachType.ContainsKey(messageType))
                {
                    yield return _brokersByEachType[messageType];
                }
                messageType = messageType.BaseType;

            }
        }

    }
}