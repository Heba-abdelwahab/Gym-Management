using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class editWorkDayEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkDays_Gyms_GymId",
                table: "WorkDays");

            migrationBuilder.DropIndex(
                name: "IX_WorkDays_GymId",
                table: "WorkDays");

            migrationBuilder.DropColumn(
                name: "GymId",
                table: "WorkDays");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GymId",
                table: "WorkDays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WorkDays_GymId",
                table: "WorkDays",
                column: "GymId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkDays_Gyms_GymId",
                table: "WorkDays",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
