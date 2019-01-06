using BotCore.Interfaces.Repository;
using BotCore.Model.Entities;

namespace BotCore.Controller
{
    public class LoveController
    {
        ILoveRepository repository;

        public LoveController()
        {
            this.repository = ServiceLocator.GetInstance<ILoveRepository>();
        }

        public string GetRandomLovePhrase()
        {
            using (repository)
            {
                return (repository.GetRandomLovePhrase() ?? new LovePhrases()).Phrase;
            }
        }
    }
}
