using Ninject;
using Tally.Bus.Core;
using Tally.Bus.Implementations;

namespace Tally.Bus.Infrastructure
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
