using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sucre_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EditTableappUsergroupUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_GroupUsers_GroupId",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_GroupId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "GroupUsers");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "AppUsers",
                newName: "GroupNumber");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "GroupUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "GroupUsers");

            migrationBuilder.RenameColumn(
                name: "GroupNumber",
                table: "AppUsers",
                newName: "GroupId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "GroupUsers",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_GroupId",
                table: "AppUsers",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_GroupUsers_GroupId",
                table: "AppUsers",
                column: "GroupId",
                principalTable: "GroupUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
