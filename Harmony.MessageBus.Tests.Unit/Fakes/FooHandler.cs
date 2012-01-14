using System;
using Tally.Bus.Contracts;


namespace Tally.Bus.Tests.Unit.Fakes
{
    public class FooHandler : IMessageHandler<FooHappens>
    {
        private readonly int _id;
        public FooHandler(int id)
        {
            _id = id;
        }

        public void Handle(FooHappens message)
        {
            var log = string.Format("Handler {0} received the FooHappens Event Id: {1}", _id, message.MessageId);
            Console.WriteLine(log);
        }
    }
}