using Tally.Bus.Core;

namespace Tally.Bus
{
    public delegate void HarmonyEvent<in TEventMessage>(TEventMessage eventMessage) where TEventMessage : IEventMessage;
}