using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellingMobileApp.Web.Migrations
{
    /// <inheritdoc />
    public partial class FixProblems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldComment: "Id of the user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                comment: "Id of the user",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
