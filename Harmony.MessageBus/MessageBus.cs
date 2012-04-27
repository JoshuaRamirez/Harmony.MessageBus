using Harmony.MessageBus.Core;
using Harmony.MessageBus.FluentConfig;
using Harmony.MessageBus.Infrastructure;
using Ninject;

namespace Harmony.MessageBus
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
