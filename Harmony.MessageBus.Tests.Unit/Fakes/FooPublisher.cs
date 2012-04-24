using Tally.Bus.Core;

namespace Tally.Bus.Tests.Unit.Fakes
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
