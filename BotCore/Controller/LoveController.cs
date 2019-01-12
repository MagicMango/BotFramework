using BotCore.DependencyInjection;
using BotCore.Interfaces.Repository;
using BotCore.Model.Entities;
using System;

namespace BotCore.Controller
{
    public class LoveController
    {
        ILoveRepository repository;

        public LoveController()
        {
            repository = ServiceLocator.GetInstance<ILoveRepository>();
        }

        public string GetRandomLovePhrase()
        {
            using (repository)
            {
                return (repository.GetRandomLovePhrase() ?? new LovePhrases()).Phrase;
            }
        }

        public string GetRandomLovePhrase(string username, string [] loveSeeker)
        {
            using (repository)
            {
                Random randomGenerator = new Random(DateTime.Now.Millisecond);
                int randomNumber = randomGenerator.Next(0, loveSeeker.Length - 1);
                return string.Format((repository.GetRandomLovePhrase() ?? new LovePhrases()).Phrase, username, loveSeeker[randomNumber]);
            }
        }
    }
}
