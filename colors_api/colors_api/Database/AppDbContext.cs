using colors_api.Models;
using Microsoft.EntityFrameworkCore;

namespace colors_api.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Color> Colors { get; set; } = null!;
        public DbSet<Palette> Palettes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Color>()
                .HasIndex(c => c.Id)
                .IsUnique();
            modelBuilder.Entity<Palette>()
                .HasIndex(p => p.Id)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
