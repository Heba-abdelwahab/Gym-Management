using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIsExtra_EditMemebershipinFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Features_Memberships_MembershipId",
                table: "Features");

            migrationBuilder.DropIndex(
                name: "IX_Features_MembershipId",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "MembershipId",
                table: "Features");

            migrationBuilder.AddColumn<bool>(
                name: "IsExtra",
                table: "Features",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.CreateIndex(
                name: "IX_GymFeatureMembership_MembershipsId",
                table: "GymFeatureMembership",
                column: "MembershipsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GymFeatureMembership");

            migrationBuilder.DropColumn(
                name: "IsExtra",
                table: "Features");

            migrationBuilder.AddColumn<int>(
                name: "MembershipId",
                table: "Features",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Features_MembershipId",
                table: "Features",
                column: "MembershipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Features_Memberships_MembershipId",
                table: "Features",
                column: "MembershipId",
                principalTable: "Memberships",
                principalColumn: "Id");
        }
    }
}
