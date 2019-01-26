using BotCore.Interfaces;
using BotCore.Interfaces.Repository;
using BotEntitiesNETCore.Models.Entities;

namespace BotCore.Model
{
    /// <summary>
    /// Test Repostory of ILoveRepository
    /// <see cref="ILoveRepository"/>
    /// </summary>
    public class LoveMockupRepository : ILoveRepository
    {
        /// <summary>
        /// No need to dispose for e.g. a database context.
        /// </summary>
        public void Dispose()
        {
            
        }
        /// <summary>
        /// Will Return a Random Love Phrase for further process the Phrase will include {0} {1} for String Format function.
        /// <see cref="String.Format(string, string, string)"/>
        /// </summary>
        /// <returns></returns>
        public ILovePhrase GetRandomLovePhrase()
        {
            return new LovePhrases() { Phrase = "{0} loves {1}" };
        }
    }
}
