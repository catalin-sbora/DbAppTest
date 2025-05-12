using DbAppTest.model;
using Microsoft.EntityFrameworkCore;

namespace DbAppTest.datasource
{
    public class WeatherAppDbContext:DbContext
    {
        public DbSet<WeatherEntry> WeatherEntries { get; set; }
        public WeatherAppDbContext(DbContextOptions<WeatherAppDbContext> options) : base(options) 
        {
        }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherEntry>()
                        .HasData(
                                    new WeatherEntry { Id = 1, City = "Craiova", MomentInTime = DateTime.UtcNow, Temperature = 36.3m },
                                    new WeatherEntry { Id = 2, City = "Bucharest", MomentInTime = DateTime.UtcNow, Temperature = 32.3m},
                                    new WeatherEntry { Id = 3, City = "London", MomentInTime = DateTime.UtcNow, Temperature = 22.3m }
                                    );
            base.OnModelCreating(modelBuilder);
        }
    }
}
