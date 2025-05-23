﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stomacho.Models;

namespace Stomacho.DataAccess.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<OrderHeader> OrderHeaders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Restaurant>().HasData(
            new Restaurant 
            { 
                Id = 1, 
                Name="ShandarMomo", 
                Cuisine="Indian & Chinese", 
                Location="Shankhamul", 
                ImageUrl="", 
                IsActive= true
            },
            new Restaurant
            {
                Id = 2,
                Name = "SpicyBites",
                Cuisine = "Mexican & Thai",
                Location = "Baneshwor",
                ImageUrl = "",
                IsActive = true
            },
            new Restaurant
            {
                Id = 3,
                Name = "FlavorsHub",
                Cuisine = "Italian & Continental",
                Location = "Lazimpat",
                ImageUrl = "",
                IsActive = true
            });

        modelBuilder.Entity<MenuItem>().HasData(
            new MenuItem
            {
                Id = 1,
                Name = "Chicken Momo",
                Description = "Steamed dumplings filled with spiced chicken.",
                Price = 220,
                ImageUrl = "",
                IsAvailable = true,
                RestaurantId = 1
            },
            new MenuItem
            {
                Id = 2,
                Name = "Thakali Khana",
                Description = "Stir-fried rice noodles with tofu, peanuts, and lime.",
                Price = 450,
                ImageUrl = "",
                IsAvailable = true,
                RestaurantId = 2
            },
            new MenuItem
            {
                Id = 3,
                Name = "Margherita Pizza",
                Description = "Classic pizza with tomato sauce, mozzarella, and basil.",
                Price = 680,
                ImageUrl = "",
                IsAvailable = true,
                RestaurantId = 3
            });
    }
}

