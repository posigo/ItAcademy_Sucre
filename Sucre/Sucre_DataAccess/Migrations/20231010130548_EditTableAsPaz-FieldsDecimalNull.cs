using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sucre_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EditTableAsPazFieldsDecimalNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AsPazs_Canals_CanalId",
                table: "AsPazs");

            migrationBuilder.DropIndex(
                name: "IX_AsPazs_CanalId",
                table: "AsPazs");

            migrationBuilder.AlterColumn<decimal>(
                name: "W_Low",
                table: "AsPazs",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "W_High",
                table: "AsPazs",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Low",
                table: "AsPazs",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "High",
                table: "AsPazs",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "CanalId",
                table: "AsPazs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "A_Low",
                table: "AsPazs",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "A_High",
                table: "AsPazs",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateIndex(
                name: "IX_AsPazs_CanalId",
                table: "AsPazs",
                column: "CanalId",
                unique: true,
                filter: "[CanalId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AsPazs_Canals_CanalId",
                table: "AsPazs",
                column: "CanalId",
                principalTable: "Canals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AsPazs_Canals_CanalId",
                table: "AsPazs");

            migrationBuilder.DropIndex(
                name: "IX_AsPazs_CanalId",
                table: "AsPazs");

            migrationBuilder.AlterColumn<decimal>(
                name: "W_Low",
                table: "AsPazs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "W_High",
                table: "AsPazs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Low",
                table: "AsPazs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "High",
                table: "AsPazs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CanalId",
                table: "AsPazs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "A_Low",
                table: "AsPazs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "A_High",
                table: "AsPazs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AsPazs_CanalId",
                table: "AsPazs",
                column: "CanalId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AsPazs_Canals_CanalId",
                table: "AsPazs",
                column: "CanalId",
                principalTable: "Canals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
