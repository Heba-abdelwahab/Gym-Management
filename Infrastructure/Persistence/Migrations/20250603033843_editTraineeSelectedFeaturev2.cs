using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class editTraineeSelectedFeaturev2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TraineeSelectedFeature_Features_FeatureId",
                table: "TraineeSelectedFeature");

            migrationBuilder.DropForeignKey(
                name: "FK_TraineeSelectedFeature_GymFeature_GymFeatureId",
                table: "TraineeSelectedFeature");

            migrationBuilder.DropIndex(
                name: "IX_TraineeSelectedFeature_FeatureId",
                table: "TraineeSelectedFeature");

            migrationBuilder.DropColumn(
                name: "FeatureId",
                table: "TraineeSelectedFeature");

            migrationBuilder.AlterColumn<int>(
                name: "GymFeatureId",
                table: "TraineeSelectedFeature",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TraineeSelectedFeature_GymFeature_GymFeatureId",
                table: "TraineeSelectedFeature",
                column: "GymFeatureId",
                principalTable: "GymFeature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TraineeSelectedFeature_GymFeature_GymFeatureId",
                table: "TraineeSelectedFeature");

            migrationBuilder.AlterColumn<int>(
                name: "GymFeatureId",
                table: "TraineeSelectedFeature",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "FeatureId",
                table: "TraineeSelectedFeature",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TraineeSelectedFeature_FeatureId",
                table: "TraineeSelectedFeature",
                column: "FeatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_TraineeSelectedFeature_Features_FeatureId",
                table: "TraineeSelectedFeature",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TraineeSelectedFeature_GymFeature_GymFeatureId",
                table: "TraineeSelectedFeature",
                column: "GymFeatureId",
                principalTable: "GymFeature",
                principalColumn: "Id");
        }
    }
}
