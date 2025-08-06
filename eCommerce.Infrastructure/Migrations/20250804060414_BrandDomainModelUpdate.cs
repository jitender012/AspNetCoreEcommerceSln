using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BrandDomainModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_AspNetUsers_CreatedByNavigationId",
                schema: "Marketing",
                table: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_Brands_CreatedByNavigationId",
                schema: "Marketing",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "CreatedByNavigationId",
                schema: "Marketing",
                table: "Brands");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_CreatedBy",
                schema: "Marketing",
                table: "Brands",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_UpdatedBy",
                schema: "Marketing",
                table: "Brands",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_AspNetUsers_CreatedBy",
                schema: "Marketing",
                table: "Brands",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_AspNetUsers_UpdatedBy",
                schema: "Marketing",
                table: "Brands",
                column: "UpdatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_AspNetUsers_CreatedBy",
                schema: "Marketing",
                table: "Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_Brands_AspNetUsers_UpdatedBy",
                schema: "Marketing",
                table: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_Brands_CreatedBy",
                schema: "Marketing",
                table: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_Brands_UpdatedBy",
                schema: "Marketing",
                table: "Brands");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByNavigationId",
                schema: "Marketing",
                table: "Brands",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Brands_CreatedByNavigationId",
                schema: "Marketing",
                table: "Brands",
                column: "CreatedByNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_AspNetUsers_CreatedByNavigationId",
                schema: "Marketing",
                table: "Brands",
                column: "CreatedByNavigationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
