using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Galaxi.Functions.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class function2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "test",
                schema: "DBO",
                table: "MovieFunction");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "test",
                schema: "DBO",
                table: "MovieFunction",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
