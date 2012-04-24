using Ninject;
using Tally.Bus.Core;
using Tally.Bus.Infrastructure;

namespace Tally.Bus
{
    public static class MessageBus
    {
        static MessageBus()
        {
            BootStrapper.Start();
        }

        public static MessageBusConfigurationOptions Configure()
        {
            return new MessageBusConfigurationOptions();
        }

        public static void RegisterPublisher<TEventMessage>(IEventPublisher<TEventMessage> publisher) where TEventMessage : IEventMessage
        {
            var messageBus = Container.Kernel.Get<IMessageBus>();
            messageBus.RegisterPublisher(publisher);
        }

        public static void UnRegisterPublisher<TEventMessage>(IEventPublisher<TEventMessage> publisher) where TEventMessage : IEventMessage
        {
            var messageBus = Container.Kernel.Get<IMessageBus>();
            messageBus.UnRegisterPublisher(publisher);
        }

        public static void UnRegisterPublisher<TEventMessage>() where TEventMessage : IEventMessage
        {
            var messageBus = Container.Kernel.Get<IMessageBus>();
            messageBus.UnRegisterPublisher<TEventMessage>();
        }

        public static void RegisterSubscriber<TEventMessage>(IEventSubscriber<TEventMessage> subscriber) where TEventMessage : IEventMessage
        {
            var messageBus = Container.Kernel.Get<IMessageBus>();
            messageBus.RegisterSubscriber(subscriber);
        }

        public static void UnRegisterSubscriber<TEventMessage>(IEventSubscriber<TEventMessage> subscriber) where TEventMessage : IEventMessage
        {
            var messageBus = Container.Kernel.Get<IMessageBus>();
            messageBus.UnRegisterSubscriber(subscriber);
        }
    }
}
