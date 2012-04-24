using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tally.Bus.Core;

namespace Tally.Bus
{
    internal class ExtensibleHarmonyEvent<TEventMessage> : IEventPublisher<TEventMessage> where TEventMessage : IEventMessage
    {
        public event HarmonyEvent<TEventMessage> Published
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }
    }
}
