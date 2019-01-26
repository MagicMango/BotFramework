using BotCore.Interfaces;

namespace BotEntitiesNETF.Models.Entities
{

    public partial class LovePhrases: ILovePhrase
    {
        public LovePhrases()
        {
            Phrase = "There are no entries in db for {0} and {1}.";
        }
    }
}
