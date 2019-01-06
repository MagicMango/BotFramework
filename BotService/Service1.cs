using BotCore;
using BotCore.Interfaces;
using BotCore.Interfaces.Repository;
using BotCore.Model;
using BotTestConsole;
using DiscordBotCore.Handler;
using Ninject;
using System.ServiceProcess;

namespace BotService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            IWrappedKernel wrapper = new NinjectWrapper(new StandardKernel())
                .Bind<ILoveRepository, LoveRepository>();
            ServiceLocator.SetKernel(wrapper);

            ChannelHandler h = new ChannelHandler();
            var t = h.GetBot;
            t.Wait();
        }

        protected override void OnStop()
        {
        }
    }
}
