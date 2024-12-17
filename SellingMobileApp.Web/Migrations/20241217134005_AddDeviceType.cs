using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellingMobileApp.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeviceTypeId",
                table: "CreateListings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DeviceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreateListings_DeviceTypeId",
                table: "CreateListings",
                column: "DeviceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreateListings_DeviceTypes_DeviceTypeId",
                table: "CreateListings",
                column: "DeviceTypeId",
                principalTable: "DeviceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreateListings_DeviceTypes_DeviceTypeId",
                table: "CreateListings");

            migrationBuilder.DropTable(
                name: "DeviceTypes");

            migrationBuilder.DropIndex(
                name: "IX_CreateListings_DeviceTypeId",
                table: "CreateListings");

            migrationBuilder.DropColumn(
                name: "DeviceTypeId",
                table: "CreateListings");
        }
    }
}
