﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Stomacho.DataAccess.Data;

#nullable disable

namespace Stomacho.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250314122140_MadeImageUrlNull")]
    partial class MadeImageUrlNull
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Stomacho.Models.Restaurant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Cuisine")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Restaurants");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Cuisine = "Indian & Chinese",
                            ImageUrl = "",
                            IsActive = true,
                            Location = "Shankhamul",
                            Name = "ShandarMomo",
                            Rating = 4.0
                        },
                        new
                        {
                            Id = 2,
                            Cuisine = "Mexican & Thai",
                            ImageUrl = "",
                            IsActive = true,
                            Location = "Baneshwor",
                            Name = "SpicyBites",
                            Rating = 4.5
                        },
                        new
                        {
                            Id = 3,
                            Cuisine = "Italian & Continental",
                            ImageUrl = "",
                            IsActive = true,
                            Location = "Lazimpat",
                            Name = "FlavorsHub",
                            Rating = 4.2000000000000002
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
