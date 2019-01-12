using BotCore.Controller;
using DiscordBot.Interfaces;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class JokeCommands : IWillCommand
    {
        [Command("!chucknorris"), Description("Sends a random Chuck Norris joke to the chat")]
        public async Task ChuckNorris(CommandContext ctx)
        {
            await ctx.RespondAsync(WebApiController.GetRandomChuckNorrrisJoke());
        }
    }
}
