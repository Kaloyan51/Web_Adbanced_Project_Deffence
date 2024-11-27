using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellingMobileApp.Web.Migrations
{
    /// <inheritdoc />
    public partial class Update_UserField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreateListings_Users_OwnerId",
                table: "CreateListings");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "CreateListings",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CreateListings_OwnerId",
                table: "CreateListings",
                newName: "IX_CreateListings_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreateListings_Users_UserId",
                table: "CreateListings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreateListings_Users_UserId",
                table: "CreateListings");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "CreateListings",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_CreateListings_UserId",
                table: "CreateListings",
                newName: "IX_CreateListings_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreateListings_Users_OwnerId",
                table: "CreateListings",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
