using BotCore.Interfaces;
using Ninject;

namespace BotTestConsole
{
    public class NinjectWrapper : IWrappedKernel
    {

        private static IKernel kernel;

        public NinjectWrapper(IKernel k)
        {
            if(kernel == null)
            {
                kernel = k;
            }
        }

        public IWrappedKernel Bind<Tinterface, Timplementation>() where Timplementation : Tinterface
        {
            kernel.Bind<Tinterface>().To<Timplementation>();
        }

        public Tinstance GetInstance<Tinstance>()
        {
            return kernel.Get<Tinstance>();
        }
    }
}
