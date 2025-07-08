using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateLogoMediaGym : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Media",
                newName: "MediaValue_Url");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Media",
                newName: "MediaValue_Type");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "Media",
                newName: "MediaValue_PublicId");

            migrationBuilder.RenameColumn(
                name: "IsMain",
                table: "Media",
                newName: "MediaValue_IsMain");

            migrationBuilder.RenameColumn(
                name: "Media",
                table: "Gyms",
                newName: "Media_Url");

            migrationBuilder.AddColumn<bool>(
                name: "Media_IsMain",
                table: "Gyms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Media_PublicId",
                table: "Gyms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Media_Type",
                table: "Gyms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Media_IsMain",
                table: "Gyms");

            migrationBuilder.DropColumn(
                name: "Media_PublicId",
                table: "Gyms");

            migrationBuilder.DropColumn(
                name: "Media_Type",
                table: "Gyms");

            migrationBuilder.RenameColumn(
                name: "MediaValue_Url",
                table: "Media",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "MediaValue_Type",
                table: "Media",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "MediaValue_PublicId",
                table: "Media",
                newName: "PublicId");

            migrationBuilder.RenameColumn(
                name: "MediaValue_IsMain",
                table: "Media",
                newName: "IsMain");

            migrationBuilder.RenameColumn(
                name: "Media_Url",
                table: "Gyms",
                newName: "Media");
        }
    }
}
