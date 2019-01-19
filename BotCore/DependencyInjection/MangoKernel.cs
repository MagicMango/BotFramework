using BotCore.Interfaces;
using System;
using System.Collections.Generic;

namespace BotCore.DependencyInjection
{
    /// <summary>
    /// Implementation of a Kernel for Dependency Injection
    /// will create a .ctor less Object to a Service an not process the .ctor Dependencies
    /// TODO: make it work with the .ctor Dependencys
    /// </summary>
    public class MangoKernel : IWrappedKernel
    {
        private static Dictionary<Type, Type> definitions = new Dictionary<Type, Type>();
        /// <summary>
        /// Register a implementation to a interface.
        /// Will throw InvalidOperationException on tryingin to bind the same Interface again.
        /// <seealso cref="IBase"/>
        /// </summary>
        /// <typeparam name="Tinterface">Interface of a service</typeparam>
        /// <typeparam name="Timplementation">Implementation of the Interface Tinterface</typeparam>
        /// <returns>IWrappedKernel for fluent Binding <see cref="IWrappedKernel"/></returns>
        public IWrappedKernel Bind<Tinterface, Timplementation>() where Timplementation : Tinterface, new()
        {
            if (definitions.ContainsKey(typeof(Tinterface))) throw new InvalidOperationException(string.Format("Already bound Interface: {0}, an interface can only be bound once.", typeof(Tinterface).FullName));
            definitions.Add(typeof(Tinterface), typeof(Timplementation));
            return this;
        }
        /// <summary>
        /// Will return a concrete implementation of an interface as Interface
        /// </summary>
        /// <typeparam name="T">Interface which the implementation is needed</typeparam>
        /// <returns></returns>
        public T GetInstance<T>() where T : IBase
        {
            if (!definitions.ContainsKey(typeof(T))) throw new InvalidOperationException(string.Format("No class bound for Interface: {0}.", typeof(T).FullName));
            Type t = null;
            definitions.TryGetValue(typeof(T), out t);
            return (T)Activator.CreateInstance(t);
        }
    }
}