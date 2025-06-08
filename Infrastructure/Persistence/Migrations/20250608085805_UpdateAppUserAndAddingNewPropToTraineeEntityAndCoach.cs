using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppUserAndAddingNewPropToTraineeEntityAndCoach : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coaches_AspNetUsers_AppUserId",
                table: "Coaches");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainees_Memberships_MembershipId",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Trainees",
                newName: "ImageUrl");

            migrationBuilder.AlterColumn<int>(
                name: "MembershipId",
                table: "Trainees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GymId",
                table: "Trainees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Trainees",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Trainees",
                type: "date",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Coaches",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Coaches",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Trainees_AppUserId",
                table: "Trainees",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coaches_AspNetUsers_AppUserId",
                table: "Coaches",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainees_AspNetUsers_AppUserId",
                table: "Trainees",
                column: "AppUserId",
                principalTable: "AspNetUsers",
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
                name: "FK_Coaches_AspNetUsers_AppUserId",
                table: "Coaches");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainees_AspNetUsers_AppUserId",
                table: "Trainees");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainees_Memberships_MembershipId",
                table: "Trainees");

            migrationBuilder.DropIndex(
                name: "IX_Trainees_AppUserId",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Coaches");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Trainees",
                newName: "Image");

            migrationBuilder.AlterColumn<int>(
                name: "MembershipId",
                table: "Trainees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GymId",
                table: "Trainees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Coaches",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Coaches_AspNetUsers_AppUserId",
                table: "Coaches",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
