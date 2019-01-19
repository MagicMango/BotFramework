using BotCore.Models.Handler;

namespace BotCore.Controller
{
    public class HueController
    {
        public static string ControlLight(string color, string mode)
        {
            return new HueHandler().ControlLight(3, color, mode).Message;
        }
    }
}
