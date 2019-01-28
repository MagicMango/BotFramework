using Microsoft.EntityFrameworkCore;

namespace BotEntitiesNETCore.Models.Entities
{
    public class StreamDBBotEntitiesCore : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;user id=StreamUser;password=?6#)[#mwBXKfY`]b;persistsecurityinfo=True;database=BotDB");
        }

        public virtual DbSet<LovePhrases> LovePhrases { get; set; }
        public virtual DbSet<HatePhrases> HatePhrases { get; set; }

    }
}
