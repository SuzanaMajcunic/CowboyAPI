using Microsoft.EntityFrameworkCore;

namespace CowboyAPI.Models
{
    public class CowboyDbContext : DbContext
    {
        public CowboyDbContext(DbContextOptions<CowboyDbContext> options) : base(options)
        {

        }

        public DbSet<Cowboy> Cowboys { get; set; }
    }
}
