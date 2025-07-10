using System;
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
                name: "FK_Media_Features_featureId",
                table: "Media");

            migrationBuilder.RenameColumn(
                name: "featureId",
                table: "Media",
                newName: "FeatureId");

            migrationBuilder.RenameIndex(
                name: "IX_Media_featureId",
                table: "Media",
                newName: "IX_Media_FeatureId");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Coaches",
                newName: "Image_Url");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApplicationDate",
                table: "GymCoach",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "CV_IsMain",
                table: "Coaches",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CV_PublicId",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CV_Type",
                table: "Coaches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CV_Url",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Image_IsMain",
                table: "Coaches",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Image_PublicId",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Image_Type",
                table: "Coaches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Features_FeatureId",
                table: "Media",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_Features_FeatureId",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "ApplicationDate",
                table: "GymCoach");

            migrationBuilder.DropColumn(
                name: "CV_IsMain",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "CV_PublicId",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "CV_Type",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "CV_Url",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "Image_IsMain",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "Image_PublicId",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "Image_Type",
                table: "Coaches");

            migrationBuilder.RenameColumn(
                name: "FeatureId",
                table: "Media",
                newName: "featureId");

            migrationBuilder.RenameIndex(
                name: "IX_Media_FeatureId",
                table: "Media",
                newName: "IX_Media_featureId");

            migrationBuilder.RenameColumn(
                name: "Image_Url",
                table: "Coaches",
                newName: "ImageUrl");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Features_featureId",
                table: "Media",
                column: "featureId",
                principalTable: "Features",
                principalColumn: "Id");
        }
    }
}
