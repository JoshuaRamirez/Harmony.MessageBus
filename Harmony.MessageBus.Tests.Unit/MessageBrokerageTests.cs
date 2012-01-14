using System;
using NSubstitute;
using NUnit.Framework;
using Tally.Bus.Contracts;
using Tally.Bus.Core;
using Tally.Bus.Tests.Unit.Fakes;


namespace Tally.Bus.Tests.Unit
{
    [TestFixture]
    public class MessageBrokerageTests
    {
        [Test]
        public void Raise_MessageHandlerAdded_HandlerInvoked()
        {
            //Arrange
            var handler = Substitute.For<IMessageHandler<FooHappens>>();
            var messageBrokerage = new MessageBrokerage();
            messageBrokerage.AddHandler(handler);

            //Act
            var message = new FooHappens(Guid.NewGuid());
            messageBrokerage.Raise(message);

            //Assert
            handler.Received(1).Handle(message);
        }

        [Test]
        public void Raise_MessageHandlerAdded_BaseTypeHandlerInvoked()
        {
            //Arrange
            var handler = Substitute.For<IMessageHandler<FooHappens>>();
            var messageBrokerage = new MessageBrokerage();
            messageBrokerage.AddHandler(handler);

            //Act
            var message = new FooBarHappens(Guid.NewGuid());
            messageBrokerage.Raise(message);

            //Assert
            handler.Received(1).Handle(message);
        }

        [Test]
        public void Raise_BaseTypeAndSubTypeMessageHandlersAdded_BaseTypeAndSubTypeHandlersInvoked()
        {
            //Arrange
            var fooHandler = Substitute.For<IMessageHandler<FooHappens>>();
            var fooBarHandler = Substitute.For<IMessageHandler<FooBarHappens>>();
            var messageBrokerage = new MessageBrokerage();
            messageBrokerage.AddHandler(fooHandler);
            messageBrokerage.AddHandler(fooBarHandler);

            //Act
            var message = new FooBarHappens(Guid.NewGuid());
            messageBrokerage.Raise(message);

            //Assert
            fooHandler.Received(1).Handle(message);
            fooBarHandler.Received(1).Handle(message);
        }
    }

}
