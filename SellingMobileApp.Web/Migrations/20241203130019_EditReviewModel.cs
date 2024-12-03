using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellingMobileApp.Web.Migrations
{
    /// <inheritdoc />
    public partial class EditReviewModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Reviews",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "Comment of current listing");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "CreateListings",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                comment: "Title of the listing",
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldComment: "Title of the listing");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Reviews");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "CreateListings",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                comment: "Title of the listing",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldComment: "Title of the listing");
        }
    }
}
