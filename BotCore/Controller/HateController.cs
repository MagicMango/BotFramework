using BotCore.DependencyInjection;
using BotCore.Interfaces.Repository;
using BotEntitiesNETF.Models.Entities;
using System;

namespace BotCore.Controller
{
    /// <summary>
    /// Controller which handels Hate management
    /// </summary>
    public class HateController
    {
        private IHateRepository repository;
        /// <summary>
        /// Constructor which will use the Service locater to create a IHateRepository
        /// <see cref="ServiceLocator"/>
        /// <seealso cref="IHateRepository"/>
        /// </summary>
        public HateController()
        {
            repository = ServiceLocator.GetInstance<IHateRepository>();
        }
        /// <summary>
        /// Will Return a Random hate Phrase for further process the Phrase will include {0} {1} for String Format function.
        /// <see cref="String.Format(string, string, string)"/>
        /// </summary>
        /// <returns></returns>
        public string GetRandomHatePhrase()
        {
            using (repository)
            {
                return (repository.GetRandomHatePhrase() ?? new HatePhrases()).Phrase;
            }
        }
        /// <summary>
        /// Will return a hate phrase wihtout the need for further process.
        /// </summary>
        /// <param name="username">name of user who initiated the command</param>
        /// <param name="HateSeeker">a list of user where one will be chosen randomly to optain the hate.</param>
        /// <returns></returns>
        public string GetRandomHatePhrase(string username, string[] HateSeeker)
        {
            using (repository)
            {
                Random randomGenerator = new Random(DateTime.Now.Millisecond);
                int randomNumber = randomGenerator.Next(0, HateSeeker.Length);
                return string.Format((repository.GetRandomHatePhrase() ?? new HatePhrases()).Phrase, username, HateSeeker[randomNumber]);
            }
        }
    }
}
