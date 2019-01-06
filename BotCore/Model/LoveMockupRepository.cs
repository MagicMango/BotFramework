using BotCore.Interfaces.Repository;
using BotCore.Model.Entities;

namespace BotCore.Model
{
    public class LoveMockupRepository : ILoveRepository
    {
        public void Dispose()
        {
            
        }

        public LovePhrases GetRandomLovePhrase()
        {
            return new LovePhrases() { Phrase = "{0} loves {1}" };
        }
    }
}
