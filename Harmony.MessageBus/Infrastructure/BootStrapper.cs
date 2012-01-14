using Ninject;
using Tally.Bus.Core;

namespace Tally.Bus.Infrastructure
{
    internal static class BootStrapper
    {

        public static void Start()
        {
            IKernel kernel = new StandardKernel();
            kernel.Bind<IMessageBroker>().To<MessageBroker>();
            kernel.Bind<IMessageBrokerage>().To<MessageBrokerage>();
            kernel.Bind<IMessageBus>().To<MemoryMessageBus>().InSingletonScope();
            Container.Kernel = kernel;
        }

    }
}
