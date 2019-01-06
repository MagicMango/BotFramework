using BotCore.Interfaces.Repository;
using BotCore.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotCore.Model
{
    public class LoveRepository : Repository, ILoveRepository
    {
        public LovePhrases GetRandomLovePhrase()
        {
            Random randomGenerator = new Random(DateTime.Now.Millisecond);
            int randomNumber = randomGenerator.Next(0, context.LovePhrases.Count() - 1);
            return context.LovePhrases.ToArray()[randomNumber];
        }
    }
}
