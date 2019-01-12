using BotCore.DependencyInjection;
using BotCore.Interfaces;
using BotCore.Interfaces.Repository;
using BotCore.Model;
using DiscordBotCore.Handler;
using System.ServiceProcess;
using TwitchBot;

namespace BotService
{
    public partial class MyBotService : ServiceBase
    {
        public MyBotService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            IWrappedKernel wrapper = new MangoKernel()
                .Bind<ILoveRepository, LoveRepository>()
                .Bind<IHateRepository, HateRepository>(); 
            ServiceLocator.SetKernel(wrapper);
            ChannelHandler h = new ChannelHandler();
            var t = h.GetBot;
            var b = new Bot();
        }

        protected override void OnStop()
        {
        }
    }
}
