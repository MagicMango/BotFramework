using Microsoft.EntityFrameworkCore;

namespace BotEntitiesNETCore.Models.Entities
{
    public class StreamDBBotEntitiesCore : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=raspbx;user id=StreamUser;password=?6#)[#mwBXKfY`]b;persistsecurityinfo=True;database=StreamDBBot");
        }

        public virtual DbSet<LovePhrases> LovePhrases { get; set; }
        public virtual DbSet<HatePhrases> HatePhrases { get; set; }

    }
}
