using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedProductVariant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductVarientId",
                schema: "Product",
                table: "ProductConfiguration",
                newName: "ProductVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductConfiguration_ProductVarientId",
                schema: "Product",
                table: "ProductConfiguration",
                newName: "IX_ProductConfiguration_ProductVariantId");

            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                schema: "Product",
                table: "ProductVariant",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Dimensions",
                schema: "Product",
                table: "ProductVariant",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                schema: "Product",
                table: "ProductVariant",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                schema: "Product",
                table: "ProductVariant",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Barcode",
                schema: "Product",
                table: "ProductVariant");

            migrationBuilder.DropColumn(
                name: "Dimensions",
                schema: "Product",
                table: "ProductVariant");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                schema: "Product",
                table: "ProductVariant");

            migrationBuilder.DropColumn(
                name: "Weight",
                schema: "Product",
                table: "ProductVariant");

            migrationBuilder.RenameColumn(
                name: "ProductVariantId",
                schema: "Product",
                table: "ProductConfiguration",
                newName: "ProductVarientId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductConfiguration_ProductVariantId",
                schema: "Product",
                table: "ProductConfiguration",
                newName: "IX_ProductConfiguration_ProductVarientId");
        }
    }
}
