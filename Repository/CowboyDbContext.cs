using Cowboy.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cowboy.Repository
{
    public class CowboyDbContext : DbContext
    {
        public CowboyDbContext(DbContextOptions<CowboyDbContext> options) : base(options)
        {

        }

        public DbSet<CowboyEntity> Cowboys { get; set; }
        public DbSet<FirearmEntity> Firearms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CowboyEntity>()
                .HasOne(p => p.Firearm)
                .WithOne(p => p.Cowboy)
                .HasForeignKey<FirearmEntity>(p => p.CowboyId)
                .IsRequired();
        }
    }
}
