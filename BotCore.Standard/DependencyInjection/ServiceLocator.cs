using BotCore.Interfaces;

namespace BotCore.DependencyInjection
{
    /// <summary>
    /// Service which will provied the needed implementation of a interface.
    /// Is independed from Kernel implementation. 
    /// Always use the ServiceLocator for further implementation
    /// </summary>
    public static class ServiceLocator
    {
        private static IWrappedKernel kernel;
        /// <summary>
        /// Define the Kernel at the startup of your application
        /// </summary>
        /// <param name="k">Kernel implementation of IWrappedKernel <see cref="IWrappedKernel"/></param>
        public static void SetKernel(IWrappedKernel k)
        {
            if(kernel == null)
            {
                kernel = k;
            }
        }
        /// <summary>
        /// Same behaviour as IWrappedKernel
        /// <see cref="IWrappedKernel"/>
        /// </summary>
        /// <typeparam name="T">Interface</typeparam>
        /// <returns>Instance to Interface</returns>
        public static T GetInstance<T>() where T: IBase
        {
            return kernel.GetInstance<T>();
        }

        /// <summary>
        /// Wrapper method for kernel.Bind
        /// <see cref="IWrappedKernel.Bind{Tinterface, Timplementation}"/>
        /// </summary>
        /// <typeparam name="Tinterface"><see cref="IWrappedKernel"/></typeparam>
        /// <typeparam name="Timplementation"><see cref="IWrappedKernel"/></typeparam>
        public static void Bind<Tinterface, Timplementation>() where Timplementation : Tinterface, new()
        {
            kernel.Bind<Tinterface, Timplementation>();
        } 

    }
}
