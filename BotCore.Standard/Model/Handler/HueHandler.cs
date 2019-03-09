using BotCore.Util;
using Q42.HueApi;
using Q42.HueApi.ColorConverters;
using Q42.HueApi.ColorConverters.Original;
using Q42.HueApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BotCore.Models.Handler
{
    public static class HueHandler
    {

        private static string[] colors = new string[] { "D60270", "FFEE8A", "ACCB52", "0038A8", "EE1289", "FF83FA", "CD9B1D", "7EC0EE" };

        /// <summary>
        /// Change light of Hue lamp to specified color and mode
        /// </summary>
        /// <param name="id">Hue id of the lamp</param>
        /// <param name="color">HEXcoded Color</param>
        /// <param name="mode">blink, normal or disco</param>
        /// <returns></returns>
        public static HueHandlerMessage ControlLight(int id, string color, string mode)
        {
            int cnt = 0;
            ILocalHueClient client = new LocalHueClient(ConfigReader.GetStringValue("mybotservice:hue:ip"));
            client.Initialize(ConfigReader.GetStringValue("mybotservice:hue:token"));
            var command = new LightCommand();
            switch (mode)
            {
                case "blink":
                    command.Alert = Alert.Multiple;
                    cnt++;
                    break;
                case "disco":
                    return DiscoMode(client, id);
                default:
                    mode = "normal";
                    break;
            }
            try
            {
                command
                    .TurnOn()
                    .SetColor(new RGBColor(color))
                    .Brightness = 255;
                cnt += 3;
            }
            catch (Exception e)
            {
                return new HueHandlerMessage() {Success = false, Message = e.Message };
            }
            //command.Alert = Alert.Once;
            //Or start a colorloop
            //command.Effect = Effect.None;
            var t = client.SendCommandAsync(command, new List<string> { "" + id });
            t.Wait();
            var results = t.Result;
            return new HueHandlerMessage() {Message = "Changed light to color: " + color + " with mode: " + mode, Success = results.Where(x => x.Success != null).Count() == cnt };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private static HueHandlerMessage DiscoMode(ILocalHueClient client, int id)
        {
            Task.Run(() =>
            {
                var command = new LightCommand();
                command.TurnOn();
                command.Alert = Alert.Once;
                command.Brightness = 255;
                foreach (var item in colors)
                {
                    command.SetColor(new RGBColor(item));
                    var t = client.SendCommandAsync(command, new List<string> { "" + id });
                    t.Wait();
                    var results = t.Result;
                    Thread.Sleep(1000);
                }
            }).ConfigureAwait(false);
            return new HueHandlerMessage() { Message = "Successfully initiated disco mode", Success = true };

        }
    }
}