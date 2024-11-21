using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellingMobileApp.Web.Migrations
{
    /// <inheritdoc />
    public partial class ConnectionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersCreateListings",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ListingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersCreateListings", x => new { x.UserId, x.ListingId });
                    table.ForeignKey(
                        name: "FK_UsersCreateListings_CreateListings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "CreateListings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersCreateListings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersCreateListings_ListingId",
                table: "UsersCreateListings",
                column: "ListingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersCreateListings");
        }
    }
}
