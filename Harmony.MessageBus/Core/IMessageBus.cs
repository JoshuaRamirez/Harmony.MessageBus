using System;

namespace Tally.Bus.Core
{
    public interface IMessageBus : IDisposable
    {
        /// <summary>
        /// Registers a single publisher with the bus. There can only be one publisher for each event type. Existing publishers are overwritten and the existing subscriptions will be removed.
        /// </summary>
        /// <typeparam name="TEventMessage"></typeparam>
        /// <param name="publisher"></param>
        void RegisterPublisher<TEventMessage>(IEventPublisher<TEventMessage> publisher) where TEventMessage : IEventMessage;
        void UnRegisterPublisher<TEventMessage>(IEventPublisher<TEventMessage> publisher) where TEventMessage : IEventMessage;
        void UnRegisterPublisher<TEventMessage>() where TEventMessage : IEventMessage;
        void RegisterSubscriber<TEventMessage>(IEventSubscriber<TEventMessage> subscriber) where TEventMessage : IEventMessage;
        void UnRegisterSubscriber<TEventMessage>(IEventSubscriber<TEventMessage> subscriber) where TEventMessage : IEventMessage;
    }
}