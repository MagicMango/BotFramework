using BotCore.Interfaces;
using System;
using System.Collections.Generic;

namespace BotCore.DependencyInjection
{
    public class MangoKernel : IWrappedKernel
    {
        private static Dictionary<Type, Type> definitions = new Dictionary<Type, Type>();

        public IWrappedKernel Bind<Tinterface, Timplementation>() where Timplementation : Tinterface, new()
        {
            if (!definitions.ContainsKey(typeof(Tinterface)))
            {
                definitions.Add(typeof(Tinterface), typeof(Timplementation));
            }
            else
            {
                throw new InvalidOperationException(string.Format("Already bound Interface: {0}, an interface can only be bound once.", typeof(Tinterface).FullName));
            }
            return this;
        }

        public T GetInstance<T>() where T: IBase
        {
            if (definitions.ContainsKey(typeof(T)))
            {
                Type t = null;
                definitions.TryGetValue(typeof(T), out t);
                return (T)Activator.CreateInstance(t);
            }
            else
            {
                throw new InvalidOperationException(string.Format("No class bound for Interface: {0}.", typeof(T).FullName));
            }

        }
    }
}