using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SellingMobileApp.Web.Migrations
{
    /// <inheritdoc />
    public partial class CreateListing_Category : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreateListings_Categories_CategoryId",
                table: "CreateListings");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "CreateListings",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                comment: "Title of the listing",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "Title of the listing");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CreateListings",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                comment: "Description detailing the specific phone",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldComment: "Description detailing the specific phone");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "CreateListings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Нов" },
                    { 2, "Употребяван" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CreateListings_Categories_CategoryId",
                table: "CreateListings",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreateListings_Categories_CategoryId",
                table: "CreateListings");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "CreateListings",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "Title of the listing",
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldComment: "Title of the listing");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CreateListings",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                comment: "Description detailing the specific phone",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldComment: "Description detailing the specific phone");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "CreateListings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                comment: "More information for phone status");

            migrationBuilder.AddForeignKey(
                name: "FK_CreateListings_Categories_CategoryId",
                table: "CreateListings",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
