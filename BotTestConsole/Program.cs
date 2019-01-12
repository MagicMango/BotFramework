using BotCore.DependencyInjection;
using BotCore.Interfaces;
using BotCore.Interfaces.Repository;
using BotCore.Model;
using DiscordBotCore.Handler;
using System;
using TwitchBot;

namespace BotTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IWrappedKernel wrapper = new MangoKernel()
                .Bind<ILoveRepository, LoveRepository>()
                .Bind<IHateRepository, HateRepository>();
            ServiceLocator.SetKernel(wrapper);
            ChannelHandler h = new ChannelHandler();
            Bot b = new Bot();
            var t = h.GetBot;
            t.Wait();
            Console.ReadKey();
        }
    }
}
