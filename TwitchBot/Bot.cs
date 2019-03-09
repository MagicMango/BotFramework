using BotCore.Controller;
using BotCore.DependencyInjection;
using BotCore.Interfaces;
using BotCore.Util;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api;
using TwitchLib.Api.Core.Models.Undocumented.Chatters;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace TwitchBot
{
    public class MangoTwitchBot
    {
        private static TwitchAPI api;
        private TwitchClient client;

        public MangoTwitchBot()
        {
            ConnectionCredentials credentials = new ConnectionCredentials(
                ConfigReader.GetStringValue("mybotservice:twitchbot:username"), 
                ConfigReader.GetStringValue("mybotservice:twitchbot:apitoken"));
            client = new TwitchClient();
            client.Initialize(credentials, ConfigReader.GetStringValue("mybotservice:twitchbot:channelname"));
            api = new TwitchAPI();
            //api.Settings.ClientId = "client_id";
            api.Settings.AccessToken = "oauth:" + ConfigReader.GetStringValue("mybotservice:twitchbot:apitoken");
            // client.OnLog += Client_OnLog;
            client.OnJoinedChannel += Client_OnJoinedChannel;
            client.OnMessageReceived += Client_OnMessageReceived;
            //client.OnWhisperReceived += Client_OnWhisperReceived;
            client.OnNewSubscriber += Client_OnNewSubscriber;
            client.OnConnected += Client_OnConnected;
            client.Connect();
        }

        private void Client_OnLog(object sender, OnLogArgs e)
        {

        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {

        }

        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            client.SendMessage(e.Channel, "The "+ ConfigReader.GetStringValue("mybotservice:twitchbot:username") + " ist now present!");
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            Task<List<ChatterFormatted>> t = null;
            if (e.ChatMessage.Message.StartsWith("!"))
            {
                string[] split = e.ChatMessage.Message.Split(' ');
                switch (split[0])
                {
                    case "!love":
                        t = Task.Run(async () =>
                                                await api.Undocumented.GetChattersAsync(e.ChatMessage.Channel)
                                        );
                        t.Wait();
                        client.SendMessage(e.ChatMessage.Channel,
                            new LoveController()
                            .GetRandomLovePhrase(e.ChatMessage.Username,
                                t.Result
                                .Where(y => y.Username != ConfigReader.GetStringValue("mybotservice:twitchbot:username"))
                                .Select(x => x.Username)
                                .ToArray())
                        );
                        break;
                    case "!hate":
                        t = Task.Run(async () =>
                                                await api.Undocumented.GetChattersAsync(e.ChatMessage.Channel)
                                        );
                        t.Wait();
                        client.SendMessage(e.ChatMessage.Channel,
                            new HateController()
                            .GetRandomHatePhrase(e.ChatMessage.Username,
                                t.Result
                                .Where(y => y.Username != ConfigReader.GetStringValue("mybotservice:twitchbot:username"))
                                .Select(x => x.Username)
                                .ToArray())
                        );
                        break;
                    case "!chucknorris":
                        client.SendMessage(e.ChatMessage.Channel, WebApiController.GetRandomChuckNorrrisJoke());
                        break;
                    case "!light":
                        try
                        {
                            client.SendMessage(e.ChatMessage.Channel, ServiceLocator.GetInstance<IControlLight>().ControlLights(split[1], split[2]));
                        }
                        catch
                        {
                            client.SendMessage(e.ChatMessage.Channel, "You must define color and mode!");
                        }
                        break;
                }
            }
        }

        private void Client_OnWhisperReceived(object sender, OnWhisperReceivedArgs e)
        {
           
        }

        private void Client_OnNewSubscriber(object sender, OnNewSubscriberArgs e)
        {
            
        }
    }
}
