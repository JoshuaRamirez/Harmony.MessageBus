using System;
using Harmony.MessageBus.Core;
using Harmony.MessageBus.Implementations;
using Harmony.MessageBus.Tests.Unit.Fakes;
using NSubstitute;
using NUnit.Framework;

namespace Harmony.MessageBus.Tests.Unit
{
    [TestFixture]
    public class SingleProcessMessageBusTests
    {

        [Test]
        [Category("Stability")]
        public void NoExceptionsWhenPublishingWithoutPublisherRegistrations()
        {
            var publisherFake = new FooPublisher();
            var messageId = Guid.NewGuid();
            var message = new FooHappens(messageId);

            publisherFake.Publish(message);
        }

        [Test]
        [Category("Stability")]
        public void NoExceptionsWhenRegisteringPublishersWithoutMatchingSubscribers()
        {
            var publisherFake = Substitute.For<IEventPublisher<FooHappens>>();
            var bus = new SingleProcessMessageBus();

            bus.RegisterPublisher(publisherFake);
        }

        [Test]
        [Category("Stability")]
        public void NoExceptionsWhenRegisteringSubscribersOfNoMatchingPublishers()
        {
            var subscriberFake = Substitute.For<IEventSubscriber<FooHappens>>();
            var bus = new SingleProcessMessageBus();

            bus.RegisterSubscriber(subscriberFake);
        }

        [Test]
        [Category("Stability")]
        public void NoExceptionsWhenOverwritingRegistrationsOfPublishersWithExistingSubscribers()
        {
            var subscriberId1 = Guid.NewGuid();
            var subscriberId2 = Guid.NewGuid();
            var subscriberFake1 = Substitute.For<IEventSubscriber<FooHappens>>();
            var subscriberFake2 = Substitute.For<IEventSubscriber<FooHappens>>();
            var publisherFake1 = Substitute.For<IEventPublisher<FooHappens>>();
            var publisherFake2 = Substitute.For<IEventPublisher<FooHappens>>();
            subscriberFake1.Id.Returns(subscriberId1);
            subscriberFake2.Id.Returns(subscriberId2);
            var bus = new SingleProcessMessageBus();
            bus.RegisterPublisher(publisherFake1);
            bus.RegisterSubscriber(subscriberFake1);
            bus.RegisterSubscriber(subscriberFake2);

            bus.RegisterPublisher(publisherFake2);
        }

        [Test]
        [Category("Stability")]
        public void NoExceptionsWhenOverwritingRegistrationsOfSubscribersOfMatchingPublishers()
        {
            var subscriberId1 = Guid.NewGuid();
            var subscriberId2 = Guid.NewGuid();
            var subscriberFake1 = Substitute.For<IEventSubscriber<FooHappens>>();
            var subscriberFake2 = Substitute.For<IEventSubscriber<FooHappens>>();
            var publisherFake1 = Substitute.For<IEventPublisher<FooHappens>>();
            subscriberFake1.Id.Returns(subscriberId1);
            subscriberFake2.Id.Returns(subscriberId2);
            var bus = new SingleProcessMessageBus();
            bus.RegisterPublisher(publisherFake1);
            bus.RegisterSubscriber(subscriberFake1);
            bus.RegisterSubscriber(subscriberFake2);

            bus.RegisterSubscriber(subscriberFake1);
            bus.RegisterSubscriber(subscriberFake2);
        }

        [Test]
        [Category("Stability")]
        public void NoExceptionsWhenRegisteringSubscribersOfMatchingPublishers()
        {
            var subscriberId1 = Guid.NewGuid();
            var subscriberId2 = Guid.NewGuid();
            var subscriberFake1 = Substitute.For<IEventSubscriber<FooHappens>>();
            var subscriberFake2 = Substitute.For<IEventSubscriber<FooHappens>>();
            var publisherFake1 = Substitute.For<IEventPublisher<FooHappens>>();
            subscriberFake1.Id.Returns(subscriberId1);
            subscriberFake2.Id.Returns(subscriberId2);
            var bus = new SingleProcessMessageBus();
            bus.RegisterPublisher(publisherFake1);

            bus.RegisterSubscriber(subscriberFake1);
            bus.RegisterSubscriber(subscriberFake2);
        }

