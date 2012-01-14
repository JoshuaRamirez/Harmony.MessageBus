using Ninject;
using Tally.Bus.Contracts;
using Tally.Bus.Infrastructure;

namespace Tally.Bus
{
    public static class MessageBus
    {
        static MessageBus()
        {
            BootStrapper.Start();
        }

        public static void Send<TCommandMessage>(TCommandMessage commandMessage) where TCommandMessage : ICommandMessage
        {
            var messageBus = Container.Kernel.Get<IMessageBus>();
            messageBus.Send(commandMessage);
        }

        public static void Publish<TEventMessage>(TEventMessage eventMessage) where TEventMessage : IEventMessage
        {
            var messageBus = Container.Kernel.Get<IMessageBus>();
            messageBus.Publish(eventMessage);
        }

        public static void Subscribe<TEventMessage>(IMessageHandler<TEventMessage> messageHandler) where TEventMessage : IEventMessage
        {
            var messageBus = Container.Kernel.Get<IMessageBus>();
            messageBus.Subscribe(messageHandler);
        }

        public static void UnSubscribe<TEventMEssage>(IMessageHandler<TEventMEssage> messageHandler) where TEventMEssage : IEventMessage
        {
            var messageBus = Container.Kernel.Get<IMessageBus>();
            messageBus.UnSubscribe(messageHandler);
        }

        public static void AddHandler<TCommandMessage>(IMessageHandler<TCommandMessage> messageHandler) where TCommandMessage : ICommandMessage
        {
            var messageBus = Container.Kernel.Get<IMessageBus>();
            messageBus.AddHandler(messageHandler);
        }

        public static MessageBusConfigurationOptions Configure()
        {
            return new MessageBusConfigurationOptions();
        }
    }
}
