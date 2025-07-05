using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ExerciseEntitiyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MuscleExercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Muscle",
                table: "Muscle");

            migrationBuilder.RenameTable(
                name: "Muscle",
                newName: "Muscles");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Muscles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Muscles",
                table: "Muscles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetMuscleId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercises_Muscles_TargetMuscleId",
                        column: x => x.TargetMuscleId,
                        principalTable: "Muscles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduledExercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExercisesScheduleId = table.Column<int>(type: "int", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduledExercises_ExercisesSchedules_ExercisesScheduleId",
                        column: x => x.ExercisesScheduleId,
                        principalTable: "ExercisesSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduledExercises_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_TargetMuscleId",
                table: "Exercises",
                column: "TargetMuscleId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledExercises_ExerciseId",
                table: "ScheduledExercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledExercises_ExercisesScheduleId",
                table: "ScheduledExercises",
                column: "ExercisesScheduleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduledExercises");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Muscles",
                table: "Muscles");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Muscles");

            migrationBuilder.RenameTable(
                name: "Muscles",
                newName: "Muscle");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Muscle",
                table: "Muscle",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MuscleExercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExercisesScheduleId = table.Column<int>(type: "int", nullable: false),
                    MuscleId = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Media = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuscleExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MuscleExercises_ExercisesSchedules_ExercisesScheduleId",
                        column: x => x.ExercisesScheduleId,
                        principalTable: "ExercisesSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MuscleExercises_Muscle_MuscleId",
                        column: x => x.MuscleId,
                        principalTable: "Muscle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MuscleExercises_ExercisesScheduleId",
                table: "MuscleExercises",
                column: "ExercisesScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_MuscleExercises_MuscleId",
                table: "MuscleExercises",
                column: "MuscleId");
        }
    }
}