        [Test]
        [Category("One Subscriber")]
        [Category("Does Handle")]
        [Category("One Subscriber Does Handle")]
        public void OneSubHandlesEventWhenPubIsRegisteredThenSubIsRegistered()
        {
            var messageId = Guid.NewGuid();
            var subscriberId = Guid.NewGuid();
            var subscriberFake = Substitute.For<IEventSubscriber<FooHappens>>();
            var publisherFake = new FooPublisher();
            subscriberFake.Id.Returns(subscriberId);
            var bus = new SingleProcessMessageBus();
            bus.RegisterPublisher(publisherFake);
            bus.RegisterSubscriber(subscriberFake);
            var message = new FooHappens(messageId);

            publisherFake.Publish(message);

            subscriberFake.Received(1).Handle(message);
        }

        [Test]
        [Category("One Subscriber")]
        [Category("Does Handle")]
        [Category("One Subscriber Does Handle")]
        [Category("Deferred Subscription")]
        public void OneSubHandlesEventWhenSubIsRegisteredThenPubIsRegistered()
        {
            var messageId = Guid.NewGuid();
            var subscriberId = Guid.NewGuid();
            var subscriberFake = Substitute.For<IEventSubscriber<FooHappens>>();
            var publisherFake = new FooPublisher();
            subscriberFake.Id.Returns(subscriberId);
            var bus = new SingleProcessMessageBus();
            bus.RegisterSubscriber(subscriberFake);
            bus.RegisterPublisher(publisherFake);
            var message = new FooHappens(messageId);

            publisherFake.Publish(message);

            subscriberFake.Received(1).Handle(message);
        }

        [Test]
        [Category("One Subscriber")]
        [Category("Don't Handle")]
        [Category("One Subscriber Doesn't Handle")]
        public void OneSubDoesntHandleEventWhenPubIsRegisteredThenSubIsRegisteredThenSubIsUnRegistered()
        {
            var messageId = Guid.NewGuid();
            var subscriberId = Guid.NewGuid();
            var subscriberFake = Substitute.For<IEventSubscriber<FooHappens>>();
            var publisherFake = new FooPublisher();
            subscriberFake.Id.Returns(subscriberId);
            var bus = new SingleProcessMessageBus();
            bus.RegisterPublisher(publisherFake);
            bus.RegisterSubscriber(subscriberFake);
            bus.UnRegisterSubscriber(subscriberFake);
            var message = new FooHappens(messageId);

            publisherFake.Publish(message);

            subscriberFake.Received(0).Handle(message);
        }

        [Test]
        [Category("One Subscriber")]
        [Category("Don't Handle")]
        [Category("One Subscriber Doesn't Handle")]
        [Category("Deferred Subscription Cancelled")]
        public void OneSubDoesntHandleEventWhenSubIsRegisteredThenPubIsRegisteredThenSubIsUnRegistered()
        {
            var messageId = Guid.NewGuid();
            var subscriberId = Guid.NewGuid();
            var subscriberFake = Substitute.For<IEventSubscriber<FooHappens>>();
            var publisherFake = new FooPublisher();
            subscriberFake.Id.Returns(subscriberId);
            var bus = new SingleProcessMessageBus();
            bus.RegisterSubscriber(subscriberFake);
            bus.RegisterPublisher(publisherFake);
            bus.UnRegisterSubscriber(subscriberFake);
            var message = new FooHappens(messageId);

            publisherFake.Publish(message);

            subscriberFake.Received(0).Handle(message);
        }

        [Test]
        [Category("One Subscriber")]
        [Category("Don't Handle")]
        [Category("One Subscriber Doesn't Handle")]
        public void OneSubDoesntHandleEventWhenPubIsRegisteredThenSubIsRegisteredThenPubIsUnRegistered()
        {
            var messageId = Guid.NewGuid();
            var subscriberId = Guid.NewGuid();
            var subscriberFake = Substitute.For<IEventSubscriber<FooHappens>>();
            var publisherFake = new FooPublisher();
            subscriberFake.Id.Returns(subscriberId);
            var bus = new SingleProcessMessageBus();
            bus.RegisterPublisher(publisherFake);
            bus.RegisterSubscriber(subscriberFake);
            bus.UnRegisterPublisher(publisherFake);
            var message = new FooHappens(messageId);

            publisherFake.Publish(message);

            subscriberFake.Received(0).Handle(message);
        }

