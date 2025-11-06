using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CartUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_AspNetUsers_CartNavigationId",
                schema: "User",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_CartNavigationId",
                schema: "User",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "CartNavigationId",
                schema: "User",
                table: "Cart");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_CustomerId",
                schema: "User",
                table: "Cart",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_AspNetUsers_CustomerId",
                schema: "User",
                table: "Cart",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_AspNetUsers_CustomerId",
                schema: "User",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_CustomerId",
                schema: "User",
                table: "Cart");

            migrationBuilder.AddColumn<Guid>(
                name: "CartNavigationId",
                schema: "User",
                table: "Cart",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Cart_CartNavigationId",
                schema: "User",
                table: "Cart",
                column: "CartNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_AspNetUsers_CartNavigationId",
                schema: "User",
                table: "Cart",
                column: "CartNavigationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
