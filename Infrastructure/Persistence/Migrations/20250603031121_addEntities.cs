using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "Trainees",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_Country",
                table: "Trainees",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Address_Location_X",
                table: "Trainees",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Address_Location_Y",
                table: "Trainees",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                table: "Trainees",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CoachId",
                table: "Trainees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GymId",
                table: "Trainees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Trainees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "MembershipEndDate",
                table: "Trainees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "MembershipId",
                table: "Trainees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "MembershipStartDate",
                table: "Trainees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Admins",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Street = table.Column<string>(type: "varchar(50)", nullable: false),
                    Address_City = table.Column<string>(type: "varchar(50)", nullable: false),
                    Address_Country = table.Column<string>(type: "varchar(50)", nullable: false),
                    Address_Location_X = table.Column<double>(type: "float", nullable: false),
                    Address_Location_Y = table.Column<double>(type: "float", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GymOwners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymOwners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Muscle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Muscle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExercisesSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    schedule_StartDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    schedule_EndDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CoachId = table.Column<int>(type: "int", nullable: false),
                    TraineeId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExercisesSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExercisesSchedules_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coaches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExercisesSchedules_Trainees_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "Trainees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    schedule_StartDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    schedule_EndDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CoachId = table.Column<int>(type: "int", nullable: false),
                    TraineeId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealSchedules_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coaches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MealSchedules_Trainees_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "Trainees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gyms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address_Street = table.Column<string>(type: "varchar(50)", nullable: false),
                    Address_City = table.Column<string>(type: "varchar(50)", nullable: false),
                    Address_Country = table.Column<string>(type: "varchar(50)", nullable: false),
                    Address_Location_X = table.Column<double>(type: "float", nullable: false),
                    Address_Location_Y = table.Column<double>(type: "float", nullable: false),
                    GymType = table.Column<int>(type: "int", nullable: false),
                    Media = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Phone = table.Column<string>(type: "varchar(50)", nullable: false),
                    Description = table.Column<string>(type: "varchar(50)", nullable: false),
                    GymOwnerId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gyms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gyms_GymOwners_GymOwnerId",
                        column: x => x.GymOwnerId,
                        principalTable: "GymOwners",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MuscleExercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Media = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Day = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MuscleId = table.Column<int>(type: "int", nullable: false),
                    ExercisesScheduleId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Day = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MealScheduleId = table.Column<int>(type: "int", nullable: false),
                    MealType = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meals_MealSchedules_MealScheduleId",
                        column: x => x.MealScheduleId,
                        principalTable: "MealSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    CurrentCapacity = table.Column<int>(type: "int", nullable: false),
                    GymId = table.Column<int>(type: "int", nullable: false),
                    CoachId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classes_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coaches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Classes_Gyms_GymId",
                        column: x => x.GymId,
                        principalTable: "Gyms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GymCoach",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    GymId = table.Column<int>(type: "int", nullable: false),
                    CoachId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymCoach", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GymCoach_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coaches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GymCoach_Gyms_GymId",
                        column: x => x.GymId,
                        principalTable: "Gyms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Memberships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    GymId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memberships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Memberships_Gyms_GymId",
                        column: x => x.GymId,
                        principalTable: "Gyms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClassTrainee",
                columns: table => new
                {
                    ClassesId = table.Column<int>(type: "int", nullable: false),
                    TraineesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassTrainee", x => new { x.ClassesId, x.TraineesId });
                    table.ForeignKey(
                        name: "FK_ClassTrainee_Classes_ClassesId",
                        column: x => x.ClassesId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassTrainee_Trainees_TraineesId",
                        column: x => x.TraineesId,
                        principalTable: "Trainees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<TimeOnly>(type: "time", nullable: false),
                    End = table.Column<TimeOnly>(type: "time", nullable: false),
                    Day = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GymCoachId = table.Column<int>(type: "int", nullable: false),
                    GymId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkDays_GymCoach_GymCoachId",
                        column: x => x.GymCoachId,
                        principalTable: "GymCoach",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkDays_Gyms_GymId",
                        column: x => x.GymId,
                        principalTable: "Gyms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MembershipId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Features_Memberships_MembershipId",
                        column: x => x.MembershipId,
                        principalTable: "Memberships",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GymFeature",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GymId = table.Column<int>(type: "int", nullable: false),
                    FeatureId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymFeature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GymFeature_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GymFeature_Gyms_GymId",
                        column: x => x.GymId,
                        principalTable: "Gyms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TraineeSelectedFeature",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionCount = table.Column<int>(type: "int", nullable: false),
                    TotalCost = table.Column<double>(type: "float", nullable: false),
                    TraineId = table.Column<int>(type: "int", nullable: false),
                    FeatureId = table.Column<int>(type: "int", nullable: false),
                    TraineeId = table.Column<int>(type: "int", nullable: false),
                    GymFeatureId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraineeSelectedFeature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TraineeSelectedFeature_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TraineeSelectedFeature_GymFeature_GymFeatureId",
                        column: x => x.GymFeatureId,
                        principalTable: "GymFeature",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TraineeSelectedFeature_Trainees_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "Trainees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trainees_CoachId",
                table: "Trainees",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainees_GymId",
                table: "Trainees",
                column: "GymId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainees_MembershipId",
                table: "Trainees",
                column: "MembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_CoachId",
                table: "Classes",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_GymId",
                table: "Classes",
                column: "GymId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassTrainee_TraineesId",
                table: "ClassTrainee",
                column: "TraineesId");

            migrationBuilder.CreateIndex(
                name: "IX_ExercisesSchedules_CoachId",
                table: "ExercisesSchedules",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_ExercisesSchedules_TraineeId",
                table: "ExercisesSchedules",
                column: "TraineeId");

            migrationBuilder.CreateIndex(
                name: "IX_Features_MembershipId",
                table: "Features",
                column: "MembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_GymCoach_CoachId",
                table: "GymCoach",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_GymCoach_GymId",
                table: "GymCoach",
                column: "GymId");

            migrationBuilder.CreateIndex(
                name: "IX_GymFeature_FeatureId",
                table: "GymFeature",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_GymFeature_GymId",
                table: "GymFeature",
                column: "GymId");

            migrationBuilder.CreateIndex(
                name: "IX_Gyms_GymOwnerId",
                table: "Gyms",
                column: "GymOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_MealScheduleId",
                table: "Meals",
                column: "MealScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_MealSchedules_CoachId",
                table: "MealSchedules",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_MealSchedules_TraineeId",
                table: "MealSchedules",
                column: "TraineeId");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_GymId",
                table: "Memberships",
                column: "GymId");

            migrationBuilder.CreateIndex(
                name: "IX_MuscleExercises_ExercisesScheduleId",
                table: "MuscleExercises",
                column: "ExercisesScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_MuscleExercises_MuscleId",
                table: "MuscleExercises",
                column: "MuscleId");

            migrationBuilder.CreateIndex(
                name: "IX_TraineeSelectedFeature_FeatureId",
                table: "TraineeSelectedFeature",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_TraineeSelectedFeature_GymFeatureId",
                table: "TraineeSelectedFeature",
                column: "GymFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_TraineeSelectedFeature_TraineeId",
                table: "TraineeSelectedFeature",
                column: "TraineeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkDays_GymCoachId",
                table: "WorkDays",
                column: "GymCoachId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkDays_GymId",
                table: "WorkDays",
                column: "GymId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainees_Coaches_CoachId",
                table: "Trainees",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainees_Gyms_GymId",
                table: "Trainees",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainees_Memberships_MembershipId",
                table: "Trainees",
                column: "MembershipId",
                principalTable: "Memberships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainees_Coaches_CoachId",
                table: "Trainees");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainees_Gyms_GymId",
                table: "Trainees");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainees_Memberships_MembershipId",
                table: "Trainees");

            migrationBuilder.DropTable(
                name: "ClassTrainee");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "MuscleExercises");

            migrationBuilder.DropTable(
                name: "TraineeSelectedFeature");

            migrationBuilder.DropTable(
                name: "WorkDays");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "MealSchedules");

            migrationBuilder.DropTable(
                name: "ExercisesSchedules");

            migrationBuilder.DropTable(
                name: "Muscle");

            migrationBuilder.DropTable(
                name: "GymFeature");

            migrationBuilder.DropTable(
                name: "GymCoach");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "Coaches");

            migrationBuilder.DropTable(
                name: "Memberships");

            migrationBuilder.DropTable(
                name: "Gyms");

            migrationBuilder.DropTable(
                name: "GymOwners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Trainees_CoachId",
                table: "Trainees");

            migrationBuilder.DropIndex(
                name: "IX_Trainees_GymId",
                table: "Trainees");

            migrationBuilder.DropIndex(
                name: "IX_Trainees_MembershipId",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "Address_Country",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "Address_Location_X",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "Address_Location_Y",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "CoachId",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "GymId",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "MembershipEndDate",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "MembershipId",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "MembershipStartDate",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Admins");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "Id");
        }
    }
}
