using BotCore.Interfaces.Repository;
using BotCore.Model.Entities;
using System;
using System.Linq;

namespace BotCore.Model
{
    public class HateRepository : Repository, IHateRepository
    {
        public HatePhrases GetRandomHatePhrase()
        {
            Random randomGenerator = new Random(DateTime.Now.Millisecond);
            int randomNumber = randomGenerator.Next(0, context.HatePhrases.Count() - 1);
            return context.HatePhrases.ToArray()[randomNumber];
        }
    }
}
