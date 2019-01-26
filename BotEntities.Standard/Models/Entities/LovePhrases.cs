using BotCore.Interfaces;

namespace BotEntitiesNETCore.Models.Entities
{
   
    public partial class LovePhrases : ILovePhrase
    {
        public int Id { get; set; }
        public string Phrase { get; set; }
    }
}
