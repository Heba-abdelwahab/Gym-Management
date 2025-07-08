using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addMediaImageToFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "GymFeature",
                newName: "Image_Url");

            migrationBuilder.AddColumn<bool>(
                name: "Image_IsMain",
                table: "GymFeature",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Image_PublicId",
                table: "GymFeature",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Image_Type",
                table: "GymFeature",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image_IsMain",
                table: "GymFeature");

            migrationBuilder.DropColumn(
                name: "Image_PublicId",
                table: "GymFeature");

            migrationBuilder.DropColumn(
                name: "Image_Type",
                table: "GymFeature");

            migrationBuilder.RenameColumn(
                name: "Image_Url",
                table: "GymFeature",
                newName: "Image");
        }
    }
}
