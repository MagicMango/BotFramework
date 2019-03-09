﻿using BotCore.Controller;
using BotCore.DependencyInjection;
using BotCore.Interfaces;
using DiscordBot.Interfaces;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class LightCommands : IWillCommand
    {
        [Command("light"), Description("Control the Lights of the Streamer")]
        public async Task Light(CommandContext ctx, [RemainingText, Description("Color code in HEX and mode")] string colorandmode)
        {
            string[] options = colorandmode.Split(' ');
            
            await ctx.RespondAsync(ServiceLocator.GetInstance<IControlLight>().ControlLights(options[0], options[1]));
        }
    }
}
