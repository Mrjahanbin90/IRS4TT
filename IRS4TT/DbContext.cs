using IRS4TT.Domains;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace IRS4TT.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cover> Covers { get; set; }
        public DbSet<CoverGroup> CoverGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cover>()
                .HasOne(c => c.Group)
                .WithMany(g => g.Covers)
                .HasForeignKey(c => c.GroupId);
        }
    }
}
