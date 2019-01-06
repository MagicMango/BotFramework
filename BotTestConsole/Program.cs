using BotCore;
using BotCore.Interfaces;
using BotCore.Interfaces.Repository;
using BotCore.Model;
using DiscordBotCore.Handler;
using Ninject;
using System;

namespace BotTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IWrappedKernel wrapper = new NinjectWrapper(new StandardKernel())
                .Bind<ILoveRepository, LoveRepository>();
            ServiceLocator.SetKernel(wrapper);

            ChannelHandler h = new ChannelHandler();
            var t = h.GetBot;
            t.Wait();
            Console.ReadKey();
        }
    }
}
