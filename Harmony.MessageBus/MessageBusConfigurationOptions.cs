using Tally.Bus.Infrastructure;

namespace Tally.Bus
{
    public class MessageBusConfigurationOptions
    {
        public void UseCustomImplementation(IMessageBus messageBus)
        {
            Container.Kernel.Rebind<IMessageBus>().ToConstant(messageBus);
        }
    }
}