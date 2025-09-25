using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_CreatedByNavigationId",
                schema: "Product",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_UpdatedByNavigationId",
                schema: "Product",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CreatedByNavigationId",
                schema: "Product",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UpdatedByNavigationId",
                schema: "Product",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedByNavigationId",
                schema: "Product",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UpdatedByNavigationId",
                schema: "Product",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UpdatedBy",
                schema: "Product",
                table: "Products",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_CreatedBy",
                schema: "Product",
                table: "Products",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_UpdatedBy",
                schema: "Product",
                table: "Products",
                column: "UpdatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_CreatedBy",
                schema: "Product",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_UpdatedBy",
                schema: "Product",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UpdatedBy",
                schema: "Product",
                table: "Products");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByNavigationId",
                schema: "Product",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByNavigationId",
                schema: "Product",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedByNavigationId",
                schema: "Product",
                table: "Products",
                column: "CreatedByNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UpdatedByNavigationId",
                schema: "Product",
                table: "Products",
                column: "UpdatedByNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_CreatedByNavigationId",
                schema: "Product",
                table: "Products",
                column: "CreatedByNavigationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_UpdatedByNavigationId",
                schema: "Product",
                table: "Products",
                column: "UpdatedByNavigationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
