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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuration des entités si nécessaire
            modelBuilder.Entity<Color>()
                .HasIndex(c => c.Name)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
