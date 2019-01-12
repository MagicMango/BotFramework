using BotCore.Controller;
using BotCore.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using TwitchLib.Api;
using TwitchLib.Api.Core.Models.Undocumented.Chatters;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace TwitchBot
{
    public class Bot
    {
        private static TwitchAPI api;
        private TwitchClient client;

        public Bot()
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
            Console.WriteLine($"{e.DateTime.ToString()}: {e.BotUsername} - {e.Data}");
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            Console.WriteLine($"Connected to {e.AutoJoinChannel}");
        }

        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            Console.WriteLine("Hey guys! I am a bot connected via TwitchLib!");
            client.SendMessage(e.Channel, "Hey guys! I am a bot connected via TwitchLib!");
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            Task<List<ChatterFormatted>> t = null;
            switch (e.ChatMessage.Message)
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
                            .Where(y=>y.Username != "magicmangobot")
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
                            .Where(y => y.Username != "magicmangobot")
                            .Select(x => x.Username)
                            .ToArray())
                    );
                    break;
                case "!chucknorris":
                    client.SendMessage(e.ChatMessage.Channel, WebApiController.GetRandomChuckNorrrisJoke());
                    break;
            }

        }

        private void Client_OnWhisperReceived(object sender, OnWhisperReceivedArgs e)
        {
            if (e.WhisperMessage.Username == "my_friend")
                client.SendWhisper(e.WhisperMessage.Username, "Hey! Whispers are so cool!!");
        }

        private void Client_OnNewSubscriber(object sender, OnNewSubscriberArgs e)
        {
            if (e.Subscriber.SubscriptionPlan == SubscriptionPlan.Prime)
                client.SendMessage(e.Channel, $"Welcome {e.Subscriber.DisplayName} to the substers! You just earned 500 points! So kind of you to use your Twitch Prime on this channel!");
            else
                client.SendMessage(e.Channel, $"Welcome {e.Subscriber.DisplayName} to the substers! You just earned 500 points!");
        }
    }
}
