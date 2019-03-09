using BotCore.DependencyInjection;
using BotCore.Interfaces.Repository;
using DiscordBot.Interfaces;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
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
                await ctx.RespondAsync(ServiceLocator.GetInstance<ILoveRepository>().GetRandomLovePhrase(ctx.Member.Username, loveSeeker.Select(x=>x.Username).ToArray()));
            }
            else
            {
                await ctx.RespondAsync(string.Format("Sorry {0} there are no other user in your Guild :(.", ctx.Member.Username));
            }
            
        }
    }
}
