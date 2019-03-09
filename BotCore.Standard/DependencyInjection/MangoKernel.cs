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
        private static Dictionary<Type, Func<object>> lampbdadefinitions = new Dictionary<Type, Func<object>>();
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
            if (definitions.ContainsKey(typeof(Tinterface)) || lampbdadefinitions.ContainsKey(typeof(Tinterface))) throw new InvalidOperationException(string.Format("Already bound Interface: {0}, an interface can only be bound once.", typeof(Tinterface).FullName));
            definitions.Add(typeof(Tinterface), typeof(Timplementation));
            return this;
        }
        /// <summary>
        /// Register a implementation to a interface.
        /// Will throw InvalidOperationException on tryingin to bind the same Interface again.
        /// </summary>
        /// <typeparam name="Tinterface">Interface of a service</typeparam>
        /// <param name="Function">Function which will return the concrete Type to an Interface</param>
        /// <returns></returns>
        public IWrappedKernel Bind<Tinterface>(Func<object> Function)
        {
            if (lampbdadefinitions.ContainsKey(typeof(Tinterface)) || lampbdadefinitions.ContainsKey(typeof(Tinterface))) throw new InvalidOperationException(string.Format("Already bound Interface: {0}, an interface can only be bound once.", typeof(Tinterface).FullName));
            lampbdadefinitions.Add(typeof(Tinterface), Function);
            return this;
        }
        /// <summary>
        /// Will return a concrete implementation of an interface as Interface
        /// </summary>
        /// <typeparam name="T">Interface which the implementation is needed</typeparam>
        /// <returns></returns>
        public T GetInstance<T>() where T : IBase
        {
            bool objectDefinitions = definitions.ContainsKey(typeof(T));
            bool functionDefinitions = lampbdadefinitions.ContainsKey(typeof(T));
            if (!(objectDefinitions || functionDefinitions)) throw new InvalidOperationException(string.Format("No class bound for Interface: {0}.", typeof(T).FullName));
            if (objectDefinitions)
            {
                Type t = null;
                definitions.TryGetValue(typeof(T), out t);
                return (T)Activator.CreateInstance(t);
            }
            else
            {
                Func<object> o;
                lampbdadefinitions.TryGetValue(typeof(T), out o);
                return (T)o.Invoke();
            }
        }

    }
}