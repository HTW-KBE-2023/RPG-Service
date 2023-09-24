using API.Models.Fights;
using API.Models.Monsters;
using API.Models.Players;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace API.Port.Database
{
    public class RPGContext : DbContext
    {
        public RPGContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Fight> Fights { get; set; }
        public DbSet<Monster> Monsters { get; set; }
        public DbSet<Player> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fight>()
                        .Property(fight => fight.Summary)
                        .HasColumnName("Summary")
                        .HasConversion(
                            summary => JsonConvert.SerializeObject(summary),
                            summary => JsonConvert.DeserializeObject<List<string>>(summary));

            modelBuilder.Entity<Fight>()
                        .Navigation(fight => fight.Player)
                        .AutoInclude();

            modelBuilder.Entity<Fight>()
                        .Navigation(fight => fight.Enemy)
                        .AutoInclude();
        }
    }
}