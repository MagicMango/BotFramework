using BotCore.Interfaces;

namespace BotEntitiesNETCore.Models.Entities
{
    public partial class HatePhrases: IHatePhrase
    {
        public int Id { get; set; }
        public string Phrase { get; set; }
    }
}
