using BotCore.DependencyInjection;
using BotCore.Interfaces.Repository;
using BotCore.Model.Entities;
using System;

namespace BotCore.Controller
{
    /// <summary>
    /// Controller which handels Love management
    /// </summary>
    public class LoveController
    {
        private ILoveRepository repository;
        /// <summary>
        /// Constructor which will use the Service locater to create a ILoveRepository
        /// <see cref="ServiceLocator"/>
        /// <seealso cref="ILoveRepository"/>
        /// </summary>
        public LoveController()
        {
            repository = ServiceLocator.GetInstance<ILoveRepository>();
        }
        /// <summary>
        /// Will Return a Random Love Phrase for further process the Phrase will include {0} {1} for String Format function.
        /// <see cref="String.Format(string, string, string)"/>
        /// </summary>
        /// <returns></returns>
        public string GetRandomLovePhrase()
        {
            using (repository)
            {
                return (repository.GetRandomLovePhrase() ?? new LovePhrases()).Phrase;
            }
        }
        /// <summary>
        /// Will return a Love phrase wihtout the need for further process.
        /// </summary>
        /// <param name="username">name of user who initiated the command</param>
        /// <param name="loveSeeker">a list of user where one will be chosen randomly to optain the Love.</param>
        /// <returns></returns>
        public string GetRandomLovePhrase(string username, string [] loveSeeker)
        {
            using (repository)
            {
                Random randomGenerator = new Random(DateTime.Now.Millisecond);
                int randomNumber = randomGenerator.Next(0, loveSeeker.Length);
                return string.Format((repository.GetRandomLovePhrase() ?? new LovePhrases()).Phrase, username, loveSeeker[randomNumber]);
            }
        }
    }
}
