using System;
using Harmony.MessageBus.Core;
using Harmony.MessageBus.Tests.Unit.Fakes;
using NSubstitute;
using NUnit.Framework;

namespace Harmony.MessageBus.Tests.Unit
{
    [TestFixture]
    class InternalBusTests
    {

        [Test]
        public void EventHandlerIsCalledWhenItsSubscrivedAndTheEventIsPublished()
        {
            var messageId = Guid.NewGuid();
            var message = new FooHappens(messageId);
            var subscriber = Substitute.For<IEventSubscriber<FooHappens>>();
            var publisher = new FooPublisher();
            MessageBus.RegisterPublisher(publisher);
            MessageBus.RegisterSubscriber(subscriber);

            publisher.Publish(message);

            subscriber.Received(1).Handle(message);
        }
    }
}
