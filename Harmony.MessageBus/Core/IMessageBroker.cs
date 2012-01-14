using Tally.Bus.Contracts;

namespace Tally.Bus.Core
{
    internal interface IMessageBroker
    {
        void Raise<TMessage>(TMessage message) where TMessage : IMessage;
        void AddHandler<TMessage>(IMessageHandler<TMessage> handler) where TMessage : IMessage;
        void RemoveHandler<TMessage>(IMessageHandler<TMessage> handler) where TMessage : IMessage;
        void ClearHandlers<TMessage>() where TMessage : IMessage;
    }
}