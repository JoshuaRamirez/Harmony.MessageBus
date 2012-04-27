using Harmony.MessageBus.Core;
using Harmony.MessageBus.Infrastructure;

namespace Harmony.MessageBus.FluentConfig
{
    public class MessageBusConfigurationOptions
    {
        public void UseCustomImplementation(IMessageBus messageBus)
        {
            Container.Kernel.Rebind<IMessageBus>().ToConstant(messageBus);
        }
    }
}