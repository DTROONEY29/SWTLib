using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryData.Migrations
{
    public partial class Final11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Roles_RoleId1",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_RoleId1",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                table: "Roles");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "RoleId1",
                table: "Roles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RoleId1",
                table: "Roles",
                column: "RoleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Roles_RoleId1",
                table: "Roles",
                column: "RoleId1",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
