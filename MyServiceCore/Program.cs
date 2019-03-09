using BotCore.DependencyInjection;
using BotCore.Interfaces;
using BotCore.Interfaces.Repository;
using BotCore.Model;
using DiscordBotCore.Handler;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using MyServiceCore.Handler;
using MyServiceCore.Hubs;
using System.Threading.Tasks;
using TwitchBot;
using WebServer.Handler;

namespace MyServiceCore
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebHost builded = CreateWebHostBuilder(args).Build();
            IWrappedKernel wrapper = new MangoKernel()
                .Bind<ILoveRepository, LoveRepository>()
                .Bind<IHateRepository, HateRepository>()
                .Bind<IControlLight>(() => 
                 new HubHandler((IHubContext<HueLightHub>)builded.Services.GetService(typeof(IHubContext<HueLightHub>)))
             );
            ServiceLocator.SetKernel(wrapper);
            MangoDiscordHandler h = new MangoDiscordHandler();
            MangoTwitchBot b = new MangoTwitchBot();
            /*List <Route> routen = new List<Route>() {
                new Route("", (response)=> ""),
                new Route("api/", (response) => "", Route.MethodType.POST, SaveConfiguration)
            };
            HttpHandler handler = new HttpHandler(routen, new Route("NotFound", (response) => "<HTML><BODY><h1>Page Not Found</h1></BODY></HTML>"));*/
            var t1 = Task.Run(() =>
            {
                builded.Run();
            }).ConfigureAwait(false);
            /*Task.Run(() =>
            {
                handler.Start();
            }).ConfigureAwait(false);*/
            var t2 = Task.Run(async () =>
            {
                await h.GetBot;
            });
            t2.Wait();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls(new[] { "http://www.stud-informatik.de:9090" })
                .UseStartup<Startup>();

        public static object SaveConfiguration(object conf)
        {
            dynamic dconf = conf;
            UglyStuff.ChangeSetting("mybotservice:twitchbot:channelname", dconf?.TwitchBotChannelName);
            UglyStuff.ChangeSetting("mybotservice:twitchbot:username", dconf?.TwitchBotName);
            UglyStuff.ChangeSetting("mybotservice:twitchbot:apitoken", dconf?.TwitchBotKey);
            UglyStuff.ChangeSetting("mybotservice:discordbot:apitoken", dconf?.DiscrodBotKey);
            return null;
        }
    }
}
