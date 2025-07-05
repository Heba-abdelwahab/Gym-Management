using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateMedia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "featureId",
                table: "Media",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "gymId",
                table: "Media",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Media_featureId",
                table: "Media",
                column: "featureId");

            migrationBuilder.CreateIndex(
                name: "IX_Media_gymId",
                table: "Media",
                column: "gymId");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Features_featureId",
                table: "Media",
                column: "featureId",
                principalTable: "Features",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Gyms_gymId",
                table: "Media",
                column: "gymId",
                principalTable: "Gyms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_Features_featureId",
                table: "Media");

            migrationBuilder.DropForeignKey(
                name: "FK_Media_Gyms_gymId",
                table: "Media");

            migrationBuilder.DropIndex(
                name: "IX_Media_featureId",
                table: "Media");

            migrationBuilder.DropIndex(
                name: "IX_Media_gymId",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "featureId",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "gymId",
                table: "Media");
        }
    }
}
