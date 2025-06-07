using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateCoachEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GymCoach_Coaches_CoachId",
                table: "GymCoach");

            migrationBuilder.DropIndex(
                name: "IX_GymCoach_CoachId",
                table: "GymCoach");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Coaches");

            migrationBuilder.AlterColumn<string>(
                name: "CoachId",
                table: "GymCoach",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CoachId1",
                table: "GymCoach",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GymCoach_CoachId1",
                table: "GymCoach",
                column: "CoachId1");

            migrationBuilder.AddForeignKey(
                name: "FK_GymCoach_Coaches_CoachId1",
                table: "GymCoach",
                column: "CoachId1",
                principalTable: "Coaches",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GymCoach_Coaches_CoachId1",
                table: "GymCoach");

            migrationBuilder.DropIndex(
                name: "IX_GymCoach_CoachId1",
                table: "GymCoach");

            migrationBuilder.DropColumn(
                name: "CoachId1",
                table: "GymCoach");

            migrationBuilder.AlterColumn<int>(
                name: "CoachId",
                table: "GymCoach",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_GymCoach_CoachId",
                table: "GymCoach",
                column: "CoachId");

            migrationBuilder.AddForeignKey(
                name: "FK_GymCoach_Coaches_CoachId",
                table: "GymCoach",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "Id");
        }
    }
}
