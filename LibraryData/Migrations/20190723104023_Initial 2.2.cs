using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryData.Migrations
{
    public partial class Initial22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "WaitList",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "RoleId1",
                table: "Roles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WaitList_RoleId",
                table: "WaitList",
                column: "RoleId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_WaitList_Roles_RoleId",
                table: "WaitList",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Roles_RoleId1",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_WaitList_Roles_RoleId",
                table: "WaitList");

            migrationBuilder.DropIndex(
                name: "IX_WaitList_RoleId",
                table: "WaitList");

            migrationBuilder.DropIndex(
                name: "IX_Roles_RoleId1",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                table: "Roles");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "WaitList",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 0);
        }
    }
}
