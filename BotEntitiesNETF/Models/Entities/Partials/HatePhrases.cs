using BotCore.Interfaces;

namespace BotEntitiesNETF.Models.Entities
{
    public partial class HatePhrases: IHatePhrase
    {
        public HatePhrases()
        {
            Phrase = "There are no entries in db for {0} and {1}.";
        }
    }
}
