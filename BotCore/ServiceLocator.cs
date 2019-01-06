using BotCore.Interfaces;

namespace BotCore
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

        public static T GetInstance<T>()
        {
            return kernel.GetInstance<T>();
        }

        public static void Bind<Tinterface, Timplementation>() where Timplementation : Tinterface
        {
            kernel.Bind<Tinterface, Timplementation>();
        } 

    }
}
