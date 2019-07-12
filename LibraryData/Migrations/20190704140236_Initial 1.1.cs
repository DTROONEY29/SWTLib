using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryData.Migrations
{
    public partial class Initial11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookmarks",
                table: "Bookmarks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Bookmarks");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Rentals",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Bookmarks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookmarks",
                table: "Bookmarks",
                columns: new[] { "UserId", "BookId" });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    IsChairMember = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_UserId",
                table: "Rentals",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmarks_User_UserId",
                table: "Bookmarks",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_User_UserId",
                table: "Rentals",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmarks_User_UserId",
                table: "Bookmarks");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_User_UserId",
                table: "Rentals");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_UserId",
                table: "Rentals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookmarks",
                table: "Bookmarks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Bookmarks");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Bookmarks",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookmarks",
                table: "Bookmarks",
                column: "Id");
        }
    }
}
