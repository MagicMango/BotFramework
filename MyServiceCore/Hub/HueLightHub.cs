using BotCore.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyServiceCore.Hubs
{
    public class HueLightHub : Hub, IControlLight
    {

        public async Task ControlLight(int id, string color, string mode)
        {
            await Clients.All.SendAsync("ControlLight", id, color, mode);
        }

        public string ControlLights(string color, string mode)
        {
            Task t = Task.Run(async () =>
            {
                await ControlLight(3, color, mode);
            });
            return "Done";
        }
    }
}
