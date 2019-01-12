using BotCore.Interfaces;

namespace BotCore.DependencyInjection
{
    public static class ServiceLocator
    {
        private static IWrappedKernel kernel;

        public static void SetKernel(IWrappedKernel k)
        {
            if(kernel == null)
            {
                kernel = k;
            }
        }

        public static T GetInstance<T>() where T: IBase
        {
            return kernel.GetInstance<T>();
        }

        public static void Bind<Tinterface, Timplementation>() where Timplementation : Tinterface, new()
        {
            kernel.Bind<Tinterface, Timplementation>();
        } 

    }
}
