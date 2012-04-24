using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSubstitute;
using NUnit.Framework;
using Tally.Bus.Core;
using Tally.Bus.Tests.Unit.Fakes;


namespace Tally.Bus.Tests.Unit
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
