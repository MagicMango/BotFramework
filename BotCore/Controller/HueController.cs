﻿using BotCore.Models.Handler;

namespace BotCore.Controller
{
    public class HueController
    {
        /// <summary>
        /// Will control a Hue Light with given options
        /// </summary>
        /// <param name="color">Hexcode collor to change to</param>
        /// <param name="mode">mode to choose: blink, normal, disco</param>
        /// <returns>Message will indicate a success or failure based on message</returns>
        public static string ControlLight(string color, string mode)
        {
            return new HueHandler().ControlLight(3, color, mode).Message;
        }
    }
}
