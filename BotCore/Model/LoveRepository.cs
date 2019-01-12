using BotCore.Interfaces.Repository;
using BotCore.Model.Entities;
using System;
using System.Linq;

namespace BotCore.Model
{
    public class LoveRepository : Repository, ILoveRepository
    {
        public LovePhrases GetRandomLovePhrase()
        {
            Random randomGenerator = new Random(DateTime.Now.Millisecond);
            int randomNumber = randomGenerator.Next(0, context.LovePhrases.Count());
            return context.LovePhrases.ToArray()[randomNumber];
        }
    }
}
