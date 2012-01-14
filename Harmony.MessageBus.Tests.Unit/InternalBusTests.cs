using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSubstitute;
using NUnit.Framework;
using Tally.Bus.Contracts;
using Tally.Bus.Tests.Unit.Fakes;


namespace Tally.Bus.Tests.Unit
{
    [TestFixture]
    class InternalBusTests
    {

        [Test]
        public void Send_UsingBaseCommandClass_HandlerInvoked()
        {
            //Arrange
            var handler = Substitute.For<ICommandHandler<CatCommand>>();
            MessageBus.AddHandler(handler);

            //Act
            var message = new CatCommand();
            message.Send();

            //Assert
            handler.Received(1).Handle(message);
        }
    }
}
