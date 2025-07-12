using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class intial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastActive = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsExtra = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Muscles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Muscles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hamada = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admins_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Specializations = table.Column<int>(type: "int", nullable: false),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Street = table.Column<string>(type: "varchar(50)", nullable: false),
                    Address_City = table.Column<string>(type: "varchar(50)", nullable: false),
                    Address_Country = table.Column<string>(type: "varchar(50)", nullable: false),
                    Address_Location_X = table.Column<double>(type: "float", nullable: true),
                    Address_Location_Y = table.Column<double>(type: "float", nullable: true),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    Image_Type = table.Column<int>(type: "int", nullable: false),
                    Image_Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image_PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image_IsMain = table.Column<bool>(type: "bit", nullable: false),
                    CV_Type = table.Column<int>(type: "int", nullable: false),
                    CV_Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CV_PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CV_IsMain = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coaches_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GymOwners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymOwners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GymOwners_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SenderUsername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RecipientUsername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateRead = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MessageSent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SenderDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RecipientDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photo_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Connections",
                columns: table => new
                {
                    ConnectionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connections", x => x.ConnectionId);
                    table.ForeignKey(
                        name: "FK_Connections_Groups_GroupName",
                        column: x => x.GroupName,
                        principalTable: "Groups",
                        principalColumn: "Name");
                });

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
                name: "Gyms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address_Street = table.Column<string>(type: "varchar(50)", nullable: false),
                    Address_City = table.Column<string>(type: "varchar(50)", nullable: false),
                    Address_Country = table.Column<string>(type: "varchar(50)", nullable: false),
                    Address_Location_X = table.Column<double>(type: "float", nullable: true),
                    Address_Location_Y = table.Column<double>(type: "float", nullable: true),
                    GymType = table.Column<int>(type: "int", nullable: false),
                    Media_Type = table.Column<int>(type: "int", nullable: false),
                    Media_Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Media_PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Media_IsMain = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Phone = table.Column<string>(type: "varchar(50)", nullable: false),
                    Description = table.Column<string>(type: "varchar(50)", nullable: false),
                    AddGymStatus = table.Column<int>(type: "int", nullable: false),
                    DeleteGymStatus = table.Column<int>(type: "int", nullable: false),
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
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    Status = table.Column<int>(type: "int", nullable: false),
                    Capcity = table.Column<int>(type: "int", nullable: false),
                    CurrentCapcity = table.Column<int>(type: "int", nullable: false),
                    ApplicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                name: "GymFeature",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image_Type = table.Column<int>(type: "int", nullable: false),
                    Image_Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image_PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image_IsMain = table.Column<bool>(type: "bit", nullable: false),
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
                name: "Media",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MediaValue_Type = table.Column<int>(type: "int", nullable: false),
                    MediaValue_Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MediaValue_PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MediaValue_IsMain = table.Column<bool>(type: "bit", nullable: false),
                    gymId = table.Column<int>(type: "int", nullable: true),
                    CoachId = table.Column<int>(type: "int", nullable: true),
                    FeatureId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Media_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coaches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Media_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Media_Gyms_gymId",
                        column: x => x.gymId,
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
                name: "WorkDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<TimeOnly>(type: "time", nullable: false),
                    End = table.Column<TimeOnly>(type: "time", nullable: false),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GymCoachId = table.Column<int>(type: "int", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "GymFeatureMembership",
                columns: table => new
                {
                    FeaturesId = table.Column<int>(type: "int", nullable: false),
                    MembershipsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymFeatureMembership", x => new { x.FeaturesId, x.MembershipsId });
                    table.ForeignKey(
                        name: "FK_GymFeatureMembership_GymFeature_FeaturesId",
                        column: x => x.FeaturesId,
                        principalTable: "GymFeature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GymFeatureMembership_Memberships_MembershipsId",
                        column: x => x.MembershipsId,
                        principalTable: "Memberships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trainees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address_Street = table.Column<string>(type: "varchar(50)", nullable: false),
                    Address_City = table.Column<string>(type: "varchar(50)", nullable: false),
                    Address_Country = table.Column<string>(type: "varchar(50)", nullable: false),
                    Address_Location_X = table.Column<double>(type: "float", nullable: true),
                    Address_Location_Y = table.Column<double>(type: "float", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MembershipStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MembershipEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    ReasonForJoining = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: true),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GymId = table.Column<int>(type: "int", nullable: true),
                    MembershipId = table.Column<int>(type: "int", nullable: true),
                    CoachId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainees_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trainees_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coaches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trainees_Gyms_GymId",
                        column: x => x.GymId,
                        principalTable: "Gyms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trainees_Memberships_MembershipId",
                        column: x => x.MembershipId,
                        principalTable: "Memberships",
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
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentIntentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientSecret = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayFor = table.Column<int>(type: "int", nullable: false),
                    TraineeId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Trainees_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "Trainees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TraineeSelectedFeature",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionCount = table.Column<int>(type: "int", nullable: false),
                    TotalCost = table.Column<double>(type: "float", nullable: false),
                    TraineeId = table.Column<int>(type: "int", nullable: false),
                    GymFeatureId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraineeSelectedFeature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TraineeSelectedFeature_GymFeature_GymFeatureId",
                        column: x => x.GymFeatureId,
                        principalTable: "GymFeature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TraineeSelectedFeature_Trainees_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "Trainees",
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

            migrationBuilder.CreateIndex(
                name: "IX_Admins_AppUserId",
                table: "Admins",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "IX_Coaches_AppUserId",
                table: "Coaches",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_GroupName",
                table: "Connections",
                column: "GroupName");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_TargetMuscleId",
                table: "Exercises",
                column: "TargetMuscleId");

            migrationBuilder.CreateIndex(
                name: "IX_ExercisesSchedules_CoachId",
                table: "ExercisesSchedules",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_ExercisesSchedules_TraineeId",
                table: "ExercisesSchedules",
                column: "TraineeId");

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
                name: "IX_GymFeatureMembership_MembershipsId",
                table: "GymFeatureMembership",
                column: "MembershipsId");

            migrationBuilder.CreateIndex(
                name: "IX_GymOwners_AppUserId",
                table: "GymOwners",
                column: "AppUserId");

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
                name: "IX_Media_CoachId",
                table: "Media",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_Media_FeatureId",
                table: "Media",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Media_gymId",
                table: "Media",
                column: "gymId");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_GymId",
                table: "Memberships",
                column: "GymId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RecipientId",
                table: "Messages",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_TraineeId",
                table: "Payments",
                column: "TraineeId");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_AppUserId",
                table: "Photo",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledExercises_ExerciseId",
                table: "ScheduledExercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledExercises_ExercisesScheduleId",
                table: "ScheduledExercises",
                column: "ExercisesScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainees_AppUserId",
                table: "Trainees",
                column: "AppUserId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ClassTrainee");

            migrationBuilder.DropTable(
                name: "Connections");

            migrationBuilder.DropTable(
                name: "GymFeatureMembership");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DropTable(
                name: "ScheduledExercises");

            migrationBuilder.DropTable(
                name: "TraineeSelectedFeature");

            migrationBuilder.DropTable(
                name: "WorkDays");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "MealSchedules");

            migrationBuilder.DropTable(
                name: "ExercisesSchedules");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "GymFeature");

            migrationBuilder.DropTable(
                name: "GymCoach");

            migrationBuilder.DropTable(
                name: "Trainees");

            migrationBuilder.DropTable(
                name: "Muscles");

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

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
