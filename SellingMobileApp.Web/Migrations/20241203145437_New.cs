using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellingMobileApp.Web.Migrations
{
    /// <inheritdoc />
    public partial class New : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreateListingId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CreateListingId",
                table: "Reviews",
                column: "CreateListingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_CreateListings_CreateListingId",
                table: "Reviews",
                column: "CreateListingId",
                principalTable: "CreateListings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_CreateListings_CreateListingId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CreateListingId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CreateListingId",
                table: "Reviews");
        }
    }
}
