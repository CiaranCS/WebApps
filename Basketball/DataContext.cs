using Azure;
using Basketball.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Basketball
{
    public class DataContext: DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-J5L41NI;Initial Catalog=BasketballDb;Integrated Security=True; TrustServerCertificate=True");
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameTeam> GameTeams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasMany(e => e.Teams)
                .WithMany(e => e.Games)
                .UsingEntity<GameTeam>(j => j
                    .HasOne(gt => gt.Team)
                    .WithMany(t => t.GameTeams)
                    .HasForeignKey(gt => gt.TeamId),
                j => j
                    .HasOne(gt => gt.Game)
                    .WithMany(g => g.GameTeams)
                    .HasForeignKey(gt => gt.GameId),
                j =>
                {
                    j.HasKey(t => new { t.GameId, t.TeamId });
                });
        }
    }
}
