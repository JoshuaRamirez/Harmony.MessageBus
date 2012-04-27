using System;
using Harmony.MessageBus.Core;

namespace Harmony.MessageBus
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
