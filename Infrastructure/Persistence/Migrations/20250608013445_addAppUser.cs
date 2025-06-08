using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Trainees",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Trainees_AppUserId",
                table: "Trainees",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainees_AspNetUsers_AppUserId",
                table: "Trainees",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainees_AspNetUsers_AppUserId",
                table: "Trainees");

            migrationBuilder.DropIndex(
                name: "IX_Trainees_AppUserId",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Trainees");
        }
    }
}
