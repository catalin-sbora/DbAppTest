using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DbAppTest.Migrations
{
    /// <inheritdoc />
    public partial class initalmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MomentInTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Temperature = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherEntries", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "WeatherEntries",
                columns: new[] { "Id", "City", "MomentInTime", "Temperature" },
                values: new object[,]
                {
                    { 1, "Craiova", new DateTime(2025, 5, 12, 16, 3, 17, 935, DateTimeKind.Utc).AddTicks(1574), 36.3m },
                    { 2, "Bucharest", new DateTime(2025, 5, 12, 16, 3, 17, 935, DateTimeKind.Utc).AddTicks(1578), 32.3m },
                    { 3, "London", new DateTime(2025, 5, 12, 16, 3, 17, 935, DateTimeKind.Utc).AddTicks(1580), 22.3m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherEntries");
        }
    }
}
