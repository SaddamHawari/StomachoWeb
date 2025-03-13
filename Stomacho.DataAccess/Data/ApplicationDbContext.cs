using Microsoft.EntityFrameworkCore;
using Stomacho.Models;

namespace Stomacho.DataAccess.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Restaurant>().HasData(
            new Restaurant { Id=1, Name="ShandarMomo", Cuisine="Indian & Chinese", Location="Shankhamul", ImageUrl="", IsActive= true, Rating= 4},
            new Restaurant { Id = 2, Name = "SpicyBites", Cuisine = "Mexican & Thai", Location = "Baneshwor", ImageUrl = "", IsActive = true, Rating = 4.5 },
            new Restaurant { Id = 3, Name = "FlavorsHub", Cuisine = "Italian & Continental", Location = "Lazimpat", ImageUrl = "", IsActive = true, Rating = 4.2 }
            );
    }

    public DbSet<Restaurant> Restaurants { get; set; }
}

