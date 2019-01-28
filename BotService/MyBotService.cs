﻿using BotCore.DependencyInjection;
using BotCore.Interfaces;
using BotCore.Interfaces.Repository;
using BotCore.Model;
using DiscordBotCore.Handler;
using System.Collections.Generic;
using System.ServiceProcess;
using TwitchBot;
using WebServer.Handler;
using WebServer.Model;

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
            MangoDiscordHandler h = new MangoDiscordHandler();
            var t = h.GetBot;
            var b = new TwitchBot.MangoTwitchBot();
            
            List<Route> routen = new List<Route>() {
                new Route("", (response)=> ""),
                new Route("api/", (response) => "", Route.MethodType.POST, SaveConfiguration)
            };
            HttpHandler handler = new HttpHandler(routen, new Route("NotFound", (response) => "<HTML><BODY><h1>Page Not Found</h1></BODY></HTML>"));
            handler.Start();
        }

        private static object SaveConfiguration(object conf)
        {
            dynamic dconf = conf;
            UglyStuff.ChangeSetting("mybotservice:twitchbot:channelname", dconf?.TwitchBotChannelName);
            UglyStuff.ChangeSetting("mybotservice:twitchbot:username", dconf?.TwitchBotName);
            UglyStuff.ChangeSetting("mybotservice:twitchbot:apitoken", dconf?.TwitchBotKey);
            UglyStuff.ChangeSetting("mybotservice:discordbot:apitoken", dconf?.DiscrodBotKey);
            UglyStuff.ChangeSetting("mybotservice:hue:token", dconf?.DiscrodBotKey);
            UglyStuff.ChangeSetting("mybotservice:hue:ip", dconf?.DiscrodBotKey);
            return null;
        }


        protected override void OnStop()
        {
        }
    }
}