        [Test]
        [Category("One Subscriber")]
        [Category("Don't Handle")]
        [Category("One Subscriber Doesn't Handle")]
        public void OneSubDoesntHandleEventWhenPubIsRegisteredThenSubIsRegisteredThenPubIsUnRegisteredThenPubIsRegistered()
        {
            var messageId = Guid.NewGuid();
            var subscriberId = Guid.NewGuid();
            var subscriberFake = Substitute.For<IEventSubscriber<FooHappens>>();
            var publisherFake = new FooPublisher();
            subscriberFake.Id.Returns(subscriberId);
            var bus = new SingleProcessMessageBus();
            bus.RegisterPublisher(publisherFake);
            bus.RegisterSubscriber(subscriberFake);
            bus.UnRegisterPublisher(publisherFake);
            bus.RegisterPublisher(publisherFake);
            var message = new FooHappens(messageId);

            publisherFake.Publish(message);

            subscriberFake.Received(0).Handle(message);
        }

        [Test]
        [Category("One Subscriber")]
        [Category("Don't Handle")]
        [Category("One Subscriber Doesn't Handle")]
        [Category("Deferred Subscription")]
        [Category("Deferred Subscription Cancelled")]
        public void OneSubDoesntHandleEventWhenSubIsRegisteredThenSubIsUnRegisteredThenPubIsRegistered()
        {
            var messageId = Guid.NewGuid();
            var subscriberId = Guid.NewGuid();
            var subscriberFake = Substitute.For<IEventSubscriber<FooHappens>>();
            var publisherFake = new FooPublisher();
            subscriberFake.Id.Returns(subscriberId);
            var bus = new SingleProcessMessageBus();
            bus.RegisterSubscriber(subscriberFake);
            bus.UnRegisterSubscriber(subscriberFake);
            bus.RegisterPublisher(publisherFake);
            var message = new FooHappens(messageId);

            publisherFake.Publish(message);

            subscriberFake.Received(0).Handle(message);
        }



        [Test]
        [Category("Three Subscribers")]
        [Category("Does Handle")]
        [Category("Three Subscribers Do Handle")]
        public void ThreeSubsHandleEventWhenPubIsRegisteredThenSubsAreRegistered()
        {
            var subscriberId1 = Guid.NewGuid();
            var subscriberId2 = Guid.NewGuid();
            var subscriberId3 = Guid.NewGuid();
            var subscriberFake1 = Substitute.For<IEventSubscriber<FooHappens>>();
            var subscriberFake2 = Substitute.For<IEventSubscriber<FooHappens>>();
            var subscriberFake3 = Substitute.For<IEventSubscriber<FooHappens>>();
            var publisherFake = new FooPublisher();
            subscriberFake1.Id.Returns(subscriberId1);
            subscriberFake2.Id.Returns(subscriberId2);
            subscriberFake3.Id.Returns(subscriberId3);
            var messageId = Guid.NewGuid();
            var message = new FooHappens(messageId);
            var bus = new SingleProcessMessageBus();
            bus.RegisterPublisher(publisherFake);
            bus.RegisterSubscriber(subscriberFake1);
            bus.RegisterSubscriber(subscriberFake2);
            bus.RegisterSubscriber(subscriberFake3);

            publisherFake.Publish(message);

            subscriberFake1.Received(1).Handle(message);
            subscriberFake2.Received(1).Handle(message);
            subscriberFake3.Received(1).Handle(message);
        }

        [Test]
        [Category("Three Subscribers")]
        [Category("Does Handle")]
        [Category("Three Subscribers Do Handle")]
        [Category("Deferred Subscription")]
        public void ThreeSubsHandleEventWhenSubsAreRegisteredThenPubIsRegistered()
        {
            var subscriberId1 = Guid.NewGuid();
            var subscriberId2 = Guid.NewGuid();
            var subscriberId3 = Guid.NewGuid();
            var subscriberFake1 = Substitute.For<IEventSubscriber<FooHappens>>();
            var subscriberFake2 = Substitute.For<IEventSubscriber<FooHappens>>();
            var subscriberFake3 = Substitute.For<IEventSubscriber<FooHappens>>();
            var publisherFake = new FooPublisher();
            subscriberFake1.Id.Returns(subscriberId1);
            subscriberFake2.Id.Returns(subscriberId2);
            subscriberFake3.Id.Returns(subscriberId3);
            var messageId = Guid.NewGuid();
            var message = new FooHappens(messageId);
            var bus = new SingleProcessMessageBus();
            bus.RegisterSubscriber(subscriberFake1);
            bus.RegisterSubscriber(subscriberFake2);
            bus.RegisterSubscriber(subscriberFake3);
            bus.RegisterPublisher(publisherFake);

            publisherFake.Publish(message);

            subscriberFake1.Received(1).Handle(message);
            subscriberFake2.Received(1).Handle(message);
            subscriberFake3.Received(1).Handle(message);
        }

