namespace BotCore.Interfaces
{
    public interface IWrappedKernel
    {
        /// <summary>
        /// Will return a concrete implementation of an interface as Interface
        /// </summary>
        /// <typeparam name="T">Interface which the implementation is needed</typeparam>
        /// <returns></returns>
        T GetInstance<T>() where T: IBase;
        /// <summary>
        /// Register a implementation to a interface.
        /// Will throw InvalidOperationException on tryingin to bind the same Interface again.
        /// <seealso cref="IBase"/>
        /// </summary>
        /// <typeparam name="Tinterface">Interface of a service</typeparam>
        /// <typeparam name="Timplementation">Implementation of the Interface Tinterface</typeparam>
        /// <returns>IWrappedKernel for fluent Binding <see cref="IWrappedKernel"/></returns>
        IWrappedKernel Bind<Tinterface, Timplementation>() where Timplementation : Tinterface, new();
    }
}
