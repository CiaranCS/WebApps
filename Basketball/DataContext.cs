using Basketball.Entities;
using Microsoft.EntityFrameworkCore;

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

    }
}
