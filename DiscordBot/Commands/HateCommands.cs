using BotCore.Controller;
using BotCore.DependencyInjection;
using BotCore.Interfaces.Repository;
using DiscordBot.Interfaces;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class HateCommands: IWillCommand
    {
        [Command("hate"), Description("Spend Hate for someone you hate ... or not")]
        public async Task Hate(CommandContext ctx)
        {
            var guildMembers = await ctx.Guild.GetAllMembersAsync();
            var hateSeeker = guildMembers.Where(x => x.Id != ctx.Member.Id && x.Id != ctx.Client.CurrentUser.Id).ToArray();
            if (hateSeeker.Count() > 0)
            {
                await ctx.RespondAsync(ServiceLocator.GetInstance<IHateRepository>().GetRandomHatePhrase(ctx.Member.Username, hateSeeker.Select(x=>x.Username).ToArray()));
            }
            else
            {
                await ctx.RespondAsync(string.Format("Sorry {0} there are no other user in your Guild :(.", ctx.Member.Username));
            }
            
        }
    }
}
