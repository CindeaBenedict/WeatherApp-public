
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using WeatherApp.Models; 

namespace WeatherApp.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<FavoriteLocation> FavoriteLocations { get; set; }
        public DbSet<UserPreference> UserPreferences { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");

                if (!string.IsNullOrEmpty(connectionString))
                {
                    optionsBuilder.UseNpgsql(connectionString);
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
{
    base.OnModelCreating(builder);

    builder.Entity<FavoriteLocation>()
        .HasOne(f => f.ApplicationUser)
        .WithMany(u => u.FavoriteLocations)
        .HasForeignKey(f => f.ApplicationUserId);  // ✅ Ensure correct Foreign Key
    
    builder.Entity<FavoriteLocation>()
        .HasOne<Location>()
        .WithMany()
        .HasForeignKey(f => f.LocationId);  // ✅ Ensure correct Foreign Key
}

    }
}
