using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Galaxi.Functions.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "DBO");

            migrationBuilder.CreateTable(
                name: "MovieFunction",
                schema: "DBO",
                columns: table => new
                {
                    FunctionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FunctionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Room = table.Column<int>(type: "int", nullable: false),
                    NumberOfSeats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieFunction", x => x.FunctionId);
                });

            migrationBuilder.InsertData(
                schema: "DBO",
                table: "MovieFunction",
                columns: new[] { "FunctionId", "FunctionDate", "MovieId", "NumberOfSeats", "Price", "Room" },
                values: new object[,]
                {
                    { new Guid("7666f4a4-608a-4791-892e-82b7bfb08668"), new DateTime(2024, 9, 3, 21, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b8fb2e1e-d07b-4838-b103-28985b56305d"), 120, 12.00m, 2 },
                    { new Guid("92c48606-23c0-46d6-b4bb-aa4f47b18d1e"), new DateTime(2024, 9, 6, 20, 0, 0, 0, DateTimeKind.Unspecified), new Guid("01868b3c-3a19-458c-815e-824364b244bd"), 200, 18.50m, 5 },
                    { new Guid("e498d71c-c2cd-47a2-8f81-b4f9ba95a4c8"), new DateTime(2024, 9, 4, 15, 0, 0, 0, DateTimeKind.Unspecified), new Guid("88e4a35e-15e2-4e40-b176-221d213bd2f7"), 150, 15.00m, 3 },
                    { new Guid("ecd93d28-035f-4219-a958-d618627e426f"), new DateTime(2024, 9, 5, 11, 0, 0, 0, DateTimeKind.Unspecified), new Guid("2dacbeac-df0f-438c-9def-68eaa914670b"), 80, 8.00m, 4 },
                    { new Guid("ff116ad1-2527-4eac-92dc-97838d61ee7e"), new DateTime(2024, 9, 3, 18, 30, 0, 0, DateTimeKind.Unspecified), new Guid("7e9c40da-756a-44c4-a84f-580d01b4db1b"), 100, 10.50m, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieFunction",
                schema: "DBO");
        }
    }
}
