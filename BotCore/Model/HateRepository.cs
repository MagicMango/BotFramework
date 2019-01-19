using BotCore.Interfaces.Repository;
using BotCore.Model.Entities;
using System;
using System.Linq;

namespace BotCore.Model
{
    public class HateRepository : Repository, IHateRepository
    {
        /// <summary>
        /// Will Return a Random hate Phrase for further process the Phrase will include {0} {1} for String Format function.
        /// <see cref="String.Format(string, string, string)"/>
        /// </summary>
        /// <returns></returns>
        public HatePhrases GetRandomHatePhrase()
        {
            Random randomGenerator = new Random(DateTime.Now.Millisecond);
            int randomNumber = randomGenerator.Next(0, context.HatePhrases.Count());
            return context.HatePhrases.ToArray()[randomNumber];
        }
    }
}
