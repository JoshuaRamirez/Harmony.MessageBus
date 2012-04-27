using Harmony.MessageBus.Core;
using Harmony.MessageBus.Implementations;
using Ninject;

namespace Harmony.MessageBus.Infrastructure
{
    internal static class BootStrapper
    {

        public static void Start()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IMessageBus>().To<SingleProcessMessageBus>().InSingletonScope();
            Container.Kernel = kernel;
        }

    }
}
