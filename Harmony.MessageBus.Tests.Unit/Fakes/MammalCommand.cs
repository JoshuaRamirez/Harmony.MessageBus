using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tally.Bus.Contracts;


namespace Tally.Bus.Tests.Unit.Fakes
{
    public class MammalCommand : ICommandMessage
    {
        public MammalCommand()
        {
            MessageId = Guid.NewGuid();
        }

        public Guid MessageId { get; private set; }

        public void Send()
        {
            MessageBus.Send(this);
        }
    }
}
