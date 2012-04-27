using Harmony.MessageBus.Core;

namespace Harmony.MessageBus
{
    public delegate void HarmonyEvent<in TEventMessage>(TEventMessage eventMessage) where TEventMessage : IEventMessage;
}