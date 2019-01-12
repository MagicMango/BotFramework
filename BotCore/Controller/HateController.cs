using BotCore.DependencyInjection;
using BotCore.Interfaces.Repository;
using BotCore.Model.Entities;
using System;

namespace BotCore.Controller
{
    public class HateController
    {
        IHateRepository repository;

        public HateController()
        {
            repository = ServiceLocator.GetInstance<IHateRepository>();
        }

        public string GetRandomHatePhrase()
        {
            using (repository)
            {
                return (repository.GetRandomHatePhrase() ?? new HatePhrases()).Phrase;
            }
        }

        public string GetRandomHatePhrase(string username, string[] HateSeeker)
        {
            using (repository)
            {
                Random randomGenerator = new Random(DateTime.Now.Millisecond);
                int randomNumber = randomGenerator.Next(0, HateSeeker.Length - 1);
                return string.Format((repository.GetRandomHatePhrase() ?? new HatePhrases()).Phrase, username, HateSeeker[randomNumber]);
            }
        }
    }
}
