using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GymCoach_Coaches_CoachId1",
                table: "GymCoach");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainees_Coaches_CoachId",
                table: "Trainees");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainees_Memberships_MembershipId",
                table: "Trainees");

            migrationBuilder.DropIndex(
                name: "IX_GymCoach_CoachId1",
                table: "GymCoach");

            migrationBuilder.DropColumn(
                name: "CoachId1",
                table: "GymCoach");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Trainees",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "GymOwners",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "CoachId",
                table: "GymCoach",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Trainees_AppUserId",
                table: "Trainees",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GymOwners_AppUserId",
                table: "GymOwners",
                column: "AppUserId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_GymOwners_AspNetUsers_AppUserId",
                table: "GymOwners",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainees_AspNetUsers_AppUserId",
                table: "Trainees",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainees_Coaches_CoachId",
                table: "Trainees",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainees_Memberships_MembershipId",
                table: "Trainees",
                column: "MembershipId",
                principalTable: "Memberships",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GymCoach_Coaches_CoachId",
                table: "GymCoach");

            migrationBuilder.DropForeignKey(
                name: "FK_GymOwners_AspNetUsers_AppUserId",
                table: "GymOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainees_AspNetUsers_AppUserId",
                table: "Trainees");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainees_Coaches_CoachId",
                table: "Trainees");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainees_Memberships_MembershipId",
                table: "Trainees");

            migrationBuilder.DropIndex(
                name: "IX_Trainees_AppUserId",
                table: "Trainees");

            migrationBuilder.DropIndex(
                name: "IX_GymOwners_AppUserId",
                table: "GymOwners");

            migrationBuilder.DropIndex(
                name: "IX_GymCoach_CoachId",
                table: "GymCoach");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "GymOwners");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Trainees_Coaches_CoachId",
                table: "Trainees",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainees_Memberships_MembershipId",
                table: "Trainees",
                column: "MembershipId",
                principalTable: "Memberships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
