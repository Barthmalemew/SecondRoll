using Microsoft.EntityFrameworkCore;
using SecondRoll.Models;

namespace SecondRoll.Data
{
    public class PlayerContext : DbContext
    {
        public PlayerContext(DbContextOptions<PlayerContext> options) : base(options) {}

        public DbSet<Player> Players { get; set; }
    }
}