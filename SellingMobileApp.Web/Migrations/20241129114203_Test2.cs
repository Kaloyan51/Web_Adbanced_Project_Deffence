﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellingMobileApp.Web.Migrations
{
    /// <inheritdoc />
    public partial class Test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreateListings_PhoneModels_PhoneModelId",
                table: "CreateListings");

            migrationBuilder.DropIndex(
                name: "IX_CreateListings_PhoneModelId",
                table: "CreateListings");

            migrationBuilder.DropColumn(
                name: "PhoneModelId",
                table: "CreateListings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PhoneModelId",
                table: "CreateListings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreateListings_PhoneModelId",
                table: "CreateListings",
                column: "PhoneModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreateListings_PhoneModels_PhoneModelId",
                table: "CreateListings",
                column: "PhoneModelId",
                principalTable: "PhoneModels",
                principalColumn: "Id");
        }
    }
}