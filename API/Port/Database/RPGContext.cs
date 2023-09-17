using API.Modells.Fights;
using API.Modells.Monsters;
using API.Modells.Players;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Reflection.Metadata;

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
        }
    }
}