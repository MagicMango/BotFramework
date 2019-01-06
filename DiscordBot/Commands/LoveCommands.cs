using BotCore.Controller;
using DiscordBot.Interfaces;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class LoveCommands: IWillCommand
    {
        [Command("love"), Description("Spend Love for someone you like ... or not")]
        public async Task Love(CommandContext ctx)
        {
            var guildMembers = await ctx.Guild.GetAllMembersAsync();
            var loveSeeker = guildMembers.Where(x => x.Id != ctx.Member.Id && x.Id != ctx.Client.CurrentUser.Id).ToArray();
            if (loveSeeker.Count() > 0)
            {
                LoveController loveController = new LoveController();
                Random randomGenerator = new Random(DateTime.Now.Millisecond);
                int randomNumber = randomGenerator.Next(0, loveSeeker.Length - 1);
                await ctx.RespondAsync(string.Format(loveController.GetRandomLovePhrase(), ctx.Member.Username, loveSeeker[randomNumber].Username));
            }
            else
            {
                await ctx.RespondAsync(string.Format("Sorry {0} there are no other user in your Guild :(.", ctx.Member.Username));
            }
            
        }
    }
}
