using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updategymcoach : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentCapcity",
                table: "Coaches");

            migrationBuilder.AddColumn<int>(
                name: "CurrentCapcity",
                table: "GymCoach",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentCapcity",
                table: "GymCoach");

            migrationBuilder.AddColumn<int>(
                name: "CurrentCapcity",
                table: "Coaches",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
