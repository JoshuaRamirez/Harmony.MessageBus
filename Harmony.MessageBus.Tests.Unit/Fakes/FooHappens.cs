using System;
using Tally.Bus.Contracts;


namespace Tally.Bus.Tests.Unit.Fakes
{
    public class FooHappens : IMessage
    {
        private readonly Guid _messageId;
        public FooHappens(Guid messageId)
        {
            _messageId = messageId;
        }

        public Guid MessageId
        {
            get { return _messageId; }
        }
    }
    
    public class FooBarHappens : FooHappens
    {
        public FooBarHappens(Guid messageId) : base(messageId){}

        public string Bar()
        {
            return DateTime.Now.ToString();
        }

    }
}