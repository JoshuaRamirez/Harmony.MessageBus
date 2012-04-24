using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Tally.Bus.Core;

namespace Tally.Bus.Implementations
{
    internal sealed class SingleProcessMessageBus : IMessageBus
    {

        //Class Level Variables
        private readonly Dictionary<Type, dynamic> _publishers;
        private readonly Dictionary<Type, Dictionary<Guid, IDisposable>> _subscriptions;
        private readonly Dictionary<Type, Dictionary<Guid, dynamic>> _subscribersToBindLater;

        //Constructor
        public SingleProcessMessageBus()
        {
            //Initialization
            _publishers = new Dictionary<Type, dynamic>();
            _subscriptions = new Dictionary<Type, Dictionary<Guid, IDisposable>>();
            _subscribersToBindLater = new Dictionary<Type, Dictionary<Guid, dynamic>>();


            //Event Based Decomposition - Event Binding
            PublisherRegistered += DisposeSubscriptionsByEventType;
            PublisherRegistered += WhenDeferredSubscriptionsExistThenTrySubscribingThem;
            PublisherUnRegistered += DisposeSubscriptionsByEventType;
            SubscriptionUnRegistered += WhenDeferredSubscriptionsExistThenForgetThem;
        }


        //Event Based Decomposition - Event Messages
        private event Action<Type> PublisherRegistered;
        private event Action<Type> PublisherUnRegistered;
        private event Action<Type> SubscriptionUnRegistered;


        //Event Based Decomposition - Event Handlers
        private void WhenDeferredSubscriptionsExistThenTrySubscribingThem(Type eventType)
        {
            if (!_subscribersToBindLater.ContainsKey(eventType)) return;
            foreach (var eventSubscriber in _subscribersToBindLater[eventType].Values)
            {
                WhenPublisherExistsThenBindImmediately(eventSubscriber, eventType);
            }
        }
        private void DisposeSubscriptionsByEventType(Type eventType)
        {
            if (!_subscriptions.ContainsKey(eventType)) return;
            _subscriptions[eventType].Values.ToObservable()
                .Do(x => x.Dispose())
                .Subscribe();
        }
        private void WhenDeferredSubscriptionsExistThenForgetThem(Type eventType)
        {
            if (!_subscribersToBindLater.ContainsKey(eventType)) return;
            _subscribersToBindLater[eventType].Clear();
        }


        //Public API

        /// <summary>
        /// Registers a single publisher with the bus. There can only be one publisher for each event type. Existing publishers are overwritten and the existing subscriptions will be removed.
        /// </summary>
        /// <typeparam name="TEventMessage"></typeparam>
        /// <param name="publisher"></param>
        public void RegisterPublisher<TEventMessage>(IEventPublisher<TEventMessage> publisher) where TEventMessage : IEventMessage
        {
            var observable = 
                Observable.FromEvent<HarmonyEvent<TEventMessage>, TEventMessage>(
                    eventHandler => publisher.Published += eventHandler,
                    eventHandler => publisher.Published -= eventHandler);
            var eventType = typeof(TEventMessage);
            _publishers[eventType] = observable;
            PublisherRegistered(eventType);
        }

        public void UnRegisterPublisher<TEventMessage>(IEventPublisher<TEventMessage> publisher) where TEventMessage : IEventMessage
        {
            UnRegisterPublisher<TEventMessage>();
        }

        public void UnRegisterPublisher<TEventMessage>() where TEventMessage : IEventMessage
        {
            var eventType = typeof(TEventMessage);
            _publishers[eventType] = null;
            PublisherUnRegistered(eventType);
        }

        public void RegisterSubscriber<TEventMessage>(IEventSubscriber<TEventMessage> subscriber) where TEventMessage : IEventMessage
        {
            var eventType = typeof (TEventMessage);
            WhenPublisherExistsThenBindImmediately(subscriber, eventType);
            WhenPublisherDoesntExistThenDeferBinding(subscriber, eventType);
        }

        public void UnRegisterSubscriber<TEventMessage>(IEventSubscriber<TEventMessage> subscriber) where TEventMessage : IEventMessage
        {
            var eventType = typeof(TEventMessage);
            WhenSubscriptionExistsDisposeIt(subscriber, eventType);
            WhenDeferredBindingExistsDisposeIt(subscriber, eventType);
            SubscriptionUnRegistered(eventType);
        }

        public void Dispose()
        {
            _subscriptions.Values.ToObservable()
                .Do(subscriptions => subscriptions.Values.ToObservable().Do(subscription => subscription.Dispose()))
                .Subscribe();
        }


        //Functionaly Decomposed Imperative Code

        private void WhenPublisherExistsThenBindImmediately<TEventMessage>(IEventSubscriber<TEventMessage> subscriber, Type eventType) where TEventMessage : IEventMessage
        {
            if (!_publishers.ContainsKey(eventType)) return;
            WhenBindingEnsureSubscriptionContainerExists(eventType);
            var observable = (IObservable<TEventMessage>)_publishers[eventType];
            var subscription = observable.Subscribe(subscriber.Handle);
            _subscriptions[eventType][subscriber.Id] = subscription;
        }
        private void WhenPublisherDoesntExistThenDeferBinding<TEventMessage>(IEventSubscriber<TEventMessage> subscriber, Type eventType) where TEventMessage : IEventMessage
        {
            if (_publishers.ContainsKey(eventType)) return;
            WhenDeferringBindingEnsureSubscriberContainerExists(eventType);
            _subscribersToBindLater[eventType][subscriber.Id] = subscriber;
        }
        private void WhenSubscriptionExistsDisposeIt<TEventMessage>(IEventSubscriber<TEventMessage> subscriber, Type eventType) where TEventMessage : IEventMessage
        {
            if (!_subscriptions.ContainsKey(eventType)) return;
            if (!_subscriptions[eventType].ContainsKey(subscriber.Id)) return;
            _subscriptions[eventType][subscriber.Id].Dispose();
            _subscriptions[eventType].Remove(subscriber.Id);
        }
        private void WhenDeferredBindingExistsDisposeIt<TEventMessage>(IEventSubscriber<TEventMessage> subscriber, Type eventType) where TEventMessage : IEventMessage
        {
            if (!_subscribersToBindLater.ContainsKey(eventType)) return;
            if (!_subscribersToBindLater[eventType].ContainsKey(subscriber.Id)) return;
            _subscribersToBindLater[eventType].Remove(subscriber.Id);
        }
        private void WhenBindingEnsureSubscriptionContainerExists(Type eventType)
        {
            if (_subscriptions.ContainsKey(eventType)) return;
            _subscriptions[eventType] = new Dictionary<Guid, IDisposable>();
        }
        private void WhenDeferringBindingEnsureSubscriberContainerExists(Type eventType)
        {
            if (_subscribersToBindLater.ContainsKey(eventType)) return;
            _subscribersToBindLater[eventType] = new Dictionary<Guid, dynamic>();
        }

    }
}
