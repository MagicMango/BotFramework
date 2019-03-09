using BotCore.Interfaces;
using Microsoft.AspNetCore.SignalR;
using MyServiceCore.Hubs;
using System.Threading.Tasks;

namespace MyServiceCore.Handler
{
    public class HubHandler : IControlLight
    {
        private IHubContext<HueLightHub> context;

        public HubHandler()
        {
        }
        public HubHandler(IHubContext<HueLightHub> context)
        {
            this.context = context;
        }

        public string ControlLights(string color, string mode)
        {
            Task.Run(async () =>
            {
                await context.Clients.All.SendAsync("ControlLight", 3, color, mode);
            });
            return "Done";
        }
    }
}
