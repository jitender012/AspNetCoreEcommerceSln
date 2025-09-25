using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BrandUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands",
                schema: "Product",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                schema: "Product",
                table: "Products",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands",
                schema: "Product",
                table: "Products",
                column: "BrandId",
                principalSchema: "Marketing",
                principalTable: "Brands",
                principalColumn: "BrandId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands",
                schema: "Product",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_BrandId",
                schema: "Product",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands",
                schema: "Product",
                table: "Products",
                column: "ProductId",
                principalSchema: "Marketing",
                principalTable: "Brands",
                principalColumn: "BrandId");
        }
    }
}
