using BotCore.Interfaces;
using BotCore.Interfaces.Repository;
using System;
using System.Linq;

namespace BotCore.Model
{
    public class LoveRepository : Repository, ILoveRepository
    {
        /// <summary>
        /// Will Return a Random Love Phrase for further process the Phrase will include {0} {1} for String Format function.
        /// <see cref="String.Format(string, string, string)"/>
        /// </summary>
        /// <returns></returns>
        public ILovePhrase GetRandomLovePhrase()
        {
            Random randomGenerator = new Random(DateTime.Now.Millisecond);
            int randomNumber = randomGenerator.Next(0, context.LovePhrases.Count());
            return context.LovePhrases.ToArray()[randomNumber];
        }
    }
}
