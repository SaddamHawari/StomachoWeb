using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Stomacho.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataToRestaurantTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Cuisine", "ImageUrl", "IsActive", "Location", "Name", "Rating" },
                values: new object[,]
                {
                    { 1, "Indian & Chinese", "", true, "Shankhamul", "ShandarMomo", 4.0 },
                    { 2, "Mexican & Thai", "", true, "Baneshwor", "SpicyBites", 4.5 },
                    { 3, "Italian & Continental", "", true, "Lazimpat", "FlavorsHub", 4.2000000000000002 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
