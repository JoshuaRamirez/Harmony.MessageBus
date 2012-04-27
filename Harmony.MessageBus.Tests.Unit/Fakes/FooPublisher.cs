using Harmony.MessageBus.Core;

namespace Harmony.MessageBus.Tests.Unit.Fakes
{
    public class FooPublisher : IEventPublisher<FooHappens>
    {
        public event HarmonyEvent<FooHappens> Published;

        public void Publish(FooHappens message)
        {
            if (Published == null) return;
            Published(message);
        }
    }
}
