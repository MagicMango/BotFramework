﻿using BotCore.DependencyInjection;
using BotCore.Interfaces;
using BotCore.Interfaces.Repository;
using BotCore.Model;
using DiscordBotCore.Handler;
using System;
using System.Collections.Generic;
using TwitchBot;
using WebServer.Handler;
using WebServer.Model;

namespace MyServiceCore
{
    class Program
    {
        static void Main(string[] args)
        {
            IWrappedKernel wrapper = new MangoKernel()
                .Bind<ILoveRepository, LoveRepository>()
                .Bind<IHateRepository, HateRepository>();
            ServiceLocator.SetKernel(wrapper);
            MangoDiscordHandler h = new MangoDiscordHandler();
            MangoTwitchBot b = new MangoTwitchBot();
            var t = h.GetBot;
            t.Wait();

            List<Route> routen = new List<Route>() {
                new Route("", (response)=> ""),
                new Route("api/", (response) => "", Route.MethodType.POST, SaveConfiguration)
            };
            HttpHandler handler = new HttpHandler(routen, new Route("NotFound", (response) => "<HTML><BODY><h1>Page Not Found</h1></BODY></HTML>"));
            handler.Start();
        }

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