        [Test]
        [Category("Three Subscribers")]
        [Category("Don't Handle")]
        [Category("Three Subscribers Don't Handle")]
        public void ThreeSubsDontHandleEventWhenPubIsRegisteredThenSubsAreRegisteredThenPubIsUnRegistered()
        {
            var subscriberId1 = Guid.NewGuid();
            var subscriberId2 = Guid.NewGuid();
            var subscriberId3 = Guid.NewGuid();
            var subscriberFake1 = Substitute.For<IEventSubscriber<FooHappens>>();
            var subscriberFake2 = Substitute.For<IEventSubscriber<FooHappens>>();
            var subscriberFake3 = Substitute.For<IEventSubscriber<FooHappens>>();
            var publisherFake = new FooPublisher();
            subscriberFake1.Id.Returns(subscriberId1);
            subscriberFake2.Id.Returns(subscriberId2);
            subscriberFake3.Id.Returns(subscriberId3);
            var messageId = Guid.NewGuid();
            var message = new FooHappens(messageId);
            var bus = new SingleProcessMessageBus();
            bus.RegisterPublisher(publisherFake);
            bus.RegisterSubscriber(subscriberFake1);
            bus.RegisterSubscriber(subscriberFake2);
            bus.RegisterSubscriber(subscriberFake3);
            bus.UnRegisterPublisher(publisherFake);

            publisherFake.Publish(message);

            subscriberFake1.Received(0).Handle(message);
            subscriberFake2.Received(0).Handle(message);
            subscriberFake3.Received(0).Handle(message);
        }

        [Test]
        [Category("Three Subscribers")]
        [Category("Don't Handle")]
        [Category("Three Subscribers Don't Handle")]
        public void ThreeSubsDontHandleEventWhenPubIsRegisteredThenSubsAreRegisteredThenPubIsUnRegisteredThenPubIsRegistered()
        {
            var subscriberId1 = Guid.NewGuid();
            var subscriberId2 = Guid.NewGuid();
            var subscriberId3 = Guid.NewGuid();
            var subscriberFake1 = Substitute.For<IEventSubscriber<FooHappens>>();
            var subscriberFake2 = Substitute.For<IEventSubscriber<FooHappens>>();
            var subscriberFake3 = Substitute.For<IEventSubscriber<FooHappens>>();
            var publisherFake = new FooPublisher();
            subscriberFake1.Id.Returns(subscriberId1);
            subscriberFake2.Id.Returns(subscriberId2);
            subscriberFake3.Id.Returns(subscriberId3);
            var messageId = Guid.NewGuid();
            var message = new FooHappens(messageId);
            var bus = new SingleProcessMessageBus();
            bus.RegisterPublisher(publisherFake);
            bus.RegisterSubscriber(subscriberFake1);
            bus.RegisterSubscriber(subscriberFake2);
            bus.RegisterSubscriber(subscriberFake3);
            bus.UnRegisterPublisher(publisherFake);
            bus.RegisterPublisher(publisherFake);

            publisherFake.Publish(message);

            subscriberFake1.Received(0).Handle(message);
            subscriberFake2.Received(0).Handle(message);
            subscriberFake3.Received(0).Handle(message);
        }

