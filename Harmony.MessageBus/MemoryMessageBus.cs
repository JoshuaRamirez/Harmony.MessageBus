using System;
using Ninject;
using Tally.Bus.Contracts;
using Tally.Bus.Core;
using Tally.Bus.Infrastructure;

namespace Tally.Bus
{
    public class MemoryMessageBus: IMessageBus
    {
        private readonly IMessageBrokerage _messageBrokerage;

        public MemoryMessageBus()
        {
            _messageBrokerage = Container.Kernel.Get<IMessageBrokerage>();
        }

        public void Send<TCommandMessage>(TCommandMessage commandMessage) where TCommandMessage : ICommandMessage
        {
            var messageType = commandMessage.GetType();
            if (_messageBrokerage.HandlesType(messageType))
            {
                _messageBrokerage.Raise(commandMessage);
            }
            else
            {
                var errorMessage = String.Format("The Bus has not registered a handler for the '{0}' type.", messageType.FullName);
                throw new InvalidOperationException(errorMessage);
            }
        }

        public void Publish<TEventMessage>(TEventMessage eventMessage) where TEventMessage : IEventMessage
        {
            _messageBrokerage.Raise(eventMessage);
        }
        public void Subscribe<TEventMessage>(IMessageHandler<TEventMessage> messageHandler) where TEventMessage : IEventMessage
        {
            _messageBrokerage.AddHandler(messageHandler);
        }
        public void UnSubscribe<TEventMEssage>(IMessageHandler<TEventMEssage> messageHandler) where TEventMEssage : IEventMessage
        {
            _messageBrokerage.RemoveHandler(messageHandler);
        }
        public void AddHandler<TCommandMessage>(IMessageHandler<TCommandMessage> messageHandler) where TCommandMessage : ICommandMessage
        {
            var commandMessageType = typeof(TCommandMessage);
            if (_messageBrokerage.HandlesType(commandMessageType))
            {
                _messageBrokerage.ClearHandlers<TCommandMessage>();
            }
            _messageBrokerage.AddHandler(messageHandler);
        }

    }
}
