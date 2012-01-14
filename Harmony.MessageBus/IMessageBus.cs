using Tally.Bus.Contracts;

namespace Tally.Bus
{
    public interface IMessageBus
    {
        void Send<TCommandMessage>(TCommandMessage commandMessage) where TCommandMessage : ICommandMessage;
        void Publish<TEventMessage>(TEventMessage eventMessage) where TEventMessage : IEventMessage;
        void Subscribe<TEventMessage>(IMessageHandler<TEventMessage> messageHandler) where TEventMessage : IEventMessage;
        void UnSubscribe<TEventMEssage>(IMessageHandler<TEventMEssage> messageHandler) where TEventMEssage : IEventMessage;
        void AddHandler<TCommandMessage>(IMessageHandler<TCommandMessage> messageHandler) where TCommandMessage : ICommandMessage;
    }
}