        [Test]
        [Category("Three Subscribers")]
        [Category("Don't Handle")]
        [Category("Three Subscribers Don't Handle")]
        public void ThreeSubsDontHandleEventWhenPubIsRegisteredThenSubsAreRegisteredThenSubsAreUnRegistered()
        {
            var subscriberId1 = Guid.NewGuid();
            var subscriberId2 = Guid.NewGuid();
            var subscriberId3 = Guid.NewGuid();
            var subscriberFake1 = Substitute.For<IEventSubscriber<FooHappens>>();
            var subscriberFake2 = Substitute.For<IEventSubscriber<FooHappens>>();
            var subscriberFake3 = Substitute.For<IEventSubscriber<FooHappens>>();
            var publisherFake = new FooPublisher();
            subscriberFake1.Id.Returns(subscriberId1);
            subscriberFake2.Id.Returns(subscriberId2);
            subscriberFake3.Id.Returns(subscriberId3);
            var messageId = Guid.NewGuid();
            var message = new FooHappens(messageId);
            var bus = new SingleProcessMessageBus();
            bus.RegisterPublisher(publisherFake);
            bus.RegisterSubscriber(subscriberFake1);
            bus.RegisterSubscriber(subscriberFake2);
            bus.RegisterSubscriber(subscriberFake3);
            bus.UnRegisterSubscriber(subscriberFake1);
            bus.UnRegisterSubscriber(subscriberFake2);
            bus.UnRegisterSubscriber(subscriberFake3);

            publisherFake.Publish(message);

            subscriberFake1.Received(0).Handle(message);
            subscriberFake2.Received(0).Handle(message);
            subscriberFake3.Received(0).Handle(message);
        }

        [Test]
        [Category("Three Subscribers")]
        [Category("Don't Handle")]
        [Category("Three Subscribers Don't Handle")]
        [Category("Deferred Subscription Cancelled")]
        public void ThreeSubsDontHandleEventWhenSubsAreRegisteredThenPubIsRegisteredThenSubsAreUnRegistered()
        {
            var subscriberId1 = Guid.NewGuid();
            var subscriberId2 = Guid.NewGuid();
            var subscriberId3 = Guid.NewGuid();
            var subscriberFake1 = Substitute.For<IEventSubscriber<FooHappens>>();
            var subscriberFake2 = Substitute.For<IEventSubscriber<FooHappens>>();
            var subscriberFake3 = Substitute.For<IEventSubscriber<FooHappens>>();
            var publisherFake = new FooPublisher();
            subscriberFake1.Id.Returns(subscriberId1);
            subscriberFake2.Id.Returns(subscriberId2);
            subscriberFake3.Id.Returns(subscriberId3);
            var messageId = Guid.NewGuid();
            var message = new FooHappens(messageId);
            var bus = new SingleProcessMessageBus();
            bus.RegisterSubscriber(subscriberFake1);
            bus.RegisterSubscriber(subscriberFake2);
            bus.RegisterSubscriber(subscriberFake3);
            bus.RegisterPublisher(publisherFake);
            bus.UnRegisterSubscriber(subscriberFake1);
            bus.UnRegisterSubscriber(subscriberFake2);
            bus.UnRegisterSubscriber(subscriberFake3);

            publisherFake.Publish(message);

            subscriberFake1.Received(0).Handle(message);
            subscriberFake2.Received(0).Handle(message);
            subscriberFake3.Received(0).Handle(message);
        }

        [Test]
        [Category("Three Subscribers")]
        [Category("Don't Handle")]
        [Category("Three Subscribers Don't Handle")]
        [Category("Deferred Subscription Cancelled")]
        public void ThreeSubsDontHandleEventWhenSubsAreRegisteredThenSubsAreUnRegisteredThenPubIsRegistered()
        {
            var subscriberId1 = Guid.NewGuid();
            var subscriberId2 = Guid.NewGuid();
            var subscriberId3 = Guid.NewGuid();
            var subscriberFake1 = Substitute.For<IEventSubscriber<FooHappens>>();
            var subscriberFake2 = Substitute.For<IEventSubscriber<FooHappens>>();
            var subscriberFake3 = Substitute.For<IEventSubscriber<FooHappens>>();
            var publisherFake = new FooPublisher();
            subscriberFake1.Id.Returns(subscriberId1);
            subscriberFake2.Id.Returns(subscriberId2);
            subscriberFake3.Id.Returns(subscriberId3);
            var messageId = Guid.NewGuid();
            var message = new FooHappens(messageId);
            var bus = new SingleProcessMessageBus();
            bus.RegisterSubscriber(subscriberFake1);
            bus.RegisterSubscriber(subscriberFake2);
            bus.RegisterSubscriber(subscriberFake3);
            bus.UnRegisterSubscriber(subscriberFake1);
            bus.UnRegisterSubscriber(subscriberFake2);
            bus.UnRegisterSubscriber(subscriberFake3);
            bus.RegisterPublisher(publisherFake);

            publisherFake.Publish(message);

            subscriberFake1.Received(0).Handle(message);
            subscriberFake2.Received(0).Handle(message);
            subscriberFake3.Received(0).Handle(message);
        }
    }
}
